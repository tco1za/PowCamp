using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowCamp
{
    class Guard
    {
        private static float movementSpeed = 60f;

        private static bool isValuesClose(double val1, double val2)  // TODO: move to helper class
        {
            double threshold = 0.1f;
            return (Math.Abs(val1 - val2) < threshold);
        } 

        private static float moveGuardSpecifiedDistanceTowardsTargetCell(GameObject guard, float distanceToTravelThisFrame, List<Point> cellsVisitedAlongPatrolRoute)
        {
            float distanceLeftOver = 0;
            int targetX = UserInterface.convertCellCoordsToVirtualScreenCoords(cellsVisitedAlongPatrolRoute[guard.PatrolRoute.targetCellIndex]).X + UserInterface.cellWidth / 2;
            int targetY = UserInterface.convertCellCoordsToVirtualScreenCoords(cellsVisitedAlongPatrolRoute[guard.PatrolRoute.targetCellIndex]).Y + UserInterface.cellWidth / 2;
            Vector2 vectorToTarget = new Vector2((float)targetX - (float)guard.ScreenCoord.x, (float)targetY - (float)guard.ScreenCoord.y);

            float distToTarget = vectorToTarget.Length();
            if (distanceToTravelThisFrame > distToTarget)
            {
                distanceToTravelThisFrame = distToTarget;
                distanceLeftOver = distanceToTravelThisFrame - distToTarget;
            }
            vectorToTarget.Normalize();
            if (float.IsNaN(vectorToTarget.X)) vectorToTarget.X = 0;
            if (float.IsNaN(vectorToTarget.Y)) vectorToTarget.Y = 0;
            Vector2 vecToTravelThisFrame = distanceToTravelThisFrame * vectorToTarget;
            guard.ScreenCoord.x += vecToTravelThisFrame.X;
            guard.ScreenCoord.y += vecToTravelThisFrame.Y;

            guard.Orientation.x = vecToTravelThisFrame.X;
            guard.Orientation.y = vecToTravelThisFrame.Y;

            if (isValuesClose(guard.ScreenCoord.x, (double)targetX) && isValuesClose(guard.ScreenCoord.y, (double)targetY))
            {
                guard.ScreenCoord.x = (double)targetX;
                guard.ScreenCoord.y = (double)targetY;
                determineNewTargetIndexInPatrolRoute(guard, cellsVisitedAlongPatrolRoute);
            }
            return distanceLeftOver;
        }

        private static void followPatrolRoute(GameObject guard, GameTime gameTime)
        {
            List<Point> cellsVisitedAlongPatrolRoute = UserInterface.buildListOfCellsVisitedAlongTrace(guard.PatrolRoute);
            float distTotravel = (float)gameTime.ElapsedGameTime.TotalSeconds * movementSpeed;

            while (distTotravel > 0)
            { 
                distTotravel = moveGuardSpecifiedDistanceTowardsTargetCell(guard, distTotravel, cellsVisitedAlongPatrolRoute);
            }
        }

        private static void determineNewTargetIndexInPatrolRoute(GameObject guard, List<Point> cellsVisitedAlongPatrolRoute)
        {
            if (guard.PatrolRoute.direction == 0)
            {
                guard.PatrolRoute.targetCellIndex = guard.PatrolRoute.targetCellIndex + 1;
            }
            else
            {
                guard.PatrolRoute.targetCellIndex = guard.PatrolRoute.targetCellIndex - 1;
            }
            if (guard.PatrolRoute.targetCellIndex == cellsVisitedAlongPatrolRoute.Count())
            {
                if (cellsVisitedAlongPatrolRoute[0].X == cellsVisitedAlongPatrolRoute[cellsVisitedAlongPatrolRoute.Count() - 1].X
                    && cellsVisitedAlongPatrolRoute[0].Y == cellsVisitedAlongPatrolRoute[cellsVisitedAlongPatrolRoute.Count() - 1].Y)
                {
                    guard.PatrolRoute.targetCellIndex = 1;
                }
                else
                {
                    guard.PatrolRoute.targetCellIndex = cellsVisitedAlongPatrolRoute.Count() - 2;
                    guard.PatrolRoute.direction = 1;
                }
            }
            if (guard.PatrolRoute.targetCellIndex == -1)
            {
                guard.PatrolRoute.targetCellIndex = 1;
                guard.PatrolRoute.direction = 0;
            }
            if (cellsVisitedAlongPatrolRoute.Count() == 1 && guard.PatrolRoute.targetCellIndex == 1) guard.PatrolRoute.targetCellIndex = 0;
        }

        public static void update(GameTime gameTime)
        {
            List<GameObject> guards = Game.gameObjects.Where(item => item.GameObjectType.enumValue == GameObjectTypeEnum.guard || item.GameObjectType.enumValue == GameObjectTypeEnum.prisoner).ToList();
            foreach ( GameObject guard in guards )
            {
                followPatrolRoute(guard, gameTime);
            }
        }

   
    }
}
