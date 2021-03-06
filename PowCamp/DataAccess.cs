﻿using System;
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
        public static PowCampDatabaseModelContainer db = new PowCampDatabaseModelContainer();    
        private static string currentErrorMessage;
        private static Dictionary<GameObjectTypeEnum, List<GameObject>> availableInstantiatedGameObjectsPool = new Dictionary<GameObjectTypeEnum, List<GameObject>>();
        private static Dictionary<GameObjectTypeEnum, List<GameObject>> inUseInstantiatedGameObjectsPool = new Dictionary<GameObjectTypeEnum, List<GameObject>>();

        public struct LoadResult
        {
            public List<GameObject> gameObjects;
            public Scene scene;
        }

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
                            if (property.Name == "CurrentAnimation")
                            {
                                object newComponent = CreateInstanceFromName("PowCamp." + property.Name);
                                ((CurrentAnimation)newComponent).Animation = db.Animations.Where(item => item.Id == ((CurrentAnimation)source).Animation.Id).FirstOrDefault();
                                ((CurrentAnimation)newComponent).index = ((CurrentAnimation)source).index;  // TODO: automatically set properties of all components
                                prop.SetValue(gameObject, newComponent, null);
                            }
                            else
                            {
                                PropertyInfo propertyToSet = source.GetType().GetProperty("Id");
                                propertyToSet.SetValue(source, -1, null);
                            }
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

        public static GameObject fetchGameObjectFromPool(GameObjectTypeEnum enumValue)
        {
            if (!availableInstantiatedGameObjectsPool.ContainsKey(enumValue))
            {
                availableInstantiatedGameObjectsPool.Add(enumValue, new List<GameObject>());
                inUseInstantiatedGameObjectsPool.Add(enumValue, new List<GameObject>());
            }

            if (availableInstantiatedGameObjectsPool[enumValue].Any())
            {
                GameObject gameObject = availableInstantiatedGameObjectsPool[enumValue][0];
                inUseInstantiatedGameObjectsPool[enumValue].Add(gameObject);
                availableInstantiatedGameObjectsPool[enumValue].RemoveAt(0);
                return gameObject;
            }
            else
            {
                GameObject newGameObject = instantiateEntity(enumValue);
                inUseInstantiatedGameObjectsPool[enumValue].Add(newGameObject);
                return newGameObject;
            }
        }

        public static void releaseGameObjectFromPool(GameObjectTypeEnum enumValue, GameObject gameObject)
        {
            availableInstantiatedGameObjectsPool[enumValue].Add(gameObject);
            inUseInstantiatedGameObjectsPool[enumValue].Remove(gameObject);
        }

        private static LoadResult loadScene(int sceneID)
        {
            LoadResult loadResult;

            loadResult.gameObjects = new List<GameObject>();
            loadResult.scene = db.Scenes.AsNoTracking().Where(item => item.Id == sceneID).FirstOrDefault();
            IQueryable<GameObject> query;
            query = db.GameObjects.AsNoTracking().Where(item => item.InstantiatedGameObject.Scene.Id == sceneID);
            foreach (var val in query)
            {
                loadResult.gameObjects.Add(val);
            }
            return loadResult;
        }

        public static List<GameObject> getAllGameObjectTypes()
        {
            return db.GameObjects.AsNoTracking().Where(item => item.InstantiatedGameObject == null).ToList();
        }

        private static void saveScene(List<GameObject> gameObjects, Scene scene)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.GameObjectType = db.GameObjectTypes.Where(item => item.enumValue == gameObject.GameObjectType.enumValue).FirstOrDefault();
                gameObject.InstantiatedGameObject = new InstantiatedGameObject();
                gameObject.InstantiatedGameObject.Scene = scene;
                resetIdsOfGameObjectComponents(gameObject);
                db.GameObjects.Add(gameObject);
            }
            db.SaveChanges();
        }

        public static void saveGame(List<GameObject> gameObjects, Scene scene)
        {
            scene.Id = -1;
            scene.SaveGame = new SaveGame() { name = DateTime.Now.ToString(), levelCreatedFrom = Game.currentLevelId };
            saveScene(gameObjects, scene);
        }

        public static void saveLevel(List<GameObject> gameObjects, string name, Scene scene)
        {
            scene.Id = -1;
            scene.Level = new Level() { name = name };
            saveScene(gameObjects, scene);
        }

        public static LoadResult loadLevel(int levelID)
        {
            Game.currentLevelId = db.Levels.Where(item => item.Id == levelID).FirstOrDefault().Id;
            return loadScene( db.Scenes.Where(item => item.Level.Id == levelID).FirstOrDefault().Id );
        }

        public static LoadResult loadSaveGame(string name)
        {
            Game.currentLevelId = db.SaveGames.Where(item => item.name == name).FirstOrDefault().levelCreatedFrom;
            return loadScene(db.Scenes.Where(item => item.SaveGame.name == name).FirstOrDefault().Id);
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

        public static List<Animation> getAllAnimations()
        {
            return db.Animations.ToList();
        }

        private static object CreateInstanceFromName(string fullyQualifiedName)
        {
            Type t = Type.GetType(fullyQualifiedName);
            return Activator.CreateInstance(t);
        }

        private static void creatAndLinkToGameObjectDependenciesThatDontExist( GameObject gameObject, string linkedComponentName )
        {
            List<string> allLinkedComponentNames = getAllLinkedComponentNames(gameObject);

            List<ComponentDependency> dependencies = db.ComponentDependencies.Where(item => item.componentName == linkedComponentName).ToList();

            foreach (ComponentDependency dependency in dependencies)
            {
                string dependentComponentName = dependency.dependsOn;
                if (!allLinkedComponentNames.Contains(dependentComponentName))
                {
                    object newComponent = CreateInstanceFromName("PowCamp." + dependentComponentName);
                    PropertyInfo propertyToSet = gameObject.GetType().GetProperty(dependentComponentName);
                    propertyToSet.SetValue(gameObject, newComponent, null);
                    creatAndLinkToGameObjectDependenciesThatDontExist(gameObject, dependentComponentName);
                }
            }
        }

        public static void createAndLinkToGameObjectsAllDependenciesThatDontExist()
        {
            var gameObjectList = from e in db.GameObjects select e;

            foreach (var gameObject in gameObjectList)
            {
                List<string> allLinkedComponentNames = getAllLinkedComponentNames(gameObject);
                foreach ( string linkedComponentName in allLinkedComponentNames)
                {
                    creatAndLinkToGameObjectDependenciesThatDontExist( gameObject, linkedComponentName );
                }
            }
            db.SaveChanges();
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
