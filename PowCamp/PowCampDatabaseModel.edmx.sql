
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/26/2016 19:00:24
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
    ALTER TABLE [dbo].[ScreenCoords] DROP CONSTRAINT [FK_ScreenCoordGameObject];
GO
IF OBJECT_ID(N'[dbo].[FK_GameObjectCellCoord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CellCoords] DROP CONSTRAINT [FK_GameObjectCellCoord];
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
IF OBJECT_ID(N'[dbo].[FK_WallGameObject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Walls] DROP CONSTRAINT [FK_WallGameObject];
GO
IF OBJECT_ID(N'[dbo].[FK_PatrolRouteGameObject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatrolRoutes] DROP CONSTRAINT [FK_PatrolRouteGameObject];
GO
IF OBJECT_ID(N'[dbo].[FK_CellPartitionGameObject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CellPartitions] DROP CONSTRAINT [FK_CellPartitionGameObject];
GO
IF OBJECT_ID(N'[dbo].[FK_OrientationGameObject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orientations] DROP CONSTRAINT [FK_OrientationGameObject];
GO
IF OBJECT_ID(N'[dbo].[FK_TargetScreenCoordGameObject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TargetScreenCoords] DROP CONSTRAINT [FK_TargetScreenCoordGameObject];
GO
IF OBJECT_ID(N'[dbo].[FK_PrevScreenCoordGameObject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PrevScreenCoords] DROP CONSTRAINT [FK_PrevScreenCoordGameObject];
GO
IF OBJECT_ID(N'[dbo].[FK_TargetPathIndexGameObject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TargetPathIndexes] DROP CONSTRAINT [FK_TargetPathIndexGameObject];
GO
IF OBJECT_ID(N'[dbo].[FK_GuardGameObject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Guards] DROP CONSTRAINT [FK_GuardGameObject];
GO
IF OBJECT_ID(N'[dbo].[FK_HealthGameObject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Healths] DROP CONSTRAINT [FK_HealthGameObject];
GO
IF OBJECT_ID(N'[dbo].[FK_PrisonerGameObject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Prisoners] DROP CONSTRAINT [FK_PrisonerGameObject];
GO
IF OBJECT_ID(N'[dbo].[FK_RemovalMarkerGameObject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RemovalMarkers] DROP CONSTRAINT [FK_RemovalMarkerGameObject];
GO
IF OBJECT_ID(N'[dbo].[FK_ToolGameObject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tools] DROP CONSTRAINT [FK_ToolGameObject];
GO
IF OBJECT_ID(N'[dbo].[FK_CostGameObject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Costs] DROP CONSTRAINT [FK_CostGameObject];
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
IF OBJECT_ID(N'[dbo].[ScreenCoords]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ScreenCoords];
GO
IF OBJECT_ID(N'[dbo].[CellCoords]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CellCoords];
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
IF OBJECT_ID(N'[dbo].[Walls]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Walls];
GO
IF OBJECT_ID(N'[dbo].[PatrolRoutes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PatrolRoutes];
GO
IF OBJECT_ID(N'[dbo].[CellPartitions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CellPartitions];
GO
IF OBJECT_ID(N'[dbo].[Orientations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orientations];
GO
IF OBJECT_ID(N'[dbo].[TargetScreenCoords]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TargetScreenCoords];
GO
IF OBJECT_ID(N'[dbo].[PrevScreenCoords]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PrevScreenCoords];
GO
IF OBJECT_ID(N'[dbo].[TargetPathIndexes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TargetPathIndexes];
GO
IF OBJECT_ID(N'[dbo].[Guards]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Guards];
GO
IF OBJECT_ID(N'[dbo].[Healths]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Healths];
GO
IF OBJECT_ID(N'[dbo].[Prisoners]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Prisoners];
GO
IF OBJECT_ID(N'[dbo].[RemovalMarkers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RemovalMarkers];
GO
IF OBJECT_ID(N'[dbo].[Tools]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tools];
GO
IF OBJECT_ID(N'[dbo].[Costs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Costs];
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

-- Creating table 'ScreenCoords'
CREATE TABLE [dbo].[ScreenCoords] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [x] real  NOT NULL,
    [y] real  NOT NULL,
    [GameObject_Id] int  NOT NULL
);
GO

-- Creating table 'CellCoords'
CREATE TABLE [dbo].[CellCoords] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [x] int  NOT NULL,
    [y] int  NOT NULL,
    [GameObject_Id] int  NOT NULL
);
GO

-- Creating table 'Scenes'
CREATE TABLE [dbo].[Scenes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [timeSinceLastPrisonerSpawn] real  NOT NULL,
    [timeToNextPrisonerSpawn] real  NOT NULL,
    [bankBalance] int  NOT NULL,
    [timeSinceLastGrant] real  NOT NULL,
    [numPrisonersEscaped] int  NOT NULL,
    [maxNumEscapedPrisonersAllowed] int  NOT NULL,
    [secondsLeft] real  NOT NULL
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
    [timeSinceLastFrameChange] real  NOT NULL,
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
    [startIndex] int  NOT NULL,
    [enumValue] int  NOT NULL,
    [count] int  NOT NULL,
    [timeBetweenFrames] real  NOT NULL,
    [mustRepeat] bit  NOT NULL,
    [topLeftCoordOfFirstFrameX] int  NOT NULL,
    [topLeftCoordOfFirstFrameY] int  NOT NULL,
    [numberOfColumns] int  NOT NULL,
    [mustAnimate] bit  NOT NULL
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

-- Creating table 'Walls'
CREATE TABLE [dbo].[Walls] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GameObject_Id] int  NOT NULL
);
GO

-- Creating table 'PatrolRoutes'
CREATE TABLE [dbo].[PatrolRoutes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [startCellX] int  NOT NULL,
    [middleCellX] int  NOT NULL,
    [endCellX] int  NOT NULL,
    [startCellY] int  NOT NULL,
    [middleCellY] int  NOT NULL,
    [endCellY] int  NOT NULL,
    [direction] int  NOT NULL,
    [targetCellIndex] int  NOT NULL,
    [GameObject_Id] int  NOT NULL
);
GO

-- Creating table 'CellPartitions'
CREATE TABLE [dbo].[CellPartitions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [partitionMidPointX] int  NOT NULL,
    [partitionMidPointY] int  NOT NULL,
    [GameObject_Id] int  NOT NULL
);
GO

-- Creating table 'Orientations'
CREATE TABLE [dbo].[Orientations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [x] real  NOT NULL,
    [y] real  NOT NULL,
    [GameObject_Id] int  NOT NULL
);
GO

-- Creating table 'TargetScreenCoords'
CREATE TABLE [dbo].[TargetScreenCoords] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [x] real  NOT NULL,
    [y] real  NOT NULL,
    [GameObject_Id] int  NOT NULL
);
GO

-- Creating table 'PrevScreenCoords'
CREATE TABLE [dbo].[PrevScreenCoords] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [x] real  NOT NULL,
    [y] real  NOT NULL,
    [GameObject_Id] int  NOT NULL
);
GO

-- Creating table 'TargetPathIndexes'
CREATE TABLE [dbo].[TargetPathIndexes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [x] int  NOT NULL,
    [y] int  NOT NULL,
    [GameObject_Id] int  NOT NULL
);
GO

-- Creating table 'Guards'
CREATE TABLE [dbo].[Guards] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [state] int  NOT NULL,
    [patrolState] int  NOT NULL,
    [trackingTargetState] int  NOT NULL,
    [GameObject_Id] int  NOT NULL
);
GO

-- Creating table 'Healths'
CREATE TABLE [dbo].[Healths] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [hitPoints] int  NOT NULL,
    [GameObject_Id] int  NOT NULL
);
GO

-- Creating table 'Prisoners'
CREATE TABLE [dbo].[Prisoners] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [state] int  NOT NULL,
    [GameObject_Id] int  NOT NULL
);
GO

-- Creating table 'RemovalMarkers'
CREATE TABLE [dbo].[RemovalMarkers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [mustBeRemoved] bit  NOT NULL,
    [timeSinceMarkedForRemoval] real  NOT NULL,
    [timeToRemoval] real  NOT NULL,
    [GameObject_Id] int  NOT NULL
);
GO

-- Creating table 'Tools'
CREATE TABLE [dbo].[Tools] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [buttonEnum] int  NOT NULL,
    [GameObject_Id] int  NOT NULL
);
GO

-- Creating table 'Costs'
CREATE TABLE [dbo].[Costs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [cost] int  NOT NULL,
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

-- Creating primary key on [Id] in table 'ScreenCoords'
ALTER TABLE [dbo].[ScreenCoords]
ADD CONSTRAINT [PK_ScreenCoords]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CellCoords'
ALTER TABLE [dbo].[CellCoords]
ADD CONSTRAINT [PK_CellCoords]
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

-- Creating primary key on [Id] in table 'Walls'
ALTER TABLE [dbo].[Walls]
ADD CONSTRAINT [PK_Walls]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PatrolRoutes'
ALTER TABLE [dbo].[PatrolRoutes]
ADD CONSTRAINT [PK_PatrolRoutes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CellPartitions'
ALTER TABLE [dbo].[CellPartitions]
ADD CONSTRAINT [PK_CellPartitions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Orientations'
ALTER TABLE [dbo].[Orientations]
ADD CONSTRAINT [PK_Orientations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TargetScreenCoords'
ALTER TABLE [dbo].[TargetScreenCoords]
ADD CONSTRAINT [PK_TargetScreenCoords]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PrevScreenCoords'
ALTER TABLE [dbo].[PrevScreenCoords]
ADD CONSTRAINT [PK_PrevScreenCoords]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TargetPathIndexes'
ALTER TABLE [dbo].[TargetPathIndexes]
ADD CONSTRAINT [PK_TargetPathIndexes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Guards'
ALTER TABLE [dbo].[Guards]
ADD CONSTRAINT [PK_Guards]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Healths'
ALTER TABLE [dbo].[Healths]
ADD CONSTRAINT [PK_Healths]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Prisoners'
ALTER TABLE [dbo].[Prisoners]
ADD CONSTRAINT [PK_Prisoners]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RemovalMarkers'
ALTER TABLE [dbo].[RemovalMarkers]
ADD CONSTRAINT [PK_RemovalMarkers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Tools'
ALTER TABLE [dbo].[Tools]
ADD CONSTRAINT [PK_Tools]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Costs'
ALTER TABLE [dbo].[Costs]
ADD CONSTRAINT [PK_Costs]
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

-- Creating foreign key on [GameObject_Id] in table 'ScreenCoords'
ALTER TABLE [dbo].[ScreenCoords]
ADD CONSTRAINT [FK_ScreenCoordGameObject]
    FOREIGN KEY ([GameObject_Id])
    REFERENCES [dbo].[GameObjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ScreenCoordGameObject'
CREATE INDEX [IX_FK_ScreenCoordGameObject]
ON [dbo].[ScreenCoords]
    ([GameObject_Id]);
GO

-- Creating foreign key on [GameObject_Id] in table 'CellCoords'
ALTER TABLE [dbo].[CellCoords]
ADD CONSTRAINT [FK_GameObjectCellCoord]
    FOREIGN KEY ([GameObject_Id])
    REFERENCES [dbo].[GameObjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameObjectCellCoord'
CREATE INDEX [IX_FK_GameObjectCellCoord]
ON [dbo].[CellCoords]
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

-- Creating foreign key on [GameObject_Id] in table 'Walls'
ALTER TABLE [dbo].[Walls]
ADD CONSTRAINT [FK_WallGameObject]
    FOREIGN KEY ([GameObject_Id])
    REFERENCES [dbo].[GameObjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WallGameObject'
CREATE INDEX [IX_FK_WallGameObject]
ON [dbo].[Walls]
    ([GameObject_Id]);
GO

-- Creating foreign key on [GameObject_Id] in table 'PatrolRoutes'
ALTER TABLE [dbo].[PatrolRoutes]
ADD CONSTRAINT [FK_PatrolRouteGameObject]
    FOREIGN KEY ([GameObject_Id])
    REFERENCES [dbo].[GameObjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatrolRouteGameObject'
CREATE INDEX [IX_FK_PatrolRouteGameObject]
ON [dbo].[PatrolRoutes]
    ([GameObject_Id]);
GO

-- Creating foreign key on [GameObject_Id] in table 'CellPartitions'
ALTER TABLE [dbo].[CellPartitions]
ADD CONSTRAINT [FK_CellPartitionGameObject]
    FOREIGN KEY ([GameObject_Id])
    REFERENCES [dbo].[GameObjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CellPartitionGameObject'
CREATE INDEX [IX_FK_CellPartitionGameObject]
ON [dbo].[CellPartitions]
    ([GameObject_Id]);
GO

-- Creating foreign key on [GameObject_Id] in table 'Orientations'
ALTER TABLE [dbo].[Orientations]
ADD CONSTRAINT [FK_OrientationGameObject]
    FOREIGN KEY ([GameObject_Id])
    REFERENCES [dbo].[GameObjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrientationGameObject'
CREATE INDEX [IX_FK_OrientationGameObject]
ON [dbo].[Orientations]
    ([GameObject_Id]);
GO

-- Creating foreign key on [GameObject_Id] in table 'TargetScreenCoords'
ALTER TABLE [dbo].[TargetScreenCoords]
ADD CONSTRAINT [FK_TargetScreenCoordGameObject]
    FOREIGN KEY ([GameObject_Id])
    REFERENCES [dbo].[GameObjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TargetScreenCoordGameObject'
CREATE INDEX [IX_FK_TargetScreenCoordGameObject]
ON [dbo].[TargetScreenCoords]
    ([GameObject_Id]);
GO

-- Creating foreign key on [GameObject_Id] in table 'PrevScreenCoords'
ALTER TABLE [dbo].[PrevScreenCoords]
ADD CONSTRAINT [FK_PrevScreenCoordGameObject]
    FOREIGN KEY ([GameObject_Id])
    REFERENCES [dbo].[GameObjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PrevScreenCoordGameObject'
CREATE INDEX [IX_FK_PrevScreenCoordGameObject]
ON [dbo].[PrevScreenCoords]
    ([GameObject_Id]);
GO

-- Creating foreign key on [GameObject_Id] in table 'TargetPathIndexes'
ALTER TABLE [dbo].[TargetPathIndexes]
ADD CONSTRAINT [FK_TargetPathIndexGameObject]
    FOREIGN KEY ([GameObject_Id])
    REFERENCES [dbo].[GameObjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TargetPathIndexGameObject'
CREATE INDEX [IX_FK_TargetPathIndexGameObject]
ON [dbo].[TargetPathIndexes]
    ([GameObject_Id]);
GO

-- Creating foreign key on [GameObject_Id] in table 'Guards'
ALTER TABLE [dbo].[Guards]
ADD CONSTRAINT [FK_GuardGameObject]
    FOREIGN KEY ([GameObject_Id])
    REFERENCES [dbo].[GameObjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GuardGameObject'
CREATE INDEX [IX_FK_GuardGameObject]
ON [dbo].[Guards]
    ([GameObject_Id]);
GO

-- Creating foreign key on [GameObject_Id] in table 'Healths'
ALTER TABLE [dbo].[Healths]
ADD CONSTRAINT [FK_HealthGameObject]
    FOREIGN KEY ([GameObject_Id])
    REFERENCES [dbo].[GameObjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HealthGameObject'
CREATE INDEX [IX_FK_HealthGameObject]
ON [dbo].[Healths]
    ([GameObject_Id]);
GO

-- Creating foreign key on [GameObject_Id] in table 'Prisoners'
ALTER TABLE [dbo].[Prisoners]
ADD CONSTRAINT [FK_PrisonerGameObject]
    FOREIGN KEY ([GameObject_Id])
    REFERENCES [dbo].[GameObjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PrisonerGameObject'
CREATE INDEX [IX_FK_PrisonerGameObject]
ON [dbo].[Prisoners]
    ([GameObject_Id]);
GO

-- Creating foreign key on [GameObject_Id] in table 'RemovalMarkers'
ALTER TABLE [dbo].[RemovalMarkers]
ADD CONSTRAINT [FK_RemovalMarkerGameObject]
    FOREIGN KEY ([GameObject_Id])
    REFERENCES [dbo].[GameObjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RemovalMarkerGameObject'
CREATE INDEX [IX_FK_RemovalMarkerGameObject]
ON [dbo].[RemovalMarkers]
    ([GameObject_Id]);
GO

-- Creating foreign key on [GameObject_Id] in table 'Tools'
ALTER TABLE [dbo].[Tools]
ADD CONSTRAINT [FK_ToolGameObject]
    FOREIGN KEY ([GameObject_Id])
    REFERENCES [dbo].[GameObjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ToolGameObject'
CREATE INDEX [IX_FK_ToolGameObject]
ON [dbo].[Tools]
    ([GameObject_Id]);
GO

-- Creating foreign key on [GameObject_Id] in table 'Costs'
ALTER TABLE [dbo].[Costs]
ADD CONSTRAINT [FK_CostGameObject]
    FOREIGN KEY ([GameObject_Id])
    REFERENCES [dbo].[GameObjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CostGameObject'
CREATE INDEX [IX_FK_CostGameObject]
ON [dbo].[Costs]
    ([GameObject_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------