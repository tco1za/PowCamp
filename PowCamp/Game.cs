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

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            //graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            //graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
                graphics.PreferredBackBufferWidth = 1380;
               graphics.PreferredBackBufferHeight = 700;
            //    graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            randomNumberGenerator = new Random();

            PathFindingGraph.initialize();

            InitializeDatabase.createGameObjectTypes();

            animations = DataAccess.getAllAnimations();

            GameObject guard = DataAccess.instantiateEntity(GameObjectTypeEnum.guard);
            UserInterface.guardToAssignPatrolRouteTo = guard;

            scene = new Scene();  // TODO: remove this when loading a level or game

       //     loadLevel();

            gameObjects.Add(guard);

            //   gameObjects = DataAccess.loadSaveGame("10/3/2016 12:00:00 AM");
            //     DataAccess.saveGame(gameObjects);
            base.Initialize();
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
            atlases.Add("spriteMap1", spriteMap1);
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

            Prisoners.update(gameTime);
            Guards.update(gameTime);
            Velocities.update(gameTime);

            UserInterface.update();
            Animations.update(gameTime);

            gameObjects.RemoveAll(x => shouldGameObjectBeRemovedFromScene(x));

            base.Update(gameTime);
        }

 

        public static void drawGameObject(SpriteBatch spriteBatch, GameObject gameObject)
        {
            if (gameObject.Orientation == null)
            { 
                spriteBatch.Draw(Game.atlases[gameObject.CurrentAnimation.Animation.atlasName],
                    new Vector2((float)gameObject.ScreenCoord.x - gameObject.CurrentAnimation.Animation.frameWidth / 2,
                    (float)gameObject.ScreenCoord.y - gameObject.CurrentAnimation.Animation.frameHeight / 2),
                    Game.calculateSourceRectangleForSprite(gameObject.CurrentAnimation), Color.White);
            }
            else
            {
                spriteBatch.Draw(Game.atlases[gameObject.CurrentAnimation.Animation.atlasName],
                    new Vector2((float)gameObject.ScreenCoord.x,
                        (float)gameObject.ScreenCoord.y),
                    Game.calculateSourceRectangleForSprite(gameObject.CurrentAnimation),
                    Color.White, MyMathHelper.convertVectorToAngleOfRotation(gameObject.Orientation.x, gameObject.Orientation.y),
                    new Vector2(gameObject.CurrentAnimation.Animation.frameWidth / 2, gameObject.CurrentAnimation.Animation.frameHeight / 2), 1f, SpriteEffects.None, 1f);
            }
        }

        public static Rectangle calculateSourceRectangleForSprite(CurrentAnimation curAnim)
        {
            const int spriteMapSize = 1024;

            int index = curAnim.Animation.startIndex + curAnim.index;
            int cellX = index % (spriteMapSize / curAnim.Animation.frameWidth);
            int cellY = index / (spriteMapSize / curAnim.Animation.frameHeight);
            return new Rectangle(new Point(cellX * curAnim.Animation.frameWidth, cellY * curAnim.Animation.frameHeight),
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

            //    Walls.draw(gameObjects, spriteBatch);

            gameObjects.ForEach(a => drawGameObject(spriteBatch, a));

            UserInterface.draw(spriteBatch);
         
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
