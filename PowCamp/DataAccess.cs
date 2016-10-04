using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Design.PluralizationServices;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace PowCamp
{
    class DataAccess
    {
        private static int currentLevelId = 1;

        private static PowCampDatabaseModelContainer db = new PowCampDatabaseModelContainer();    

        private static string currentErrorMessage;

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

        public static void createComponentDependencies()
        {
            ComponentDependency newComponentDependency;
            newComponentDependency = new ComponentDependency() { componentName = "Velocity", dependsOn = "ScreenCoord" };
            db.ComponentDependencies.Add(newComponentDependency);
            newComponentDependency = new ComponentDependency() { componentName = "Acceleration", dependsOn = "Velocity" };
            db.ComponentDependencies.Add(newComponentDependency);
            db.SaveChanges();
        }

        public static void createGameObjectTypes()
        {
            GameObject newGameObjectType = new GameObject();
            newGameObjectType.GameObjectType = new GameObjectType();
            newGameObjectType.GameObjectType.enumValue = GameObjectTypeEnum.grassBlock;
            newGameObjectType.GameObjectType.name = Enum.GetName(typeof(GameObjectTypeEnum), newGameObjectType.GameObjectType.enumValue);
            newGameObjectType.Acceleration = new Acceleration();
            db.GameObjects.Add(newGameObjectType);

            db.SaveChanges();            
        }

        private static List<string> getDatabaseTableNames()
        {
            List<string> tableNames = new List<string>();
            db.Database.Connection.Open();
            DataTable schema = db.Database.Connection.GetSchema("Tables");
            foreach (DataRow row in schema.Rows)
            {
                tableNames.Add(row[2].ToString());
            }
            return tableNames;
        }

        private static List<string> getRowsInComponentDependenciesThatContainErrors()
        {
            List<string> tableNames = getDatabaseTableNames();
            List<string> rowsThatContainErrors = new List<string>();
            var query = from e in db.ComponentDependencies
                        select e;
            foreach (var row in query)
            {
                if (tableNames.Contains(row.componentName) == false || tableNames.Contains(row.dependsOn) == false)
                {
                    rowsThatContainErrors.Add(row.componentName + " " + row.dependsOn);
                }
            }
            currentErrorMessage = "Database Incoherent! There are field entries in ComponentDependencies that dont match the names of Tables. Invalid rows are: ";
            currentErrorMessage += String.Join(", ", rowsThatContainErrors);
            return rowsThatContainErrors;
        }

        private static List<string> getRowsInComponentDependenciesTableThatHaveCircularDependencies()
        {
            // TODO : implement
            return new List<string>();
        }


        public static void createAndLinkToGameObjectsAllDependenciesThatDontExist()
        {
            List<GameObject> gameObjectList = db.GameObjects.AsNoTracking().ToList();
            foreach (var gameObject in gameObjectList)
            {
                List<string> allLinkedComponentNames = getAllLinkedComponentNames(gameObject);
                foreach ( string linkedComponentName in allLinkedComponentNames)
                {
                    string dependentComponentName = db.ComponentDependencies.Where(item => item.componentName == linkedComponentName).FirstOrDefault().dependsOn;
                    while ( !allLinkedComponentNames.Contains(dependentComponentName))
                    {
                        db.Database.ExecuteSqlCommand("INSERT INTO [" + PluralizationService.CreateService(new CultureInfo("en-US")).Pluralize(dependentComponentName) + "] (GameObject_Id) VALUES ( " + gameObject.Id + " );");
                        dependentComponentName = db.ComponentDependencies.Where(item => item.componentName == dependentComponentName).FirstOrDefault().dependsOn;
                    }
                }
            }
        }

        private static List<string> getAllLinkedComponentNames(GameObject gameObject)
        {
            List<string> linkedComponents = new List<string>();
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
                            linkedComponents.Add(property.Name);
                        }
                    }
                }
            }
            return linkedComponents;
        }

        public static void isDatabaseCoherent()
        {
            Debug.Assert(getRowsInComponentDependenciesThatContainErrors().Count == 0, currentErrorMessage);
        }



    }
}
