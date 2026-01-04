-- =============================================
-- MindFit Intelligence - Views
-- =============================================

USE MindFitDB;
GO

-- =============================================
-- View: Active Members with Membership Details
-- =============================================
CREATE OR ALTER VIEW vw_ActiveMembersWithMemberships
AS
SELECT 
    m.Id AS MemberId,
    m.FirstName,
    m.LastName,
    m.Email,
    m.Phone,
    m.MemberSince,
    ms.Id AS MembershipId,
    mp.Name AS MembershipPlan,
    ms.StartDate,
    ms.EndDate,
    DATEDIFF(DAY, GETDATE(), ms.EndDate) AS DaysUntilExpiration,
    ms.Status AS MembershipStatus
FROM Members m
INNER JOIN Memberships ms ON m.Id = ms.MemberId
INNER JOIN MembershipPlans mp ON ms.MembershipPlanId = mp.Id
WHERE m.IsActive = 1 
    AND ms.Status = 'Active'
    AND ms.EndDate >= GETDATE();
GO

-- =============================================
-- View: Class Schedule Overview
-- =============================================
CREATE OR ALTER VIEW vw_ClassScheduleOverview
AS
SELECT 
    cs.Id AS ScheduleId,
    c.Name AS ClassName,
    c.Category,
    t.FirstName + ' ' + t.LastName AS TrainerName,
    cs.StartDateTime,
    cs.EndDateTime,
    cs.Room,
    c.MaxCapacity,
    cs.AvailableSpots,
    (c.MaxCapacity - cs.AvailableSpots) AS BookedSpots,
    cs.Status
FROM ClassSchedules cs
INNER JOIN Classes c ON cs.ClassId = c.Id
INNER JOIN Trainers t ON c.TrainerId = t.Id
WHERE cs.IsActive = 1;
GO

-- =============================================
-- View: Monthly Revenue Summary
-- =============================================
CREATE OR ALTER VIEW vw_MonthlyRevenueSummary
AS
SELECT 
    YEAR(PaymentDate) AS Year,
    MONTH(PaymentDate) AS Month,
    COUNT(*) AS TotalPayments,
    SUM(Amount) AS TotalRevenue,
    AVG(Amount) AS AveragePayment,
    MIN(Amount) AS MinPayment,
    MAX(Amount) AS MaxPayment
FROM Payments
WHERE Status = 'Completed'
GROUP BY YEAR(PaymentDate), MONTH(PaymentDate);
GO

-- =============================================
-- View: Member Attendance Statistics
-- =============================================
CREATE OR ALTER VIEW vw_MemberAttendanceStats
AS
SELECT 
    m.Id AS MemberId,
    m.FirstName + ' ' + m.LastName AS MemberName,
    COUNT(a.Id) AS TotalVisits,
    MAX(a.CheckInTime) AS LastVisit,
    AVG(DATEDIFF(MINUTE, a.CheckInTime, ISNULL(a.CheckOutTime, GETDATE()))) AS AvgDurationMinutes
FROM Members m
LEFT JOIN Attendances a ON m.Id = a.MemberId
WHERE m.IsActive = 1
GROUP BY m.Id, m.FirstName, m.LastName;
GO

-- =============================================
-- View: Trainer Class Load
-- =============================================
CREATE OR ALTER VIEW vw_TrainerClassLoad
AS
SELECT 
    t.Id AS TrainerId,
    t.FirstName + ' ' + t.LastName AS TrainerName,
    t.Specialization,
    COUNT(DISTINCT c.Id) AS TotalClasses,
    COUNT(cs.Id) AS TotalScheduledSessions,
    SUM(c.MaxCapacity - cs.AvailableSpots) AS TotalStudents
FROM Trainers t
LEFT JOIN Classes c ON t.Id = c.TrainerId
LEFT JOIN ClassSchedules cs ON c.Id = cs.ClassId 
    AND cs.Status IN ('Scheduled', 'InProgress')
WHERE t.IsActive = 1
GROUP BY t.Id, t.FirstName, t.LastName, t.Specialization;
GO

PRINT 'Views created successfully!';
GO
