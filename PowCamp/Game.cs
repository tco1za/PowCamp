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


        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            //graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            //graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            DataAccess.createGameObjectTypes();

            gameObjects = DataAccess.loadLevel(3);

         //   gameObjects = DataAccess.loadSaveGame("10/3/2016 12:00:00 AM");
       //     DataAccess.saveGame(gameObjects);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>("backTest");
            spriteMap1 = Content.Load<Texture2D>("spriteMap1");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                DataAccess.saveLevel(gameObjects, "level1");
                Exit();
            }
            // TODO: Add your update logic here
            // TODO: play audio here
            base.Update(gameTime);
        }

        private Rectangle calculateSourceRectangleForSprite(CurrentAnimation curAnim)
        {
            const int spriteMapSize = 1024;

            int index = curAnim.Animation.startIndex + curAnim.index;
            int cellX = index % (spriteMapSize / curAnim.Animation.frameWidth);
            int cellY = index / (spriteMapSize / curAnim.Animation.frameHeight);
            return new Rectangle(new Point(cellX * curAnim.Animation.frameWidth, cellY * curAnim.Animation.frameHeight),
                new Point(curAnim.Animation.frameWidth, curAnim.Animation.frameHeight));
        }

        protected override void Draw(GameTime gameTime)
        {
         //   GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin( SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, UserInterface.GetScaleMatrix());
                spriteBatch.Draw(background, new Vector2(0 , 0), Color.White);

            var gameObjectsToDraw = gameObjects.Where(a => a.CurrentAnimation != null);

            foreach ( var gameObject in gameObjectsToDraw )
            {
                Point screenCoordsForCell = UserInterface.convertCellCoordsToVirtualScreenCoords(new Point(gameObject.CellCoord.x, gameObject.CellCoord.y));
                spriteBatch.Draw(spriteMap1, new Vector2(screenCoordsForCell.X, screenCoordsForCell.Y), calculateSourceRectangleForSprite( gameObject.CurrentAnimation ), Color.White);

            }
//                    spriteBatch.Draw(mudBlock, new Vector2(-108 / 4 + x * 108 + 108 / 2 - 54, -108 / 4 + y * 54 + 108 / 2), sourceRectangle, Color.White, (float)MathHelper.ToRadians(degrees[y, x]), origin, 1.0f, s, 1);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
