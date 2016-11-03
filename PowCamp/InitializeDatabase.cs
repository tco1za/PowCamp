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
            DataAccess.db.ComponentDependencies.Add(new ComponentDependency() { componentName = "Guard", dependsOn = "CurrentAnimation" });


            DataAccess.db.SaveChanges();
        }

        private static void createAnimations()
        {
            int spriteMap1CellWidth = 100;
            int spriteMap1CellHeight = 100;
            Animation newAnimation;
            newAnimation = new Animation() { atlasName = "spriteMap1", enumValue = AnimationEnum.fence, frameHeight = spriteMap1CellHeight, frameWidth = spriteMap1CellWidth, startIndex = 0, count = 2 };
            newAnimation.name = Enum.GetName(typeof(AnimationEnum), newAnimation.enumValue);
            DataAccess.db.Animations.Add(newAnimation);
            newAnimation = new Animation() { atlasName = "spriteMap1", enumValue = AnimationEnum.mouseCursor, frameHeight = spriteMap1CellHeight, frameWidth = spriteMap1CellWidth, startIndex = 11, count = 1 };
            newAnimation.name = Enum.GetName(typeof(AnimationEnum), newAnimation.enumValue);
            DataAccess.db.Animations.Add(newAnimation);
            newAnimation = new Animation() { atlasName = "spriteMap1", enumValue = AnimationEnum.mouseBuildGlyph, frameHeight = spriteMap1CellHeight, frameWidth = spriteMap1CellWidth, startIndex = 10, count = 1 };
            newAnimation.name = Enum.GetName(typeof(AnimationEnum), newAnimation.enumValue);
            DataAccess.db.Animations.Add(newAnimation);
            newAnimation = new Animation() { atlasName = "spriteMap1", enumValue = AnimationEnum.concreteWall, frameHeight = spriteMap1CellHeight, frameWidth = spriteMap1CellWidth, startIndex = 2, count = 2 };
            newAnimation.name = Enum.GetName(typeof(AnimationEnum), newAnimation.enumValue);
            DataAccess.db.Animations.Add(newAnimation);
            newAnimation = new Animation() { atlasName = "spriteMap1", enumValue = AnimationEnum.mouseCellCornerGlyph, frameHeight = spriteMap1CellHeight, frameWidth = spriteMap1CellWidth, startIndex = 12, count = 2 };
            newAnimation.name = Enum.GetName(typeof(AnimationEnum), newAnimation.enumValue);
            DataAccess.db.Animations.Add(newAnimation);
            newAnimation = new Animation() { atlasName = "spriteMap1", enumValue = AnimationEnum.guardWalk, frameHeight = spriteMap1CellHeight, frameWidth = spriteMap1CellWidth, startIndex = 20, count = 2 };
            newAnimation.name = Enum.GetName(typeof(AnimationEnum), newAnimation.enumValue);
            DataAccess.db.Animations.Add(newAnimation);
            newAnimation = new Animation() { atlasName = "spriteMap1", enumValue = AnimationEnum.prisonerWalk, frameHeight = spriteMap1CellHeight, frameWidth = spriteMap1CellWidth, startIndex = 30, count = 2 };
            newAnimation.name = Enum.GetName(typeof(AnimationEnum), newAnimation.enumValue);
            DataAccess.db.Animations.Add(newAnimation);
            newAnimation = new Animation() { atlasName = "spriteMap1", enumValue = AnimationEnum.greenWall, frameHeight = spriteMap1CellHeight, frameWidth = spriteMap1CellWidth, startIndex = 4, count = 2 };
            newAnimation.name = Enum.GetName(typeof(AnimationEnum), newAnimation.enumValue);
            DataAccess.db.Animations.Add(newAnimation);
            newAnimation = new Animation() { atlasName = "spriteMap1", enumValue = AnimationEnum.redWall, frameHeight = spriteMap1CellHeight, frameWidth = spriteMap1CellWidth, startIndex = 6, count = 2 };
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
                newGameObjectType.Wall = new Wall();
                DataAccess.db.GameObjects.Add(newGameObjectType);
                newGameObjectType = new GameObject();
                newGameObjectType.GameObjectType = new GameObjectType();
                newGameObjectType.GameObjectType.enumValue = GameObjectTypeEnum.mouseBuildGlyph;
                newGameObjectType.GameObjectType.name = Enum.GetName(typeof(GameObjectTypeEnum), newGameObjectType.GameObjectType.enumValue);
                newGameObjectType.CurrentAnimation = new CurrentAnimation() { index = 0, Animation = DataAccess.db.Animations.Where(item => item.enumValue == AnimationEnum.mouseBuildGlyph).FirstOrDefault() };
                newGameObjectType.Wall = new Wall();
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
                newGameObjectType.Orientation = new Orientation();
                newGameObjectType.TargetScreenCoord = new TargetScreenCoord();
                newGameObjectType.TargetPathIndex = new TargetPathIndex();
                newGameObjectType.Health = new Health();
                DataAccess.db.GameObjects.Add(newGameObjectType);
                newGameObjectType = new GameObject();
                newGameObjectType.GameObjectType = new GameObjectType();
                newGameObjectType.GameObjectType.enumValue = GameObjectTypeEnum.greenWall;
                newGameObjectType.GameObjectType.name = Enum.GetName(typeof(GameObjectTypeEnum), newGameObjectType.GameObjectType.enumValue);
                newGameObjectType.CurrentAnimation = new CurrentAnimation() { index = 0, Animation = DataAccess.db.Animations.Where(item => item.enumValue == AnimationEnum.greenWall).FirstOrDefault() };
                newGameObjectType.ScreenCoord = new ScreenCoord();
                newGameObjectType.Wall = new Wall();
                DataAccess.db.GameObjects.Add(newGameObjectType);
                newGameObjectType = new GameObject();
                newGameObjectType.GameObjectType = new GameObjectType();
                newGameObjectType.GameObjectType.enumValue = GameObjectTypeEnum.redWall;
                newGameObjectType.GameObjectType.name = Enum.GetName(typeof(GameObjectTypeEnum), newGameObjectType.GameObjectType.enumValue);
                newGameObjectType.CurrentAnimation = new CurrentAnimation() { index = 0, Animation = DataAccess.db.Animations.Where(item => item.enumValue == AnimationEnum.redWall).FirstOrDefault() };
                newGameObjectType.ScreenCoord = new ScreenCoord();
                newGameObjectType.Wall = new Wall();
                DataAccess.db.GameObjects.Add(newGameObjectType);

                DataAccess.db.SaveChanges();
                DataAccess.createAndLinkToGameObjectsAllDependenciesThatDontExist();
            }
        }

    }
}
