-- =============================================
-- MindFit Intelligence - Triggers
-- =============================================

USE MindFitDB;
GO

-- =============================================
-- Trigger: Update Membership Status on Expiration
-- =============================================
CREATE OR ALTER TRIGGER trg_UpdateExpiredMemberships
ON Memberships
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE ms
    SET Status = 'Expired',
        UpdatedAt = GETDATE()
    FROM Memberships ms
    INNER JOIN inserted i ON ms.Id = i.Id
    WHERE ms.EndDate < GETDATE()
        AND ms.Status = 'Active';
END
GO

-- =============================================
-- Trigger: Prevent Overbooking
-- =============================================
CREATE OR ALTER TRIGGER trg_PreventOverbooking
ON ClassBookings
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @ClassScheduleId UNIQUEIDENTIFIER;
    DECLARE @AvailableSpots INT;
    
    SELECT @ClassScheduleId = ClassScheduleId
    FROM inserted;
    
    SELECT @AvailableSpots = AvailableSpots
    FROM ClassSchedules
    WHERE Id = @ClassScheduleId;
    
    IF @AvailableSpots > 0
    BEGIN
        INSERT INTO ClassBookings (Id, MemberId, ClassScheduleId, BookingDate, Status, CreatedAt, IsActive)
        SELECT Id, MemberId, ClassScheduleId, BookingDate, Status, GETDATE(), IsActive
        FROM inserted;
        
        UPDATE ClassSchedules
        SET AvailableSpots = AvailableSpots - 1,
            UpdatedAt = GETDATE()
        WHERE Id = @ClassScheduleId;
    END
    ELSE
    BEGIN
        RAISERROR ('No available spots for this class', 16, 1);
    END
END
GO

-- =============================================
-- Trigger: Update Attendance Duration
-- =============================================
CREATE OR ALTER TRIGGER trg_UpdateAttendanceDuration
ON Attendances
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    IF UPDATE(CheckOutTime)
    BEGIN
        UPDATE a
        SET UpdatedAt = GETDATE()
        FROM Attendances a
        INNER JOIN inserted i ON a.Id = i.Id
        WHERE i.CheckOutTime IS NOT NULL;
    END
END
GO

-- =============================================
-- Trigger: Auto-update UpdatedAt column
-- =============================================
CREATE OR ALTER TRIGGER trg_Members_UpdatedAt
ON Members
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE Members
    SET UpdatedAt = GETDATE()
    FROM Members m
    INNER JOIN inserted i ON m.Id = i.Id;
END
GO

CREATE OR ALTER TRIGGER trg_Trainers_UpdatedAt
ON Trainers
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE Trainers
    SET UpdatedAt = GETDATE()
    FROM Trainers t
    INNER JOIN inserted i ON t.Id = i.Id;
END
GO

PRINT 'Triggers created successfully!';
GO
