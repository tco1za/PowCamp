using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PowCamp
{
    class DataAccess
    {
        private static int currentLevelId = 1;

        private static PowCampDatabaseModelContainer db = new PowCampDatabaseModelContainer();    

        private static void resetIdsOfGameObjectComponents(GameObject gameObject)
        {
            object source;
            TypeInfo entityTypeInfo = typeof(GameObject).GetTypeInfo();
            IEnumerable<PropertyInfo> declaredProperties = entityTypeInfo.DeclaredProperties;
            foreach (var property in declaredProperties)
            {
                source = gameObject;
                if (property.Name != "Id" && property.Name != "GameObjectType" && property.Name != "InstantiatedGameObject")  // TODO: convert these strings to references
                {
                    PropertyInfo prop = source.GetType().GetProperty(property.Name);
                    if (prop != null)
                    {
                        source = prop.GetValue(source, null);
                        if (source != null)
                        {
                            PropertyInfo propertyToSet = source.GetType().GetProperty("Id");
                            propertyToSet.SetValue(source, -1, null);
                        }
                    }
                }
            }
        }

        public static GameObject instantiateEntity(GameObjectTypeEnum enumValue)
        {
            GameObject newGameObject;
            newGameObject = db.GameObjects.AsNoTracking().FirstOrDefault(item => item.GameObjectType.enumValue == enumValue);
            return newGameObject;
        }

        private static List<GameObject> loadScene(int sceneID)
        {
            List<GameObject> gameObjects = new List<GameObject>();
            IQueryable<GameObject> query;
            query = db.GameObjects.AsNoTracking().Where(item => item.InstantiatedGameObject.Scene.Id == sceneID);
            foreach (var val in query)
            {
                gameObjects.Add(val);
            }
            return gameObjects;
        }

        private static void saveScene(List<GameObject> gameObjects, Scene scene)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.GameObjectType = db.GameObjectTypes.Where(item => item.enumValue == gameObject.GameObjectType.enumValue  ).FirstOrDefault();
                gameObject.InstantiatedGameObject = new InstantiatedGameObject();
                gameObject.InstantiatedGameObject.Scene = scene;
                resetIdsOfGameObjectComponents(gameObject);
                db.GameObjects.Add(gameObject);
            }
            db.SaveChanges();
        }

        public static void saveGame(List<GameObject> gameObjects)
        {
            Scene savegameScene = new Scene();
            savegameScene.SaveGame = new SaveGame() { name = DateTime.Now.ToString(), levelCreatedFrom = currentLevelId };
            saveScene(gameObjects, savegameScene);
        }

        public static void saveLevel(List<GameObject> gameObjects, string name)
        {
            Scene levelScene = new Scene();
            levelScene.Level = new Level() { name = name };
            saveScene(gameObjects, levelScene);
        }

        public static List<GameObject> loadLevel(int levelID)
        {
            currentLevelId = db.Levels.Where(item => item.Id == levelID).FirstOrDefault().Id;
            return loadScene( db.Scenes.Where(item => item.Level.Id == levelID).FirstOrDefault().Id );
        }

        public static List<GameObject> loadSaveGame(string name)
        {
            currentLevelId = db.SaveGames.Where(item => item.name == name).FirstOrDefault().levelCreatedFrom;
            return loadScene(db.Scenes.Where(item => item.SaveGame.name == name).FirstOrDefault().Id);
        }

        public static void createGameObjectTypes()
        {
            GameObject grassBlock = new GameObject();
            grassBlock.GameObjectType = new GameObjectType();
            grassBlock.GameObjectType.enumValue = GameObjectTypeEnum.grassBlock;
            grassBlock.GameObjectType.name = Enum.GetName(typeof(GameObjectTypeEnum), grassBlock.GameObjectType.enumValue);
            grassBlock.ScreenCoord = new ScreenCoord() { x = 10, y = 12 };
            db.GameObjects.Add(grassBlock);
            db.SaveChanges();
        }

    }
}
