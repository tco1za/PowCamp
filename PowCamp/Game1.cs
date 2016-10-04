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
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {

        private Texture2D grassBlock;
        private Texture2D mudBlock;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
  


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //            createGameObjectTypes();

            //graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            //graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            


       //     DataAccess.createComponentDependencies();

         //   DataAccess.createGameObjectTypes();
            DataAccess.createAndLinkToGameObjectsAllDependenciesThatDontExist();


            List<GameObject> gameObjects = new List<GameObject>();

         //   gameObjects = DataAccess.loadSaveGame("10/3/2016 12:00:00 AM");

            //gameObjects.Add(DataAccess.instantiateEntity(GameObjectTypeEnum.grassBlock));
            //Debug.WriteLine(gameObjects[0].ScreenCoord.x);
            //gameObjects[0].ScreenCoord.x = 456;
       //     DataAccess.saveGame(gameObjects);


            // cell Dimensions = 54x54


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            mudBlock = Content.Load<Texture2D>("backTest");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

//public virtual Point PointToScreen(Point point)
//        {
//            var matrix = Matrix.Invert(GetScaleMatrix());
//            return Vector2.Transform(point.ToVector2(), matrix).ToPoint();
//        }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

        //    Rectangle sourceRectangle = new Rectangle(0, 0, grassBlock.Width, grassBlock.Height);
            var scaleX = (float)800 / 1920;
            var scaleY = (float)600 / 1080;
            var matrix = Matrix.CreateScale(scaleX, scaleY, 1.0f);

            spriteBatch.Begin( SpriteSortMode.Immediate, BlendState.Additive, null, null, null, null, matrix);
         //   Vector2 origin = new Vector2(grassBlock.Width / 2, grassBlock.Height/2);

//            Vector2 origin = new Vector2(0, 0);


//                    //spriteBatch.Draw(grassBlock, new Vector2(-108 / 4 + x * 108 , -108 / 4 + y * 54), Color.White);

                spriteBatch.Draw(mudBlock, new Vector2(0 , 0), Color.White);


//            for (int y = 0; y < 20; y++)
//            {
//                for (int x = 0; x < 35; x++)
//                {

//                    //spriteBatch.Draw(grassBlock, new Vector2(-108 / 4 + x * 108 , -108 / 4 + y * 54), Color.White);

//                    //spriteBatch.Draw(grassBlock, new Vector2(-108 / 4 + x * 108, -108 / 4 + y * 54), sourceRectangle, Color.White, (float)MathHelper.ToRadians(degrees[y,x]), origin, 1.0f, SpriteEffects.None, 1);

//                    SpriteEffects s = new SpriteEffects();
//                    if ( flip[y,x] == 1)
//                    {
//                        s = SpriteEffects.FlipHorizontally;
//                    }
//                    else
//                    {
//                        s = SpriteEffects.None;
//                    }
//                    spriteBatch.Draw(grassBlock, new Vector2(-108 / 4 + x * 108 + 108/2, -108 / 4 + y * 54 + 108 / 2), sourceRectangle, Color.White, (float)MathHelper.ToRadians(degrees[y,x]), origin, 1.0f, s, 1);
//                    spriteBatch.Draw(mudBlock, new Vector2(-108 / 4 + x * 108 + 108 / 2 - 54, -108 / 4 + y * 54 + 108 / 2), sourceRectangle, Color.White, (float)MathHelper.ToRadians(degrees[y, x]), origin, 1.0f, s, 1);

////                    spriteBatch.Draw(mudBlock, new Vector2(-108 / 4 + x * 108 -54, -108 / 4 + y * 54), Color.White);
//                }
//            }

            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
