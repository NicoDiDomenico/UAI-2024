-- =============================================
-- MindFit Intelligence - Database Schema
-- SQL Server Database Creation Script
-- =============================================

USE master;
GO

-- Drop database if exists
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'MindFitDB')
BEGIN
    ALTER DATABASE MindFitDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE MindFitDB;
END
GO

-- Create database
CREATE DATABASE MindFitDB;
GO

USE MindFitDB;
GO

-- =============================================
-- Table: Members
-- =============================================
CREATE TABLE Members (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(200) NOT NULL UNIQUE,
    Phone NVARCHAR(20) NOT NULL,
    Address NVARCHAR(500) NOT NULL,
    DateOfBirth DATE NOT NULL,
    EmergencyContact NVARCHAR(100) NOT NULL,
    EmergencyPhone NVARCHAR(20) NOT NULL,
    ProfilePhoto NVARCHAR(500) NULL,
    MemberSince DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    INDEX IX_Members_Email (Email),
    INDEX IX_Members_IsActive (IsActive),
    INDEX IX_Members_MemberSince (MemberSince)
);
GO

-- =============================================
-- Table: MembershipPlans
-- =============================================
CREATE TABLE MembershipPlans (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    DurationDays INT NOT NULL,
    MaxClasses INT NOT NULL,
    HasPersonalTrainer BIT NOT NULL DEFAULT 0,
    HasNutritionist BIT NOT NULL DEFAULT 0,
    Benefits NVARCHAR(MAX) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    INDEX IX_MembershipPlans_IsActive (IsActive)
);
GO

-- =============================================
-- Table: Memberships
-- =============================================
CREATE TABLE Memberships (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    MemberId UNIQUEIDENTIFIER NOT NULL,
    MembershipPlanId UNIQUEIDENTIFIER NOT NULL,
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',
    AutoRenewal BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Memberships_Members FOREIGN KEY (MemberId) REFERENCES Members(Id),
    CONSTRAINT FK_Memberships_MembershipPlans FOREIGN KEY (MembershipPlanId) REFERENCES MembershipPlans(Id),
    CONSTRAINT CHK_Memberships_Status CHECK (Status IN ('Active', 'Expired', 'Suspended', 'Cancelled')),
    INDEX IX_Memberships_MemberId (MemberId),
    INDEX IX_Memberships_Status (Status),
    INDEX IX_Memberships_EndDate (EndDate)
);
GO

-- =============================================
-- Table: Trainers
-- =============================================
CREATE TABLE Trainers (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(200) NOT NULL UNIQUE,
    Phone NVARCHAR(20) NOT NULL,
    Specialization NVARCHAR(200) NOT NULL,
    Certifications NVARCHAR(MAX) NULL,
    Bio NVARCHAR(MAX) NULL,
    ProfilePhoto NVARCHAR(500) NULL,
    HireDate DATETIME NOT NULL,
    HourlyRate DECIMAL(18,2) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    INDEX IX_Trainers_Email (Email),
    INDEX IX_Trainers_IsActive (IsActive)
);
GO

-- =============================================
-- Table: Classes
-- =============================================
CREATE TABLE Classes (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    TrainerId UNIQUEIDENTIFIER NOT NULL,
    MaxCapacity INT NOT NULL,
    Duration INT NOT NULL, -- Duration in minutes
    Category NVARCHAR(100) NOT NULL,
    ImageUrl NVARCHAR(500) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Classes_Trainers FOREIGN KEY (TrainerId) REFERENCES Trainers(Id),
    INDEX IX_Classes_TrainerId (TrainerId),
    INDEX IX_Classes_Category (Category),
    INDEX IX_Classes_IsActive (IsActive)
);
GO

-- =============================================
-- Table: ClassSchedules
-- =============================================
CREATE TABLE ClassSchedules (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ClassId UNIQUEIDENTIFIER NOT NULL,
    StartDateTime DATETIME NOT NULL,
    EndDateTime DATETIME NOT NULL,
    Room NVARCHAR(100) NOT NULL,
    AvailableSpots INT NOT NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT 'Scheduled',
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_ClassSchedules_Classes FOREIGN KEY (ClassId) REFERENCES Classes(Id),
    CONSTRAINT CHK_ClassSchedules_Status CHECK (Status IN ('Scheduled', 'InProgress', 'Completed', 'Cancelled')),
    INDEX IX_ClassSchedules_ClassId (ClassId),
    INDEX IX_ClassSchedules_StartDateTime (StartDateTime),
    INDEX IX_ClassSchedules_Status (Status)
);
GO

-- =============================================
-- Table: ClassBookings
-- =============================================
CREATE TABLE ClassBookings (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    MemberId UNIQUEIDENTIFIER NOT NULL,
    ClassScheduleId UNIQUEIDENTIFIER NOT NULL,
    BookingDate DATETIME NOT NULL DEFAULT GETDATE(),
    Status NVARCHAR(50) NOT NULL DEFAULT 'Confirmed',
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_ClassBookings_Members FOREIGN KEY (MemberId) REFERENCES Members(Id),
    CONSTRAINT FK_ClassBookings_ClassSchedules FOREIGN KEY (ClassScheduleId) REFERENCES ClassSchedules(Id),
    CONSTRAINT CHK_ClassBookings_Status CHECK (Status IN ('Confirmed', 'Cancelled', 'Attended', 'NoShow')),
    INDEX IX_ClassBookings_MemberId (MemberId),
    INDEX IX_ClassBookings_ClassScheduleId (ClassScheduleId),
    INDEX IX_ClassBookings_Status (Status)
);
GO

-- =============================================
-- Table: Payments
-- =============================================
CREATE TABLE Payments (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    MemberId UNIQUEIDENTIFIER NOT NULL,
    MembershipId UNIQUEIDENTIFIER NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    PaymentDate DATETIME NOT NULL DEFAULT GETDATE(),
    PaymentMethod NVARCHAR(50) NOT NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',
    TransactionId NVARCHAR(200) NULL,
    Notes NVARCHAR(MAX) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Payments_Members FOREIGN KEY (MemberId) REFERENCES Members(Id),
    CONSTRAINT FK_Payments_Memberships FOREIGN KEY (MembershipId) REFERENCES Memberships(Id),
    CONSTRAINT CHK_Payments_Status CHECK (Status IN ('Pending', 'Completed', 'Failed', 'Refunded')),
    INDEX IX_Payments_MemberId (MemberId),
    INDEX IX_Payments_PaymentDate (PaymentDate),
    INDEX IX_Payments_Status (Status)
);
GO

-- =============================================
-- Table: Attendances
-- =============================================
CREATE TABLE Attendances (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    MemberId UNIQUEIDENTIFIER NOT NULL,
    CheckInTime DATETIME NOT NULL DEFAULT GETDATE(),
    CheckOutTime DATETIME NULL,
    Notes NVARCHAR(MAX) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Attendances_Members FOREIGN KEY (MemberId) REFERENCES Members(Id),
    INDEX IX_Attendances_MemberId (MemberId),
    INDEX IX_Attendances_CheckInTime (CheckInTime)
);
GO

-- =============================================
-- Table: Users
-- =============================================
CREATE TABLE Users (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Username NVARCHAR(100) NOT NULL UNIQUE,
    Email NVARCHAR(200) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(500) NOT NULL,
    Role NVARCHAR(50) NOT NULL DEFAULT 'Staff',
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    LastLogin DATETIME NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT CHK_Users_Role CHECK (Role IN ('Admin', 'Staff', 'Trainer')),
    INDEX IX_Users_Username (Username),
    INDEX IX_Users_Email (Email),
    INDEX IX_Users_Role (Role)
);
GO

PRINT 'Database schema created successfully!';
GO
