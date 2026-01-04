-- =============================================
-- MindFit Intelligence - Sample Data
-- =============================================

USE MindFitDB;
GO

-- =============================================
-- Insert Sample Membership Plans
-- =============================================
INSERT INTO MembershipPlans (Id, Name, Description, Price, DurationDays, MaxClasses, HasPersonalTrainer, HasNutritionist, Benefits)
VALUES 
    (NEWID(), 'Plan Básico', 'Acceso ilimitado al gimnasio con equipamiento básico', 29.99, 30, 999, 0, 0, 'Acceso a equipamiento básico, Uso de vestuarios'),
    (NEWID(), 'Plan Premium', 'Incluye acceso completo más clases grupales ilimitadas', 49.99, 30, 999, 0, 0, 'Acceso completo, Clases grupales ilimitadas, Uso de sauna'),
    (NEWID(), 'Plan Elite', 'Todo incluido con entrenador personal y nutricionista', 89.99, 30, 999, 1, 1, 'Acceso completo, Clases ilimitadas, Entrenador personal, Nutricionista, Sauna, Masajes');
GO

-- =============================================
-- Insert Sample Members
-- =============================================
DECLARE @Member1 UNIQUEIDENTIFIER = NEWID();
DECLARE @Member2 UNIQUEIDENTIFIER = NEWID();
DECLARE @Member3 UNIQUEIDENTIFIER = NEWID();
DECLARE @Member4 UNIQUEIDENTIFIER = NEWID();
DECLARE @Member5 UNIQUEIDENTIFIER = NEWID();

INSERT INTO Members (Id, FirstName, LastName, Email, Phone, Address, DateOfBirth, EmergencyContact, EmergencyPhone, MemberSince)
VALUES 
    (@Member1, 'Juan', 'Pérez', 'juan.perez@email.com', '555-0101', 'Calle Principal 123', '1990-05-15', 'María Pérez', '555-0102', DATEADD(MONTH, -6, GETDATE())),
    (@Member2, 'Ana', 'García', 'ana.garcia@email.com', '555-0201', 'Avenida Central 456', '1985-08-22', 'Carlos García', '555-0202', DATEADD(MONTH, -3, GETDATE())),
    (@Member3, 'Pedro', 'Martínez', 'pedro.martinez@email.com', '555-0301', 'Plaza Mayor 789', '1992-11-30', 'Laura Martínez', '555-0302', DATEADD(MONTH, -12, GETDATE())),
    (@Member4, 'Laura', 'López', 'laura.lopez@email.com', '555-0401', 'Calle Secundaria 321', '1988-03-10', 'Miguel López', '555-0402', DATEADD(MONTH, -2, GETDATE())),
    (@Member5, 'Carlos', 'Rodríguez', 'carlos.rodriguez@email.com', '555-0501', 'Avenida Norte 654', '1995-07-18', 'Sofía Rodríguez', '555-0502', DATEADD(MONTH, -9, GETDATE()));
GO

-- =============================================
-- Insert Sample Trainers
-- =============================================
DECLARE @Trainer1 UNIQUEIDENTIFIER = NEWID();
DECLARE @Trainer2 UNIQUEIDENTIFIER = NEWID();
DECLARE @Trainer3 UNIQUEIDENTIFIER = NEWID();

INSERT INTO Trainers (Id, FirstName, LastName, Email, Phone, Specialization, Certifications, HireDate, HourlyRate)
VALUES 
    (@Trainer1, 'Roberto', 'Sánchez', 'roberto.sanchez@mindfit.com', '555-1001', 'Entrenamiento Funcional', 'Certificación NSCA-CPT, CrossFit Level 1', DATEADD(YEAR, -2, GETDATE()), 35.00),
    (@Trainer2, 'Carmen', 'Flores', 'carmen.flores@mindfit.com', '555-1002', 'Yoga y Pilates', 'RYT-200, Pilates Instructor Certificate', DATEADD(YEAR, -3, GETDATE()), 30.00),
    (@Trainer3, 'Miguel', 'Torres', 'miguel.torres@mindfit.com', '555-1003', 'Culturismo y Fuerza', 'ISSA Bodybuilding Specialist, ACE-CPT', DATEADD(YEAR, -1, GETDATE()), 40.00);
GO

-- =============================================
-- Insert Sample Classes
-- =============================================
DECLARE @Class1 UNIQUEIDENTIFIER = NEWID();
DECLARE @Class2 UNIQUEIDENTIFIER = NEWID();
DECLARE @Class3 UNIQUEIDENTIFIER = NEWID();
DECLARE @Class4 UNIQUEIDENTIFIER = NEWID();

INSERT INTO Classes (Id, Name, Description, TrainerId, MaxCapacity, Duration, Category)
VALUES 
    (@Class1, 'Yoga Matutino', 'Clase de yoga suave para comenzar el día con energía', (SELECT TOP 1 Id FROM Trainers WHERE FirstName = 'Carmen'), 20, 60, 'Yoga'),
    (@Class2, 'CrossFit Intensivo', 'Entrenamiento de alta intensidad para todos los niveles', (SELECT TOP 1 Id FROM Trainers WHERE FirstName = 'Roberto'), 15, 45, 'Cardio'),
    (@Class3, 'Pilates Reformer', 'Fortalecimiento del core con equipamiento especializado', (SELECT TOP 1 Id FROM Trainers WHERE FirstName = 'Carmen'), 12, 50, 'Pilates'),
    (@Class4, 'Entrenamiento de Fuerza', 'Desarrollo muscular y técnica de levantamiento', (SELECT TOP 1 Id FROM Trainers WHERE FirstName = 'Miguel'), 10, 60, 'Strength');
GO

-- =============================================
-- Insert Sample Class Schedules
-- =============================================
INSERT INTO ClassSchedules (ClassId, StartDateTime, EndDateTime, Room, AvailableSpots, Status)
VALUES 
    (@Class1, DATEADD(HOUR, 8, CAST(GETDATE() AS DATE)), DATEADD(HOUR, 9, CAST(GETDATE() AS DATE)), 'Sala A', 15, 'Scheduled'),
    (@Class2, DATEADD(HOUR, 10, CAST(GETDATE() AS DATE)), DATEADD(HOUR, 10, CAST(GETDATE() AS DATE)) + 45, 'Sala B', 12, 'Scheduled'),
    (@Class3, DATEADD(HOUR, 14, CAST(GETDATE() AS DATE)), DATEADD(HOUR, 14, CAST(GETDATE() AS DATE)) + 50, 'Sala A', 10, 'Scheduled'),
    (@Class4, DATEADD(HOUR, 18, CAST(GETDATE() AS DATE)), DATEADD(HOUR, 19, CAST(GETDATE() AS DATE)), 'Sala C', 8, 'Scheduled');
GO

-- =============================================
-- Insert Sample Memberships
-- =============================================
DECLARE @PlanPremium UNIQUEIDENTIFIER = (SELECT TOP 1 Id FROM MembershipPlans WHERE Name = 'Plan Premium');
DECLARE @PlanBasico UNIQUEIDENTIFIER = (SELECT TOP 1 Id FROM MembershipPlans WHERE Name = 'Plan Básico');

INSERT INTO Memberships (MemberId, MembershipPlanId, StartDate, EndDate, Status, AutoRenewal)
SELECT Id, @PlanPremium, GETDATE(), DATEADD(DAY, 30, GETDATE()), 'Active', 1
FROM Members
WHERE FirstName IN ('Juan', 'Ana', 'Pedro');

INSERT INTO Memberships (MemberId, MembershipPlanId, StartDate, EndDate, Status, AutoRenewal)
SELECT Id, @PlanBasico, GETDATE(), DATEADD(DAY, 30, GETDATE()), 'Active', 0
FROM Members
WHERE FirstName IN ('Laura', 'Carlos');
GO

-- =============================================
-- Insert Sample Payments
-- =============================================
INSERT INTO Payments (MemberId, MembershipId, Amount, PaymentDate, PaymentMethod, Status)
SELECT 
    ms.MemberId,
    ms.Id,
    mp.Price,
    GETDATE(),
    'Card',
    'Completed'
FROM Memberships ms
INNER JOIN MembershipPlans mp ON ms.MembershipPlanId = mp.Id
WHERE ms.Status = 'Active';
GO

-- =============================================
-- Insert Sample Attendances
-- =============================================
INSERT INTO Attendances (MemberId, CheckInTime, CheckOutTime)
SELECT TOP 20
    m.Id,
    DATEADD(HOUR, -FLOOR(RAND() * 72), GETDATE()),
    DATEADD(HOUR, -FLOOR(RAND() * 72) + 2, GETDATE())
FROM Members m
CROSS JOIN (SELECT TOP 4 1 AS n FROM sys.objects) AS nums
WHERE m.IsActive = 1
ORDER BY NEWID();
GO

-- =============================================
-- Insert Sample Users
-- =============================================
INSERT INTO Users (Username, Email, PasswordHash, Role, FirstName, LastName)
VALUES 
    ('admin', 'admin@mindfit.com', 'HASHED_PASSWORD_HERE', 'Admin', 'Admin', 'System'),
    ('staff01', 'staff01@mindfit.com', 'HASHED_PASSWORD_HERE', 'Staff', 'María', 'González'),
    ('trainer01', 'roberto.sanchez@mindfit.com', 'HASHED_PASSWORD_HERE', 'Trainer', 'Roberto', 'Sánchez');
GO

PRINT 'Sample data inserted successfully!';
GO
