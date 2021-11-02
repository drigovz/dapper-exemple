USE [master]
GO

IF NOT EXISTS (
    SELECT [name]
        FROM sys.databases
        WHERE [name] = N'CustomersDb'
)
CREATE DATABASE CustomersDb
GO

USE [CustomersDb]
GO

IF OBJECT_ID('[dbo].[Users]', 'U') IS NOT NULL
DROP TABLE [dbo].[Users]
GO

CREATE TABLE [dbo].[Users]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(70) NOT NULL,
    [Email] VARCHAR(100) NOT NULL,
    [Genre] CHAR(1) NULL,
    [RG] VARCHAR(15) NULL,
    [CPF] CHAR(14) NULL,
    [MotherName] VARCHAR(70) NULL,
    [Status] CHAR(1) NOT NULL,
    [CreatedAt] DATETIMEOFFSET NOT NULL,

    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

/*One-To-One*/
IF OBJECT_ID('[dbo].[Contacts]', 'U') IS NOT NULL
DROP TABLE [dbo].[Contacts]
GO

CREATE TABLE [dbo].[Contacts]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [UserId] INT NOT NULL,
    [Phone] VARCHAR(15) NULL,
    [Cellphone] VARCHAR(15) NULL,

    CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Contacts_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);
GO

/*One-To-Many*/
IF OBJECT_ID('[dbo].[Address]', 'U') IS NOT NULL
DROP TABLE [dbo].[Address]
GO

CREATE TABLE [dbo].[Address]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [UserId] INT NOT NULL,
    [AddressName] VARCHAR(100) NOT NULL,
    [Zipcode] CHAR(10) NOT NULL,
    [State] CHAR(2) NOT NULL,
    [City] VARCHAR(120) NOT NULL,
    [Neighborhood] VARCHAR(200) NOT NULL,
    [Street] VARCHAR(200) NOT NULL,
    [Number] VARCHAR(20) NULL,
    [Complement] VARCHAR(30) NULL,

    CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Address_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE

);
GO

/*Many-To-Many*/
IF OBJECT_ID('[dbo].[Departments]', 'U') IS NOT NULL
DROP TABLE [dbo].[Departments]
GO

CREATE TABLE [dbo].[Departments]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(100) NOT NULL,
    CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED ([Id] ASC),
);
GO

IF OBJECT_ID('[dbo].[UsersDepartments]', 'U') IS NOT NULL
DROP TABLE [dbo].[UsersDepartments]
GO

CREATE TABLE [dbo].[UsersDepartments]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [UserId] INT NOT NULL,
    [DepartmentId] INT NOT NULL,

    CONSTRAINT [PK_UsersDepartments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UsersDepartments_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UsersDepartments_Departments] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Departments] ([Id]) ON DELETE CASCADE
);
GO