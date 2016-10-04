
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/04/2016 19:52:59
-- Generated from EDMX file: C:\PowCamp\PowCamp\PowCampDatabaseModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PowCampDatabase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_GameObjectGameObjectType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GameObjects] DROP CONSTRAINT [FK_GameObjectGameObjectType];
GO
IF OBJECT_ID(N'[dbo].[FK_InstantiatedGameObjectGameObject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InstantiatedGameObjects] DROP CONSTRAINT [FK_InstantiatedGameObjectGameObject];
GO
IF OBJECT_ID(N'[dbo].[FK_ScreenCoordGameObject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ScreenCoord] DROP CONSTRAINT [FK_ScreenCoordGameObject];
GO
IF OBJECT_ID(N'[dbo].[FK_GameObjectCellCoord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CellCoord] DROP CONSTRAINT [FK_GameObjectCellCoord];
GO
IF OBJECT_ID(N'[dbo].[FK_SceneSaveGame]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SaveGames] DROP CONSTRAINT [FK_SceneSaveGame];
GO
IF OBJECT_ID(N'[dbo].[FK_LevelScene]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Levels] DROP CONSTRAINT [FK_LevelScene];
GO
IF OBJECT_ID(N'[dbo].[FK_InstantiatedGameObjectScene]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InstantiatedGameObjects] DROP CONSTRAINT [FK_InstantiatedGameObjectScene];
GO
IF OBJECT_ID(N'[dbo].[FK_CurrentAnimationGameObject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CurrentAnimations] DROP CONSTRAINT [FK_CurrentAnimationGameObject];
GO
IF OBJECT_ID(N'[dbo].[FK_CurrentAnimationAnimation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CurrentAnimations] DROP CONSTRAINT [FK_CurrentAnimationAnimation];
GO
IF OBJECT_ID(N'[dbo].[FK_VelocityGameObject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Velocities] DROP CONSTRAINT [FK_VelocityGameObject];
GO
IF OBJECT_ID(N'[dbo].[FK_AccelerationGameObject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Accelerations] DROP CONSTRAINT [FK_AccelerationGameObject];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[GameObjects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GameObjects];
GO
IF OBJECT_ID(N'[dbo].[GameObjectTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GameObjectTypes];
GO
IF OBJECT_ID(N'[dbo].[InstantiatedGameObjects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InstantiatedGameObjects];
GO
IF OBJECT_ID(N'[dbo].[ScreenCoord]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ScreenCoord];
GO
IF OBJECT_ID(N'[dbo].[CellCoord]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CellCoord];
GO
IF OBJECT_ID(N'[dbo].[Scenes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Scenes];
GO
IF OBJECT_ID(N'[dbo].[ComponentDependencies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ComponentDependencies];
GO
IF OBJECT_ID(N'[dbo].[Levels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Levels];
GO
IF OBJECT_ID(N'[dbo].[SaveGames]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SaveGames];
GO
IF OBJECT_ID(N'[dbo].[CurrentAnimations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CurrentAnimations];
GO
IF OBJECT_ID(N'[dbo].[Animations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Animations];
GO
IF OBJECT_ID(N'[dbo].[Velocities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Velocities];
GO
IF OBJECT_ID(N'[dbo].[Accelerations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accelerations];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'GameObjects'
CREATE TABLE [dbo].[GameObjects] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GameObjectType_Id] int  NOT NULL
);
GO

-- Creating table 'GameObjectTypes'
CREATE TABLE [dbo].[GameObjectTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [enumValue] int  NOT NULL
);
GO

-- Creating table 'InstantiatedGameObjects'
CREATE TABLE [dbo].[InstantiatedGameObjects] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GameObject_Id] int  NOT NULL,
    [Scene_Id] int  NOT NULL
);
GO

-- Creating table 'ScreenCoord'
CREATE TABLE [dbo].[ScreenCoord] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [x] float  NOT NULL,
    [y] float  NOT NULL,
    [GameObject_Id] int  NOT NULL
);
GO

-- Creating table 'CellCoord'
CREATE TABLE [dbo].[CellCoord] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [x] float  NOT NULL,
    [y] float  NOT NULL,
    [GameObject_Id] int  NOT NULL
);
GO

-- Creating table 'Scenes'
CREATE TABLE [dbo].[Scenes] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'ComponentDependencies'
CREATE TABLE [dbo].[ComponentDependencies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [componentName] nvarchar(max)  NOT NULL,
    [dependsOn] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Levels'
CREATE TABLE [dbo].[Levels] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [Scene_Id] int  NOT NULL
);
GO

-- Creating table 'SaveGames'
CREATE TABLE [dbo].[SaveGames] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [levelCreatedFrom] int  NOT NULL,
    [Scene_Id] int  NOT NULL
);
GO

-- Creating table 'CurrentAnimations'
CREATE TABLE [dbo].[CurrentAnimations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [index] int  NOT NULL,
    [GameObject_Id] int  NOT NULL,
    [Animation_Id] int  NOT NULL
);
GO

-- Creating table 'Animations'
CREATE TABLE [dbo].[Animations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [atlasName] nvarchar(max)  NOT NULL,
    [frameWidth] int  NOT NULL,
    [frameHeight] int  NOT NULL,
    [startX] int  NOT NULL,
    [startY] int  NOT NULL,
    [enumValue] int  NOT NULL
);
GO

-- Creating table 'Velocities'
CREATE TABLE [dbo].[Velocities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [x] float  NOT NULL,
    [y] float  NOT NULL,
    [GameObject_Id] int  NOT NULL
);
GO

-- Creating table 'Accelerations'
CREATE TABLE [dbo].[Accelerations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [x] float  NOT NULL,
    [y] float  NOT NULL,
    [GameObject_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'GameObjects'
ALTER TABLE [dbo].[GameObjects]
ADD CONSTRAINT [PK_GameObjects]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GameObjectTypes'
ALTER TABLE [dbo].[GameObjectTypes]
ADD CONSTRAINT [PK_GameObjectTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InstantiatedGameObjects'
ALTER TABLE [dbo].[InstantiatedGameObjects]
ADD CONSTRAINT [PK_InstantiatedGameObjects]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ScreenCoord'
ALTER TABLE [dbo].[ScreenCoord]
ADD CONSTRAINT [PK_ScreenCoord]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CellCoord'
ALTER TABLE [dbo].[CellCoord]
ADD CONSTRAINT [PK_CellCoord]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Scenes'
ALTER TABLE [dbo].[Scenes]
ADD CONSTRAINT [PK_Scenes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ComponentDependencies'
ALTER TABLE [dbo].[ComponentDependencies]
ADD CONSTRAINT [PK_ComponentDependencies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Levels'
ALTER TABLE [dbo].[Levels]
ADD CONSTRAINT [PK_Levels]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SaveGames'
ALTER TABLE [dbo].[SaveGames]
ADD CONSTRAINT [PK_SaveGames]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CurrentAnimations'
ALTER TABLE [dbo].[CurrentAnimations]
ADD CONSTRAINT [PK_CurrentAnimations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Animations'
ALTER TABLE [dbo].[Animations]
ADD CONSTRAINT [PK_Animations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Velocities'
ALTER TABLE [dbo].[Velocities]
ADD CONSTRAINT [PK_Velocities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Accelerations'
ALTER TABLE [dbo].[Accelerations]
ADD CONSTRAINT [PK_Accelerations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [GameObjectType_Id] in table 'GameObjects'
ALTER TABLE [dbo].[GameObjects]
ADD CONSTRAINT [FK_GameObjectGameObjectType]
    FOREIGN KEY ([GameObjectType_Id])
    REFERENCES [dbo].[GameObjectTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameObjectGameObjectType'
CREATE INDEX [IX_FK_GameObjectGameObjectType]
ON [dbo].[GameObjects]
    ([GameObjectType_Id]);
GO

-- Creating foreign key on [GameObject_Id] in table 'InstantiatedGameObjects'
ALTER TABLE [dbo].[InstantiatedGameObjects]
ADD CONSTRAINT [FK_InstantiatedGameObjectGameObject]
    FOREIGN KEY ([GameObject_Id])
    REFERENCES [dbo].[GameObjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstantiatedGameObjectGameObject'
CREATE INDEX [IX_FK_InstantiatedGameObjectGameObject]
ON [dbo].[InstantiatedGameObjects]
    ([GameObject_Id]);
GO

-- Creating foreign key on [GameObject_Id] in table 'ScreenCoord'
ALTER TABLE [dbo].[ScreenCoord]
ADD CONSTRAINT [FK_ScreenCoordGameObject]
    FOREIGN KEY ([GameObject_Id])
    REFERENCES [dbo].[GameObjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ScreenCoordGameObject'
CREATE INDEX [IX_FK_ScreenCoordGameObject]
ON [dbo].[ScreenCoord]
    ([GameObject_Id]);
GO

-- Creating foreign key on [GameObject_Id] in table 'CellCoord'
ALTER TABLE [dbo].[CellCoord]
ADD CONSTRAINT [FK_GameObjectCellCoord]
    FOREIGN KEY ([GameObject_Id])
    REFERENCES [dbo].[GameObjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameObjectCellCoord'
CREATE INDEX [IX_FK_GameObjectCellCoord]
ON [dbo].[CellCoord]
    ([GameObject_Id]);
GO

-- Creating foreign key on [Scene_Id] in table 'SaveGames'
ALTER TABLE [dbo].[SaveGames]
ADD CONSTRAINT [FK_SceneSaveGame]
    FOREIGN KEY ([Scene_Id])
    REFERENCES [dbo].[Scenes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SceneSaveGame'
CREATE INDEX [IX_FK_SceneSaveGame]
ON [dbo].[SaveGames]
    ([Scene_Id]);
GO

-- Creating foreign key on [Scene_Id] in table 'Levels'
ALTER TABLE [dbo].[Levels]
ADD CONSTRAINT [FK_LevelScene]
    FOREIGN KEY ([Scene_Id])
    REFERENCES [dbo].[Scenes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LevelScene'
CREATE INDEX [IX_FK_LevelScene]
ON [dbo].[Levels]
    ([Scene_Id]);
GO

-- Creating foreign key on [Scene_Id] in table 'InstantiatedGameObjects'
ALTER TABLE [dbo].[InstantiatedGameObjects]
ADD CONSTRAINT [FK_InstantiatedGameObjectScene]
    FOREIGN KEY ([Scene_Id])
    REFERENCES [dbo].[Scenes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstantiatedGameObjectScene'
CREATE INDEX [IX_FK_InstantiatedGameObjectScene]
ON [dbo].[InstantiatedGameObjects]
    ([Scene_Id]);
GO

-- Creating foreign key on [GameObject_Id] in table 'CurrentAnimations'
ALTER TABLE [dbo].[CurrentAnimations]
ADD CONSTRAINT [FK_CurrentAnimationGameObject]
    FOREIGN KEY ([GameObject_Id])
    REFERENCES [dbo].[GameObjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CurrentAnimationGameObject'
CREATE INDEX [IX_FK_CurrentAnimationGameObject]
ON [dbo].[CurrentAnimations]
    ([GameObject_Id]);
GO

-- Creating foreign key on [Animation_Id] in table 'CurrentAnimations'
ALTER TABLE [dbo].[CurrentAnimations]
ADD CONSTRAINT [FK_CurrentAnimationAnimation]
    FOREIGN KEY ([Animation_Id])
    REFERENCES [dbo].[Animations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CurrentAnimationAnimation'
CREATE INDEX [IX_FK_CurrentAnimationAnimation]
ON [dbo].[CurrentAnimations]
    ([Animation_Id]);
GO

-- Creating foreign key on [GameObject_Id] in table 'Velocities'
ALTER TABLE [dbo].[Velocities]
ADD CONSTRAINT [FK_VelocityGameObject]
    FOREIGN KEY ([GameObject_Id])
    REFERENCES [dbo].[GameObjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VelocityGameObject'
CREATE INDEX [IX_FK_VelocityGameObject]
ON [dbo].[Velocities]
    ([GameObject_Id]);
GO

-- Creating foreign key on [GameObject_Id] in table 'Accelerations'
ALTER TABLE [dbo].[Accelerations]
ADD CONSTRAINT [FK_AccelerationGameObject]
    FOREIGN KEY ([GameObject_Id])
    REFERENCES [dbo].[GameObjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccelerationGameObject'
CREATE INDEX [IX_FK_AccelerationGameObject]
ON [dbo].[Accelerations]
    ([GameObject_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------