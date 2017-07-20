
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/19/2017 11:21:25
-- Generated from EDMX file: D:\Junaid\Github\Chatbot\Chatbot\Chatbot.edmx
-- --------------------------------------------------

USE master
GO

--Create a database
IF EXISTS(SELECT name FROM sys.databases
    WHERE name = 'Chatbot')
    DROP DATABASE Chatbot
GO

CREATE DATABASE Chatbot
GO

SET QUOTED_IDENTIFIER OFF;
GO
USE [Chatbot];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserChat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Chats] DROP CONSTRAINT [FK_UserChat];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Chats]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Chats];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Username] nvarchar(max)  NULL,
    [Password] nvarchar(max)  NULL,
    [Email] nvarchar(max)  NULL,
    [Role] nvarchar(max)  NULL,
    [Age] bigint  NULL
);
GO

-- Creating table 'Chats'
CREATE TABLE [dbo].[Chats] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Message] nvarchar(max)  NULL,
    [IsFromUser] bit  NULL,
    [UserId] bigint  NOT NULL
);
GO

-- Creating table 'Patients'
CREATE TABLE [dbo].[Patients] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Disease] nvarchar(max)  NULL,
    [User_Id] bigint  NOT NULL
);
GO

-- Creating table 'Doctors'
CREATE TABLE [dbo].[Doctors] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Specialization] nvarchar(max)  NULL,
    [LastDegree] nvarchar(max)  NULL,
    [User_Id] bigint  NOT NULL
);
GO

-- Creating table 'Messages'
CREATE TABLE [dbo].[Messages] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Text] nvarchar(max)  NULL,
    [FromPatient] bit  NULL,
    [PatientId] bigint  NOT NULL,
    [DoctorId] bigint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Chats'
ALTER TABLE [dbo].[Chats]
ADD CONSTRAINT [PK_Chats]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Patients'
ALTER TABLE [dbo].[Patients]
ADD CONSTRAINT [PK_Patients]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Doctors'
ALTER TABLE [dbo].[Doctors]
ADD CONSTRAINT [PK_Doctors]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [PK_Messages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'Chats'
ALTER TABLE [dbo].[Chats]
ADD CONSTRAINT [FK_UserChat]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserChat'
CREATE INDEX [IX_FK_UserChat]
ON [dbo].[Chats]
    ([UserId]);
GO

-- Creating foreign key on [User_Id] in table 'Patients'
ALTER TABLE [dbo].[Patients]
ADD CONSTRAINT [FK_UserPatient]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserPatient'
CREATE INDEX [IX_FK_UserPatient]
ON [dbo].[Patients]
    ([User_Id]);
GO

-- Creating foreign key on [User_Id] in table 'Doctors'
ALTER TABLE [dbo].[Doctors]
ADD CONSTRAINT [FK_UserDoctor]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDoctor'
CREATE INDEX [IX_FK_UserDoctor]
ON [dbo].[Doctors]
    ([User_Id]);
GO

-- Creating foreign key on [PatientId] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_PatientMessage]
    FOREIGN KEY ([PatientId])
    REFERENCES [dbo].[Patients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientMessage'
CREATE INDEX [IX_FK_PatientMessage]
ON [dbo].[Messages]
    ([PatientId]);
GO

-- Creating foreign key on [DoctorId] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_DoctorMessage]
    FOREIGN KEY ([DoctorId])
    REFERENCES [dbo].[Doctors]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DoctorMessage'
CREATE INDEX [IX_FK_DoctorMessage]
ON [dbo].[Messages]
    ([DoctorId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------