using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowCamp
{
    class Velocities
    {
        private static void updateVelocity( GameObject gameObject, GameTime gameTime )
        {
            Vector2 velocity = new Vector2((float)gameObject.Velocity.x, (float)gameObject.Velocity.y);
            Vector2 screenCoord = new Vector2((float)gameObject.ScreenCoord.x, (float)gameObject.ScreenCoord.y);
            screenCoord = screenCoord + velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            gameObject.ScreenCoord.x = screenCoord.X;
            gameObject.ScreenCoord.y = screenCoord.Y;
        }

        public static void update( GameTime gameTime )
        {
            Game.gameObjects.Where(a => a.Velocity != null).ToList().ForEach(a => updateVelocity(a, gameTime));
        }

    }
}
