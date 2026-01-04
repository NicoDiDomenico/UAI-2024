-- =============================================
-- MindFit Intelligence - Stored Procedures
-- =============================================

USE MindFitDB;
GO

-- =============================================
-- SP: Get Member with Active Membership
-- =============================================
CREATE OR ALTER PROCEDURE sp_GetMemberWithActiveMembership
    @MemberId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        m.*,
        ms.Id AS MembershipId,
        ms.StartDate,
        ms.EndDate,
        ms.Status AS MembershipStatus,
        mp.Name AS PlanName,
        mp.Price AS PlanPrice
    FROM Members m
    LEFT JOIN Memberships ms ON m.Id = ms.MemberId 
        AND ms.Status = 'Active' 
        AND ms.EndDate >= GETDATE()
    LEFT JOIN MembershipPlans mp ON ms.MembershipPlanId = mp.Id
    WHERE m.Id = @MemberId AND m.IsActive = 1;
END
GO

-- =============================================
-- SP: Get Members with Expiring Memberships
-- =============================================
CREATE OR ALTER PROCEDURE sp_GetMembersWithExpiringMemberships
    @DaysBeforeExpiration INT = 7
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        m.Id,
        m.FirstName,
        m.LastName,
        m.Email,
        m.Phone,
        ms.EndDate,
        mp.Name AS PlanName,
        DATEDIFF(DAY, GETDATE(), ms.EndDate) AS DaysUntilExpiration
    FROM Members m
    INNER JOIN Memberships ms ON m.Id = ms.MemberId
    INNER JOIN MembershipPlans mp ON ms.MembershipPlanId = mp.Id
    WHERE ms.Status = 'Active'
        AND ms.EndDate BETWEEN GETDATE() AND DATEADD(DAY, @DaysBeforeExpiration, GETDATE())
        AND m.IsActive = 1
    ORDER BY ms.EndDate ASC;
END
GO

-- =============================================
-- SP: Get Class Schedule with Bookings
-- =============================================
CREATE OR ALTER PROCEDURE sp_GetClassScheduleWithBookings
    @ClassScheduleId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        cs.*,
        c.Name AS ClassName,
        c.Description AS ClassDescription,
        c.MaxCapacity,
        t.FirstName + ' ' + t.LastName AS TrainerName,
        COUNT(cb.Id) AS TotalBookings,
        cs.AvailableSpots
    FROM ClassSchedules cs
    INNER JOIN Classes c ON cs.ClassId = c.Id
    INNER JOIN Trainers t ON c.TrainerId = t.Id
    LEFT JOIN ClassBookings cb ON cs.Id = cb.ClassScheduleId 
        AND cb.Status = 'Confirmed'
    WHERE cs.Id = @ClassScheduleId
    GROUP BY 
        cs.Id, cs.ClassId, cs.StartDateTime, cs.EndDateTime, 
        cs.Room, cs.AvailableSpots, cs.Status, cs.CreatedAt, 
        cs.UpdatedAt, cs.IsActive,
        c.Name, c.Description, c.MaxCapacity,
        t.FirstName, t.LastName;
END
GO

-- =============================================
-- SP: Create Class Booking
-- =============================================
CREATE OR ALTER PROCEDURE sp_CreateClassBooking
    @MemberId UNIQUEIDENTIFIER,
    @ClassScheduleId UNIQUEIDENTIFIER,
    @BookingId UNIQUEIDENTIFIER OUTPUT,
    @ErrorMessage NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Check if member has active membership
        IF NOT EXISTS (
            SELECT 1 FROM Memberships 
            WHERE MemberId = @MemberId 
                AND Status = 'Active' 
                AND EndDate >= GETDATE()
        )
        BEGIN
            SET @ErrorMessage = 'Member does not have an active membership';
            ROLLBACK TRANSACTION;
            RETURN -1;
        END
        
        -- Check if class has available spots
        DECLARE @AvailableSpots INT;
        SELECT @AvailableSpots = AvailableSpots 
        FROM ClassSchedules 
        WHERE Id = @ClassScheduleId;
        
        IF @AvailableSpots <= 0
        BEGIN
            SET @ErrorMessage = 'No available spots for this class';
            ROLLBACK TRANSACTION;
            RETURN -2;
        END
        
        -- Check if member already has a booking for this class
        IF EXISTS (
            SELECT 1 FROM ClassBookings 
            WHERE MemberId = @MemberId 
                AND ClassScheduleId = @ClassScheduleId 
                AND Status = 'Confirmed'
        )
        BEGIN
            SET @ErrorMessage = 'Member already has a booking for this class';
            ROLLBACK TRANSACTION;
            RETURN -3;
        END
        
        -- Create booking
        SET @BookingId = NEWID();
        INSERT INTO ClassBookings (Id, MemberId, ClassScheduleId, BookingDate, Status)
        VALUES (@BookingId, @MemberId, @ClassScheduleId, GETDATE(), 'Confirmed');
        
        -- Update available spots
        UPDATE ClassSchedules 
        SET AvailableSpots = AvailableSpots - 1,
            UpdatedAt = GETDATE()
        WHERE Id = @ClassScheduleId;
        
        COMMIT TRANSACTION;
        SET @ErrorMessage = NULL;
        RETURN 0;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @ErrorMessage = ERROR_MESSAGE();
        RETURN -99;
    END CATCH
END
GO

-- =============================================
-- SP: Get Monthly Revenue Report
-- =============================================
CREATE OR ALTER PROCEDURE sp_GetMonthlyRevenueReport
    @Year INT,
    @Month INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        CAST(PaymentDate AS DATE) AS PaymentDay,
        COUNT(*) AS TotalPayments,
        SUM(Amount) AS TotalRevenue,
        AVG(Amount) AS AveragePayment
    FROM Payments
    WHERE YEAR(PaymentDate) = @Year
        AND MONTH(PaymentDate) = @Month
        AND Status = 'Completed'
    GROUP BY CAST(PaymentDate AS DATE)
    ORDER BY PaymentDay;
    
    -- Summary
    SELECT 
        COUNT(*) AS TotalPaymentsMonth,
        SUM(Amount) AS TotalRevenueMonth,
        AVG(Amount) AS AveragePaymentMonth,
        MIN(Amount) AS MinPayment,
        MAX(Amount) AS MaxPayment
    FROM Payments
    WHERE YEAR(PaymentDate) = @Year
        AND MONTH(PaymentDate) = @Month
        AND Status = 'Completed';
END
GO

-- =============================================
-- SP: Get Member Attendance History
-- =============================================
CREATE OR ALTER PROCEDURE sp_GetMemberAttendanceHistory
    @MemberId UNIQUEIDENTIFIER,
    @FromDate DATETIME = NULL,
    @ToDate DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @FromDate IS NULL SET @FromDate = DATEADD(MONTH, -1, GETDATE());
    IF @ToDate IS NULL SET @ToDate = GETDATE();
    
    SELECT 
        a.Id,
        a.CheckInTime,
        a.CheckOutTime,
        DATEDIFF(MINUTE, a.CheckInTime, a.CheckOutTime) AS DurationMinutes,
        a.Notes
    FROM Attendances a
    WHERE a.MemberId = @MemberId
        AND a.CheckInTime BETWEEN @FromDate AND @ToDate
    ORDER BY a.CheckInTime DESC;
    
    -- Summary
    SELECT 
        COUNT(*) AS TotalVisits,
        AVG(DATEDIFF(MINUTE, CheckInTime, CheckOutTime)) AS AvgDurationMinutes
    FROM Attendances
    WHERE MemberId = @MemberId
        AND CheckInTime BETWEEN @FromDate AND @ToDate
        AND CheckOutTime IS NOT NULL;
END
GO

-- =============================================
-- SP: Get Dashboard Statistics
-- =============================================
CREATE OR ALTER PROCEDURE sp_GetDashboardStatistics
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Active Members Count
    SELECT COUNT(*) AS ActiveMembers
    FROM Members 
    WHERE IsActive = 1;
    
    -- Today's Classes
    SELECT COUNT(*) AS TodayClasses
    FROM ClassSchedules
    WHERE CAST(StartDateTime AS DATE) = CAST(GETDATE() AS DATE)
        AND Status IN ('Scheduled', 'InProgress');
    
    -- Current Month Revenue
    SELECT ISNULL(SUM(Amount), 0) AS MonthlyRevenue
    FROM Payments
    WHERE YEAR(PaymentDate) = YEAR(GETDATE())
        AND MONTH(PaymentDate) = MONTH(GETDATE())
        AND Status = 'Completed';
    
    -- Growth Rate (vs previous month)
    DECLARE @CurrentMonthRevenue DECIMAL(18,2);
    DECLARE @PreviousMonthRevenue DECIMAL(18,2);
    
    SELECT @CurrentMonthRevenue = ISNULL(SUM(Amount), 0)
    FROM Payments
    WHERE YEAR(PaymentDate) = YEAR(GETDATE())
        AND MONTH(PaymentDate) = MONTH(GETDATE())
        AND Status = 'Completed';
    
    SELECT @PreviousMonthRevenue = ISNULL(SUM(Amount), 0)
    FROM Payments
    WHERE YEAR(PaymentDate) = YEAR(DATEADD(MONTH, -1, GETDATE()))
        AND MONTH(PaymentDate) = MONTH(DATEADD(MONTH, -1, GETDATE()))
        AND Status = 'Completed';
    
    SELECT 
        CASE 
            WHEN @PreviousMonthRevenue = 0 THEN 0
            ELSE ((@CurrentMonthRevenue - @PreviousMonthRevenue) / @PreviousMonthRevenue) * 100
        END AS GrowthPercentage;
END
GO

PRINT 'Stored procedures created successfully!';
GO
