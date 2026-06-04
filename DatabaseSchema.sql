CREATE TABLE AdminUsers (
    UserId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    FullName NVARCHAR(120) NOT NULL,
    Email NVARCHAR(128) NOT NULL,
    Username NVARCHAR(60) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(256) NOT NULL,
    PasswordSalt NVARCHAR(128) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Phone NVARCHAR(30) NULL,
    IsAdmin BIT NOT NULL CONSTRAINT DF_AdminUsers_IsAdmin DEFAULT (1),
    IsActive BIT NOT NULL CONSTRAINT DF_AdminUsers_IsActive DEFAULT (1),
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_AdminUsers_CreatedAt DEFAULT (SYSDATETIME())
);

CREATE TABLE ClubMembers (
    MemberId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    FullName NVARCHAR(120) NOT NULL,
    Position NVARCHAR(120) NOT NULL,
    Department NVARCHAR(120) NULL,
    Email NVARCHAR(128) NULL,
    Phone NVARCHAR(40) NULL,
    Bio NVARCHAR(MAX) NULL,
    PhotoUrl NVARCHAR(400) NULL,
    IsActive BIT NOT NULL CONSTRAINT DF_ClubMembers_IsActive DEFAULT (1),
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_ClubMembers_CreatedAt DEFAULT (SYSDATETIME()),
    UpdatedAt DATETIME2 NULL
);

CREATE TABLE ClubEvents (
    EventId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Slug NVARCHAR(80) NOT NULL UNIQUE,
    Title NVARCHAR(160) NOT NULL,
    Tagline NVARCHAR(220) NULL,
    Summary NVARCHAR(MAX) NULL,
    EventDate NVARCHAR(40) NOT NULL,
    Format NVARCHAR(60) NOT NULL,
    Eligibility NVARCHAR(240) NULL,
    Fee NVARCHAR(40) NULL,
    PaymentNote NVARCHAR(240) NULL,
    Guidelines NVARCHAR(MAX) NULL,
    BackgroundUrl NVARCHAR(400) NULL,
    IsUpcoming BIT NOT NULL CONSTRAINT DF_ClubEvents_IsUpcoming DEFAULT (0),
    IsActive BIT NOT NULL CONSTRAINT DF_ClubEvents_IsActive DEFAULT (1),
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_ClubEvents_CreatedAt DEFAULT (SYSDATETIME()),
    UpdatedAt DATETIME2 NULL
);IF OBJECT_ID('dbo.AdminUsers', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.AdminUsers
    (
        UserId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        FullName NVARCHAR(120) NOT NULL,
        Email NVARCHAR(120) NOT NULL,
        Username NVARCHAR(60) NOT NULL UNIQUE,
        PasswordHash NVARCHAR(256) NOT NULL,
        PasswordSalt NVARCHAR(256) NOT NULL,
        DateOfBirth DATE NOT NULL,
        Phone NVARCHAR(30) NULL,
        IsActive BIT NOT NULL CONSTRAINT DF_AdminUsers_IsActive DEFAULT (1),
        IsAdmin BIT NOT NULL CONSTRAINT DF_AdminUsers_IsAdmin DEFAULT (1),
        CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_AdminUsers_CreatedAt DEFAULT (SYSDATETIME())
    );
END;
GO

IF OBJECT_ID('dbo.ClubMembers', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.ClubMembers
    (
        MemberId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        FullName NVARCHAR(120) NOT NULL,
        Email NVARCHAR(120) NULL,
        Phone NVARCHAR(30) NULL,
        Department NVARCHAR(80) NULL,
        Position NVARCHAR(80) NOT NULL,
        Bio NVARCHAR(MAX) NULL,
        PhotoUrl NVARCHAR(400) NULL,
        IsActive BIT NOT NULL CONSTRAINT DF_ClubMembers_IsActive DEFAULT (1),
        CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_ClubMembers_CreatedAt DEFAULT (SYSDATETIME()),
        UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_ClubMembers_UpdatedAt DEFAULT (SYSDATETIME())
    );
END;
GO

IF OBJECT_ID('dbo.ClubEvents', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.ClubEvents
    (
        EventId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Slug NVARCHAR(80) NOT NULL UNIQUE,
        Title NVARCHAR(180) NOT NULL,
        EventDate DATE NOT NULL,
        Tagline NVARCHAR(220) NULL,
        Summary NVARCHAR(MAX) NULL,
        Format NVARCHAR(80) NULL,
        Eligibility NVARCHAR(260) NULL,
        Fee NVARCHAR(60) NULL,
        PaymentNote NVARCHAR(260) NULL,
        Guidelines NVARCHAR(MAX) NULL,
        BackgroundUrl NVARCHAR(400) NULL,
        Venue NVARCHAR(160) NULL,
        IsUpcoming BIT NOT NULL CONSTRAINT DF_ClubEvents_IsUpcoming DEFAULT (1),
        IsActive BIT NOT NULL CONSTRAINT DF_ClubEvents_IsActive DEFAULT (1),
        CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_ClubEvents_CreatedAt DEFAULT (SYSDATETIME()),
        UpdatedAt DATETIME2 NOT NULL CONSTRAINT DF_ClubEvents_UpdatedAt DEFAULT (SYSDATETIME())
    );
END;
GO
