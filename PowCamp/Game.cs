using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace PowCamp
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private Texture2D spriteMap1;
        private Texture2D background;
        private Texture2D buttonsAndMisc;
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static List<GameObject> gameObjects = new List<GameObject>();
        private static MouseState previousMouseState;
        public static MouseState currentMouseState;
        public static bool isLeftMouseClicked = false;
        public static Dictionary<string, Texture2D> atlases = new Dictionary<string, Texture2D>();
        public static List<Animation> animations;
        public static Scene scene;
        public static Random randomNumberGenerator;
        public static List<GameObject> gameObjectTypes;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
        //    graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
        //    graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
                graphics.PreferredBackBufferWidth = 1380;
               graphics.PreferredBackBufferHeight = 700;
        //        graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            randomNumberGenerator = new Random();
            PathFindingGraph.initialize();
            UserInterface.initialize();
            InitializeDatabase.createGameObjectTypes();
            animations = DataAccess.getAllAnimations();
            gameObjectTypes = DataAccess.getAllGameObjectTypes();

            createLevel();  // TODO: remove when loading

            //     loadLevel();
       

            //   gameObjects = DataAccess.loadSaveGame("10/3/2016 12:00:00 AM");
            //     DataAccess.saveGame(gameObjects);
            base.Initialize();
        }

        private static void createLevel()
        {
         

            GameObject hireGuardTool = DataAccess.instantiateEntity(GameObjectTypeEnum.hireGuardTool);
            gameObjects.Add(hireGuardTool);
            GameObject buildFenceTool = DataAccess.instantiateEntity(GameObjectTypeEnum.buildFenceTool);
            gameObjects.Add(buildFenceTool);
            scene = new Scene(); 
        }

        private static void loadLevel()
        {
            DataAccess.LoadResult loadResult = DataAccess.loadLevel(1);
            gameObjects = loadResult.gameObjects;
            scene = loadResult.scene;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>("backTest");
            spriteMap1 = Content.Load<Texture2D>("spriteMap1");
            buttonsAndMisc = Content.Load<Texture2D>("buttonsAndMisc");

            atlases.Add("spriteMap1", spriteMap1);
            atlases.Add("buttonsAndMisc", buttonsAndMisc);

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                DataAccess.saveLevel(gameObjects, "level1", scene);
                Exit();
            }
            // TODO: Add your update logic here
            // TODO: play audio here
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            if (previousMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
            {
                isLeftMouseClicked = true;
            }
            else
            {
                isLeftMouseClicked = false;
            }

            gameObjects.RemoveAll(x => shouldGameObjectBeRemovedFromScene(x));
            RemovalMarkers.update(gameObjects, gameTime);


            Prisoners.update(gameTime);
            Guards.update(gameTime);
            Velocities.update(gameTime);

            UserInterface.update();
            Animations.update(gameTime);

            base.Update(gameTime);
        }

        public static void drawGameObject(SpriteBatch spriteBatch, GameObject gameObject)
        {
            float alpha = 1f;
            if ( gameObject.RemovalMarker != null && gameObject.RemovalMarker.mustBeRemoved )
            {
                alpha = 1-(gameObject.RemovalMarker.timeSinceMarkedForRemoval / gameObject.RemovalMarker.timeToRemoval);
            }
            Color color = new Color(new Color(255,255,255), alpha);
            if (gameObject.Orientation == null)
            { 
                spriteBatch.Draw(Game.atlases[gameObject.CurrentAnimation.Animation.atlasName],
                    new Vector2((float)gameObject.ScreenCoord.x - gameObject.CurrentAnimation.Animation.frameWidth / 2,
                    (float)gameObject.ScreenCoord.y - gameObject.CurrentAnimation.Animation.frameHeight / 2),
                    Game.calculateSourceRectangleForSprite(gameObject.CurrentAnimation), color);
            }
            else
            {
                spriteBatch.Draw(Game.atlases[gameObject.CurrentAnimation.Animation.atlasName],
                    new Vector2((float)gameObject.ScreenCoord.x,
                        (float)gameObject.ScreenCoord.y),
                    Game.calculateSourceRectangleForSprite(gameObject.CurrentAnimation),
                    color, MyMathHelper.convertVectorToAngleOfRotationInRadians(gameObject.Orientation.x, gameObject.Orientation.y),
                    new Vector2(gameObject.CurrentAnimation.Animation.frameWidth / 2, gameObject.CurrentAnimation.Animation.frameHeight / 2), 1f, SpriteEffects.None, 1f);
            }
        }

        public static Rectangle calculateSourceRectangleForSprite(CurrentAnimation curAnim)   // TODO: move to Animations class
        {
            int index = curAnim.Animation.startIndex + curAnim.index;
            int cellX = index % curAnim.Animation.numberOfColumns;
            int cellY = index / curAnim.Animation.numberOfColumns;
            return new Rectangle(new Point(cellX * curAnim.Animation.frameWidth + curAnim.Animation.topLeftCoordOfFirstFrameX,
                cellY * curAnim.Animation.frameHeight + curAnim.Animation.topLeftCoordOfFirstFrameY),
                new Point(curAnim.Animation.frameWidth, curAnim.Animation.frameHeight));
        }

        private bool isGameObjectSpriteVisibleOnscreen(GameObject gameObject )
        {
            Point coordinateOfBottomRightMostOffscreenCell = UserInterface.convertCellCoordsToVirtualScreenCoords(
                    new Point(UserInterface.getNumHorizontalCells() + 1, UserInterface.getNumVerticalCells() + 1));
            Point coordinateOfTopLeftMostOffscreenCell = UserInterface.convertCellCoordsToVirtualScreenCoords(
                    new Point(-1, -1));
            if ( gameObject.ScreenCoord.x >= coordinateOfBottomRightMostOffscreenCell.X + UserInterface.cellWidth/2 ||
                gameObject.ScreenCoord.x <= coordinateOfTopLeftMostOffscreenCell.X + UserInterface.cellWidth / 2 ||
                gameObject.ScreenCoord.y >= coordinateOfBottomRightMostOffscreenCell.Y + UserInterface.cellWidth / 2  ||
                gameObject.ScreenCoord.y <= coordinateOfTopLeftMostOffscreenCell.Y + UserInterface.cellWidth / 2)  
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool shouldGameObjectBeRemovedFromScene(GameObject gameObject)
        {
            if ( gameObject.ScreenCoord != null && gameObject.CurrentAnimation != null && !isGameObjectSpriteVisibleOnscreen(gameObject) )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, UserInterface.GetScaleMatrix());
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            Walls.draw(gameObjects, spriteBatch);
            gameObjects.Where(a => a.CellPartition == null && a.ScreenCoord != null && a.CurrentAnimation != null).ToList().ForEach(a => drawGameObject(spriteBatch, a));
            UserInterface.draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
