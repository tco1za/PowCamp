using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowCamp
{
    class RemovalMarkers
    {
        public static void update(List<GameObject> gameObjects, GameTime gameTime)
        {
            List<GameObject> gameObjectsThatMustBeRemoved = gameObjects.Where(a => a.RemovalMarker != null && a.RemovalMarker.mustBeRemoved).ToList();

            foreach (GameObject gameObject in gameObjectsThatMustBeRemoved)
            {
                if (gameObject.RemovalMarker.timeSinceMarkedForRemoval > gameObject.RemovalMarker.timeToRemoval)
                {
                    gameObjects.Remove(gameObject);
                }
                else
                {
                    gameObject.RemovalMarker.timeSinceMarkedForRemoval += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
        }
    }
}
