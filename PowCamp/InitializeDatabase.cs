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
            ComponentDependency newComponentDependency;
            newComponentDependency = new ComponentDependency() { componentName = "Velocity", dependsOn = "ScreenCoord" };
            db.ComponentDependencies.Add(newComponentDependency);
            newComponentDependency = new ComponentDependency() { componentName = "Acceleration", dependsOn = "Velocity" };
            db.ComponentDependencies.Add(newComponentDependency);
            db.SaveChanges();
        }

        private static void createAnimations()
        {
            int spriteMap1CellWidth = 78;
            int spriteMap1CellHeight = 78;


            Animation newAnimation = new Animation()
            {
                atlasName = "spriteMap1",
                enumValue = AnimationEnum.fence,
                frameHeight = spriteMap1CellHeight,
                frameWidth = spriteMap1CellWidth,
                startIndex = 0,
                count = 2
            };
            newAnimation.name = Enum.GetName(typeof(AnimationEnum), newAnimation.enumValue);

            db.Animations.Add(newAnimation);
            db.SaveChanges();
        }

        public static void createGameObjectTypes()
        {
            if (db.GameObjectTypes.Count() == 0)
            {
                createComponentDependencies();
                createAnimations();
                GameObject newGameObjectType = new GameObject();
                newGameObjectType.GameObjectType = new GameObjectType();
                newGameObjectType.GameObjectType.enumValue = GameObjectTypeEnum.fence;
                newGameObjectType.GameObjectType.name = Enum.GetName(typeof(GameObjectTypeEnum), newGameObjectType.GameObjectType.enumValue);
                newGameObjectType.CurrentAnimation = new CurrentAnimation() { index = 0, Animation = db.Animations.Where(item => item.enumValue == AnimationEnum.fence).FirstOrDefault() };
                newGameObjectType.CellCoord = new CellCoord();
                db.GameObjects.Add(newGameObjectType);
                db.SaveChanges();
                createAndLinkToGameObjectsAllDependenciesThatDontExist();
            }
        }

    }
}
