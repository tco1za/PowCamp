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
        public static void changeAnimation(GameObject gameObject, AnimationEnum enumValue)
        {
            if (gameObject.CurrentAnimation.Animation.enumValue != enumValue)
            {
                gameObject.CurrentAnimation.Animation = DataAccess.db.Animations.Where(item => item.enumValue == enumValue).FirstOrDefault();
                gameObject.CurrentAnimation.index = 0;
                gameObject.CurrentAnimation.timeSinceLastFrameChange = 0;
            }
        }

        public static bool isCurrentAnimationAtEndOfLastFrame( CurrentAnimation currentAnimation )
        {
            return (currentAnimation.index == currentAnimation.Animation.count - 1 && currentAnimation.timeSinceLastFrameChange > currentAnimation.Animation.timeBetweenFrames);
        }

        public static void update( GameTime gameTime )
        {
            List<GameObject> gameObjectsWithAnimationsThatMustAnimate = Game.gameObjects.Where(a => a.CurrentAnimation != null && a.CurrentAnimation.Animation.mustAnimate).ToList();

            foreach ( GameObject gameObject in gameObjectsWithAnimationsThatMustAnimate )
            {
                gameObject.CurrentAnimation.timeSinceLastFrameChange += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if ( gameObject.CurrentAnimation.timeSinceLastFrameChange > gameObject.CurrentAnimation.Animation.timeBetweenFrames )
                {
                    gameObject.CurrentAnimation.index++;
                    if (gameObject.CurrentAnimation.index >= gameObject.CurrentAnimation.Animation.count)
                    {
                        if (gameObject.CurrentAnimation.Animation.mustRepeat)
                        {
                            gameObject.CurrentAnimation.index = 0;
                            gameObject.CurrentAnimation.timeSinceLastFrameChange = 0;
                        }
                        else
                        {
                            gameObject.CurrentAnimation.index = gameObject.CurrentAnimation.Animation.count - 1;
                        }
                    }
                    else
                    {
                        gameObject.CurrentAnimation.timeSinceLastFrameChange = 0;
                    }
                }
            }
        }
    }
}
