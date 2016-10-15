using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowCamp
{
    class Guard
    {
        private double movementSpeed = 10f;

        private bool isValuesClose(double val1, double val2)  // TODO: move to helper class
        {
            double threshold = 1f;
            return (Math.Abs(val1 - val2) < threshold);
        } 

        private void followPatrolRoute(GameObject guard, GameTime gameTime)
        {
            double distToTarget = 0;

            if (isValuesClose(guard.ScreenCoord.x, (double)guard.PatrolRoute.targetX) && isValuesClose(guard.ScreenCoord.x, (double)guard.PatrolRoute.targetX))
            {
                // get new target
                List<Point> cellsVisitedAlongPatrolRoute = UserInterface.buildListOfCellsVisitedAlongTrace(guard.PatrolRoute);


                
            }

            if (isValuesClose(guard.ScreenCoord.x, (double)guard.PatrolRoute.targetX))
            {
                distToTarget = (double)guard.PatrolRoute.targetY - guard.ScreenCoord.y;
            }

            if (isValuesClose(guard.ScreenCoord.y, (double)guard.PatrolRoute.targetY))
            {
                distToTarget = (double)guard.PatrolRoute.targetX - guard.ScreenCoord.x;
            }

            double distTotravelThisFrame = gameTime.ElapsedGameTime.TotalSeconds * movementSpeed;
            if ( distToTarget < 0 )
            {
                distTotravelThisFrame = -1 * distTotravelThisFrame;
            }
            if (Math.Abs(distTotravelThisFrame) > Math.Abs(distToTarget))
            {
                distTotravelThisFrame = distToTarget;
            }

            if (isValuesClose(guard.ScreenCoord.x, (double)guard.PatrolRoute.targetX))
            {
                guard.ScreenCoord.y = guard.ScreenCoord.y + distTotravelThisFrame;
            }

            if (isValuesClose(guard.ScreenCoord.y, (double)guard.PatrolRoute.targetY))
            {
                guard.ScreenCoord.x = guard.ScreenCoord.x + distTotravelThisFrame;
            }


        }

        public void update(GameTime gameTime)
        {
            List<GameObject> guards = Game.gameObjects.Where(item => item.GameObjectType.enumValue == GameObjectTypeEnum.guard).ToList();
            foreach ( GameObject guard in guards )
            {
                followPatrolRoute(guard, gameTime);
            }

        }

    }
}
