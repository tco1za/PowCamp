using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowCamp
{
    class InitializeDatabase
    {
        private static void createComponentDependencies()
        {
            DataAccess.db.ComponentDependencies.Add(new ComponentDependency() { componentName = "Velocity", dependsOn = "ScreenCoord" });
            DataAccess.db.ComponentDependencies.Add(new ComponentDependency() { componentName = "Acceleration", dependsOn = "Velocity" });
            DataAccess.db.ComponentDependencies.Add(new ComponentDependency() { componentName = "Wall", dependsOn = "CellPartition" });
            DataAccess.db.ComponentDependencies.Add(new ComponentDependency() { componentName = "CurrentAnimation", dependsOn = "ScreenCoord" });
            DataAccess.db.ComponentDependencies.Add(new ComponentDependency() { componentName = "PatrolRoute", dependsOn = "Orientation" });
            DataAccess.db.ComponentDependencies.Add(new ComponentDependency() { componentName = "Guard", dependsOn = "PatrolRoute" });
            DataAccess.db.ComponentDependencies.Add(new ComponentDependency() { componentName = "Prisoner", dependsOn = "Health" });
            DataAccess.db.ComponentDependencies.Add(new ComponentDependency() { componentName = "Prisoner", dependsOn = "TargetScreenCoord" });
            DataAccess.db.ComponentDependencies.Add(new ComponentDependency() { componentName = "Prisoner", dependsOn = "TargetPathIndex" });
            DataAccess.db.ComponentDependencies.Add(new ComponentDependency() { componentName = "Prisoner", dependsOn = "Orientation" });
            DataAccess.db.ComponentDependencies.Add(new ComponentDependency() { componentName = "Prisoner", dependsOn = "CurrentAnimation" });
            DataAccess.db.ComponentDependencies.Add(new ComponentDependency() { componentName = "Prisoner", dependsOn = "RemovalMarker" });
            DataAccess.db.ComponentDependencies.Add(new ComponentDependency() { componentName = "Wall", dependsOn = "ScreenCoord" });

            DataAccess.db.SaveChanges();
        }

        private static void createAnimations()
        {
            int spriteMap1CellWidth = 100;
            int spriteMap1CellHeight = 100;  // TODO: create a new entity for animation columns and offsets
            Animation newAnimation;
            newAnimation = new Animation() { atlasName = "spriteMap1", enumValue = AnimationEnum.fence, frameHeight = spriteMap1CellHeight, frameWidth = spriteMap1CellWidth, startIndex = 0, count = 2, numberOfColumns = 10 };
            newAnimation.name = Enum.GetName(typeof(AnimationEnum), newAnimation.enumValue);
            DataAccess.db.Animations.Add(newAnimation);
            newAnimation = new Animation() { atlasName = "spriteMap1", enumValue = AnimationEnum.mouseCursor, frameHeight = spriteMap1CellHeight, frameWidth = spriteMap1CellWidth, startIndex = 11, count = 1, numberOfColumns = 10 };
            newAnimation.name = Enum.GetName(typeof(AnimationEnum), newAnimation.enumValue);
            DataAccess.db.Animations.Add(newAnimation);
            newAnimation = new Animation() { atlasName = "spriteMap1", enumValue = AnimationEnum.mouseBuildGlyph, frameHeight = spriteMap1CellHeight, frameWidth = spriteMap1CellWidth, startIndex = 10, count = 1, numberOfColumns = 10 };
            newAnimation.name = Enum.GetName(typeof(AnimationEnum), newAnimation.enumValue);
            DataAccess.db.Animations.Add(newAnimation);
            newAnimation = new Animation() { atlasName = "spriteMap1", enumValue = AnimationEnum.concreteWall, frameHeight = spriteMap1CellHeight, frameWidth = spriteMap1CellWidth, startIndex = 2, count = 2, numberOfColumns = 10 };
            newAnimation.name = Enum.GetName(typeof(AnimationEnum), newAnimation.enumValue);
            DataAccess.db.Animations.Add(newAnimation);
            newAnimation = new Animation() { atlasName = "spriteMap1", enumValue = AnimationEnum.mouseCellCornerGlyph, frameHeight = spriteMap1CellHeight, frameWidth = spriteMap1CellWidth, startIndex = 12, count = 2, numberOfColumns = 10 };
            newAnimation.name = Enum.GetName(typeof(AnimationEnum), newAnimation.enumValue);
            DataAccess.db.Animations.Add(newAnimation);
            newAnimation = new Animation() { atlasName = "spriteMap1", enumValue = AnimationEnum.guardWalk, frameHeight = spriteMap1CellHeight, frameWidth = spriteMap1CellWidth, startIndex = 20, count = 2, numberOfColumns = 10 };
            newAnimation.name = Enum.GetName(typeof(AnimationEnum), newAnimation.enumValue);
            DataAccess.db.Animations.Add(newAnimation);
            newAnimation = new Animation() { atlasName = "spriteMap1", enumValue = AnimationEnum.prisonerWalk, frameHeight = spriteMap1CellHeight, frameWidth = spriteMap1CellWidth, startIndex = 30, count = 2, numberOfColumns = 10 };
            newAnimation.name = Enum.GetName(typeof(AnimationEnum), newAnimation.enumValue);
            DataAccess.db.Animations.Add(newAnimation);
            newAnimation = new Animation() { atlasName = "spriteMap1", enumValue = AnimationEnum.greenWall, frameHeight = spriteMap1CellHeight, frameWidth = spriteMap1CellWidth, startIndex = 4, count = 2, numberOfColumns = 10 };
            newAnimation.name = Enum.GetName(typeof(AnimationEnum), newAnimation.enumValue);
            DataAccess.db.Animations.Add(newAnimation);
            newAnimation = new Animation() { atlasName = "spriteMap1", enumValue = AnimationEnum.redWall, frameHeight = spriteMap1CellHeight, frameWidth = spriteMap1CellWidth, startIndex = 6, count = 2, numberOfColumns = 10 };
            newAnimation.name = Enum.GetName(typeof(AnimationEnum), newAnimation.enumValue);
            DataAccess.db.Animations.Add(newAnimation);
            newAnimation = new Animation() { atlasName = "spriteMap1", enumValue = AnimationEnum.guardShooting, frameHeight = spriteMap1CellHeight, frameWidth = spriteMap1CellWidth, startIndex = 22, count = 3, timeBetweenFrames = 0.1f, mustRepeat = false, numberOfColumns = 10 };
            newAnimation.name = Enum.GetName(typeof(AnimationEnum), newAnimation.enumValue);
            DataAccess.db.Animations.Add(newAnimation);
            newAnimation = new Animation() { atlasName = "spriteMap1", enumValue = AnimationEnum.guardTurning, frameHeight = spriteMap1CellHeight, frameWidth = spriteMap1CellWidth, startIndex = 25, count = 2, timeBetweenFrames = 0.5f, numberOfColumns = 10 };
            newAnimation.name = Enum.GetName(typeof(AnimationEnum), newAnimation.enumValue);
            DataAccess.db.Animations.Add(newAnimation);
            newAnimation = new Animation() { atlasName = "spriteMap1", enumValue = AnimationEnum.prisonerDying, frameHeight = spriteMap1CellHeight, frameWidth = spriteMap1CellWidth, startIndex = 32, count = 3, timeBetweenFrames = 0.5f, mustRepeat = false, numberOfColumns = 10 };
            newAnimation.name = Enum.GetName(typeof(AnimationEnum), newAnimation.enumValue);
            DataAccess.db.Animations.Add(newAnimation);

            newAnimation = new Animation() { atlasName = "buttonsAndMisc", enumValue = AnimationEnum.sidePanels, frameHeight = 1080, frameWidth = 204, startIndex = 0, count = 2, topLeftCoordOfFirstFrameX = 0, topLeftCoordOfFirstFrameY = 0, numberOfColumns = 2, mustAnimate = false };
            newAnimation.name = Enum.GetName(typeof(AnimationEnum), newAnimation.enumValue);
            DataAccess.db.Animations.Add(newAnimation);
            newAnimation = new Animation() { atlasName = "buttonsAndMisc", enumValue = AnimationEnum.buttons, frameHeight = 180, frameWidth = 180, startIndex = 0, count = 4, topLeftCoordOfFirstFrameX = 408, topLeftCoordOfFirstFrameY = 0, numberOfColumns = 4, mustAnimate = false };
            newAnimation.name = Enum.GetName(typeof(AnimationEnum), newAnimation.enumValue);
            DataAccess.db.Animations.Add(newAnimation);


            DataAccess.db.SaveChanges();
        }

        public static void createGameObjectTypes()
        {
            if (DataAccess.db.GameObjectTypes.Count() == 0)
            {
                createComponentDependencies();
                createAnimations();
                GameObject newGameObjectType;
                newGameObjectType = new GameObject();
                newGameObjectType.GameObjectType = new GameObjectType();
                newGameObjectType.GameObjectType.enumValue = GameObjectTypeEnum.fence;
                newGameObjectType.GameObjectType.name = Enum.GetName(typeof(GameObjectTypeEnum), newGameObjectType.GameObjectType.enumValue);
                newGameObjectType.CurrentAnimation = new CurrentAnimation() { index = 0, Animation = DataAccess.db.Animations.Where(item => item.enumValue == AnimationEnum.fence).FirstOrDefault() };
                newGameObjectType.Wall = new Wall();
                DataAccess.db.GameObjects.Add(newGameObjectType);
                newGameObjectType = new GameObject();
                newGameObjectType.GameObjectType = new GameObjectType();
                newGameObjectType.GameObjectType.enumValue = GameObjectTypeEnum.mouseCursor;
                newGameObjectType.GameObjectType.name = Enum.GetName(typeof(GameObjectTypeEnum), newGameObjectType.GameObjectType.enumValue);
                newGameObjectType.CurrentAnimation = new CurrentAnimation() { index = 0, Animation = DataAccess.db.Animations.Where(item => item.enumValue == AnimationEnum.mouseCursor).FirstOrDefault() };
                newGameObjectType.Wall = new Wall();  // TODO: remove this?
                DataAccess.db.GameObjects.Add(newGameObjectType);
                newGameObjectType = new GameObject();
                newGameObjectType.GameObjectType = new GameObjectType();
                newGameObjectType.GameObjectType.enumValue = GameObjectTypeEnum.mouseBuildGlyph;
                newGameObjectType.GameObjectType.name = Enum.GetName(typeof(GameObjectTypeEnum), newGameObjectType.GameObjectType.enumValue);
                newGameObjectType.CurrentAnimation = new CurrentAnimation() { index = 0, Animation = DataAccess.db.Animations.Where(item => item.enumValue == AnimationEnum.mouseBuildGlyph).FirstOrDefault() };
                newGameObjectType.Wall = new Wall();  // TODO: remove this ?
                DataAccess.db.GameObjects.Add(newGameObjectType);
                newGameObjectType = new GameObject();
                newGameObjectType.GameObjectType = new GameObjectType();
                newGameObjectType.GameObjectType.enumValue = GameObjectTypeEnum.concreteWall;
                newGameObjectType.GameObjectType.name = Enum.GetName(typeof(GameObjectTypeEnum), newGameObjectType.GameObjectType.enumValue);
                newGameObjectType.CurrentAnimation = new CurrentAnimation() { index = 0, Animation = DataAccess.db.Animations.Where(item => item.enumValue == AnimationEnum.concreteWall).FirstOrDefault() };
                newGameObjectType.Wall = new Wall();
                DataAccess.db.GameObjects.Add(newGameObjectType);
                newGameObjectType = new GameObject();
                newGameObjectType.GameObjectType = new GameObjectType();
                newGameObjectType.GameObjectType.enumValue = GameObjectTypeEnum.mouseCellCornerGlyph;
                newGameObjectType.GameObjectType.name = Enum.GetName(typeof(GameObjectTypeEnum), newGameObjectType.GameObjectType.enumValue);
                newGameObjectType.CurrentAnimation = new CurrentAnimation() { index = 0, Animation = DataAccess.db.Animations.Where(item => item.enumValue == AnimationEnum.mouseCellCornerGlyph).FirstOrDefault() };
                DataAccess.db.GameObjects.Add(newGameObjectType);
                newGameObjectType = new GameObject();
                newGameObjectType.GameObjectType = new GameObjectType();
                newGameObjectType.GameObjectType.enumValue = GameObjectTypeEnum.guard;
                newGameObjectType.GameObjectType.name = Enum.GetName(typeof(GameObjectTypeEnum), newGameObjectType.GameObjectType.enumValue);
                newGameObjectType.CurrentAnimation = new CurrentAnimation() { index = 0, Animation = DataAccess.db.Animations.Where(item => item.enumValue == AnimationEnum.guardWalk).FirstOrDefault() };
                newGameObjectType.Guard = new Guard() { state = GuardState.patrolling, patrolState = GuardPatrollingState.walking };
                DataAccess.db.GameObjects.Add(newGameObjectType);
                newGameObjectType = new GameObject();
                newGameObjectType.GameObjectType = new GameObjectType();
                newGameObjectType.GameObjectType.enumValue = GameObjectTypeEnum.prisoner;
                newGameObjectType.GameObjectType.name = Enum.GetName(typeof(GameObjectTypeEnum), newGameObjectType.GameObjectType.enumValue);
                newGameObjectType.CurrentAnimation = new CurrentAnimation() { index = 0, Animation = DataAccess.db.Animations.Where(item => item.enumValue == AnimationEnum.prisonerWalk).FirstOrDefault() };
                newGameObjectType.ScreenCoord = new ScreenCoord() { x = 0, y = 0 };
                newGameObjectType.Prisoner = new Prisoner();
                newGameObjectType.RemovalMarker = new RemovalMarker() { timeToRemoval = 10 };
                DataAccess.db.GameObjects.Add(newGameObjectType);
                newGameObjectType = new GameObject();
                newGameObjectType.GameObjectType = new GameObjectType();
                newGameObjectType.GameObjectType.enumValue = GameObjectTypeEnum.greenWall;
                newGameObjectType.GameObjectType.name = Enum.GetName(typeof(GameObjectTypeEnum), newGameObjectType.GameObjectType.enumValue);
                newGameObjectType.CurrentAnimation = new CurrentAnimation() { index = 0, Animation = DataAccess.db.Animations.Where(item => item.enumValue == AnimationEnum.greenWall).FirstOrDefault() };
                newGameObjectType.Wall = new Wall();
                DataAccess.db.GameObjects.Add(newGameObjectType);
                newGameObjectType = new GameObject();
                newGameObjectType.GameObjectType = new GameObjectType();
                newGameObjectType.GameObjectType.enumValue = GameObjectTypeEnum.redWall;
                newGameObjectType.GameObjectType.name = Enum.GetName(typeof(GameObjectTypeEnum), newGameObjectType.GameObjectType.enumValue);
                newGameObjectType.CurrentAnimation = new CurrentAnimation() { index = 0, Animation = DataAccess.db.Animations.Where(item => item.enumValue == AnimationEnum.redWall).FirstOrDefault() };
                newGameObjectType.Wall = new Wall();
                DataAccess.db.GameObjects.Add(newGameObjectType);
                newGameObjectType = new GameObject();
                newGameObjectType.GameObjectType = new GameObjectType();
                newGameObjectType.GameObjectType.enumValue = GameObjectTypeEnum.leftSidePanel;
                newGameObjectType.GameObjectType.name = Enum.GetName(typeof(GameObjectTypeEnum), newGameObjectType.GameObjectType.enumValue);
                newGameObjectType.CurrentAnimation = new CurrentAnimation() { index = 0, Animation = DataAccess.db.Animations.Where(item => item.enumValue == AnimationEnum.sidePanels).FirstOrDefault() };
                DataAccess.db.GameObjects.Add(newGameObjectType);
                newGameObjectType = new GameObject();
                newGameObjectType.GameObjectType = new GameObjectType();
                newGameObjectType.GameObjectType.enumValue = GameObjectTypeEnum.rightSidePanel;
                newGameObjectType.GameObjectType.name = Enum.GetName(typeof(GameObjectTypeEnum), newGameObjectType.GameObjectType.enumValue);
                newGameObjectType.CurrentAnimation = new CurrentAnimation() { index = 1, Animation = DataAccess.db.Animations.Where(item => item.enumValue == AnimationEnum.sidePanels).FirstOrDefault() };
                DataAccess.db.GameObjects.Add(newGameObjectType);

                createButtons();
                createTools();

                DataAccess.db.SaveChanges();
                DataAccess.createAndLinkToGameObjectsAllDependenciesThatDontExist();
            }
        }

        private static void createButtons()
        {
            GameObject newGameObjectType;
            newGameObjectType = new GameObject();
            newGameObjectType.GameObjectType = new GameObjectType();  // TODO:  make a function for all these
            newGameObjectType.GameObjectType.enumValue = GameObjectTypeEnum.hireGuardButton;
            newGameObjectType.GameObjectType.name = Enum.GetName(typeof(GameObjectTypeEnum), newGameObjectType.GameObjectType.enumValue);
            newGameObjectType.CurrentAnimation = new CurrentAnimation() { index = 2, Animation = DataAccess.db.Animations.Where(item => item.enumValue == AnimationEnum.buttons).FirstOrDefault() };
            DataAccess.db.GameObjects.Add(newGameObjectType);
            newGameObjectType = new GameObject();
            newGameObjectType.GameObjectType = new GameObjectType();
            newGameObjectType.GameObjectType.enumValue = GameObjectTypeEnum.buildFenceButton;
            newGameObjectType.GameObjectType.name = Enum.GetName(typeof(GameObjectTypeEnum), newGameObjectType.GameObjectType.enumValue);
            newGameObjectType.CurrentAnimation = new CurrentAnimation() { index = 0, Animation = DataAccess.db.Animations.Where(item => item.enumValue == AnimationEnum.buttons).FirstOrDefault() };
            DataAccess.db.GameObjects.Add(newGameObjectType);
        }

        private static void createTools()
        {
            GameObject newGameObjectType;
            newGameObjectType = new GameObject();
            newGameObjectType.GameObjectType = new GameObjectType();
            newGameObjectType.GameObjectType.enumValue = GameObjectTypeEnum.buildFenceTool;
            newGameObjectType.GameObjectType.name = Enum.GetName(typeof(GameObjectTypeEnum), newGameObjectType.GameObjectType.enumValue);
            newGameObjectType.Tool = new Tool() { buttonEnum = GameObjectTypeEnum.buildFenceButton };
            DataAccess.db.GameObjects.Add(newGameObjectType);
            newGameObjectType = new GameObject();
            newGameObjectType.GameObjectType = new GameObjectType();
            newGameObjectType.GameObjectType.enumValue = GameObjectTypeEnum.hireGuardTool;
            newGameObjectType.GameObjectType.name = Enum.GetName(typeof(GameObjectTypeEnum), newGameObjectType.GameObjectType.enumValue);
            newGameObjectType.Tool = new Tool() { buttonEnum = GameObjectTypeEnum.hireGuardButton };
            DataAccess.db.GameObjects.Add(newGameObjectType);
        }
    }
}
