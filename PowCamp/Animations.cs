using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowCamp
{
    class Animations
    {
        public static void update( GameTime gameTime )
        {
            List<GameObject> gameObjectsWithAnimations = Game.gameObjects.Where(a => a.CurrentAnimation != null).ToList();

            foreach ( GameObject gameObject in gameObjectsWithAnimations )
            {
                gameObject.CurrentAnimation.timeSinceLastFrameChange += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if ( gameObject.CurrentAnimation.timeSinceLastFrameChange > gameObject.CurrentAnimation.Animation.timeBetweenFrames )
                {
                    gameObject.CurrentAnimation.index++;
                    if (gameObject.CurrentAnimation.index >= gameObject.CurrentAnimation.Animation.count)
                    {
                        gameObject.CurrentAnimation.index = 0;
                    }
                    gameObject.CurrentAnimation.timeSinceLastFrameChange = 0;
                }
            }
        }
    }
}
