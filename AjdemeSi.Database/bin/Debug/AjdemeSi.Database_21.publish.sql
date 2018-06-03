﻿/*
Deployment script for AjdemeSi

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "AjdemeSi"
:setvar DefaultFilePrefix "AjdemeSi"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
/*
The column [dbo].[Car].[DriverId] is being dropped, data loss could occur.

The column [dbo].[Car].[UserId] on table [dbo].[Car] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[Car])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
/*
The column [dbo].[Driver].[Id] is being dropped, data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[Driver])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping [dbo].[FK_dbo.Car_dbo.Driver_UserId]...';


GO
ALTER TABLE [dbo].[Car] DROP CONSTRAINT [FK_dbo.Car_dbo.Driver_UserId];


GO
PRINT N'Dropping [dbo].[FK_dbo.Driver_dbo.AspNetUsers_UserId]...';


GO
ALTER TABLE [dbo].[Driver] DROP CONSTRAINT [FK_dbo.Driver_dbo.AspNetUsers_UserId];


GO
PRINT N'Starting rebuilding table [dbo].[Car]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Car] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [UserId]               NVARCHAR (128) NOT NULL,
    [Make]                 NVARCHAR (128) NOT NULL,
    [Model]                NVARCHAR (128) NOT NULL,
    [YearOfManufacture]    INT            NOT NULL,
    [SiteRegistrationDate] DATETIME       NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_dbo.Car1] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Car])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Car] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Car] ([Id], [Make], [Model], [YearOfManufacture], [SiteRegistrationDate])
        SELECT   [Id],
                 [Make],
                 [Model],
                 [YearOfManufacture],
                 [SiteRegistrationDate]
        FROM     [dbo].[Car]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Car] OFF;
    END

DROP TABLE [dbo].[Car];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Car]', N'Car';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_dbo.Car1]', N'PK_dbo.Car', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[Driver]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Driver] (
    [UserId]                  NVARCHAR (128) NOT NULL,
    [LicenceRegistrationDate] NVARCHAR (128) NOT NULL,
    [SiteRegistrationDate]    DATETIME       NOT NULL,
    UNIQUE NONCLUSTERED ([UserId] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_PK_dbo.Driver1] PRIMARY KEY CLUSTERED ([UserId] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Driver])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_Driver] ([UserId], [LicenceRegistrationDate], [SiteRegistrationDate])
        SELECT   [UserId],
                 [LicenceRegistrationDate],
                 [SiteRegistrationDate]
        FROM     [dbo].[Driver]
        ORDER BY [UserId] ASC;
    END

DROP TABLE [dbo].[Driver];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Driver]', N'Driver';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_dbo.Driver1]', N'PK_dbo.Driver', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[FK_dbo.Car_dbo.Driver_UserId]...';


GO
ALTER TABLE [dbo].[Car] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Car_dbo.Driver_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Driver] ([UserId]) ON DELETE CASCADE;


GO
PRINT N'Creating [dbo].[FK_dbo.Driver_dbo.AspNetUsers_UserId]...';


GO
ALTER TABLE [dbo].[Driver] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.Driver_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE;


GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

MERGE INTO AspNetRoles AS TARGET USING (VALUES 
('e6e0d6ae-ee09-4b5f-a46a-c91a021ec148', N'User'),
('ccfa943c-ee4d-48df-b9c8-a3941e576dd9', N'Driver'),
('c81fd2e2-756b-4197-9ad0-9061d30e952f', N'Admin')) AS Source (Id, Name)
ON TARGET.Id = Source.Id
--	update matched rows 
WHEN MATCHED THEN 
UPDATE SET Name = Source.Name 
--	insert new rows 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Id, Name) 
VALUES (Id, Name);
GO

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Car] WITH CHECK CHECK CONSTRAINT [FK_dbo.Car_dbo.Driver_UserId];

ALTER TABLE [dbo].[Driver] WITH CHECK CHECK CONSTRAINT [FK_dbo.Driver_dbo.AspNetUsers_UserId];


GO
PRINT N'Update complete.';


GO
