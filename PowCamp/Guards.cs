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
    class Guards
    {
        private static float movementSpeed = 60f;
        private static float patrolTurningRate = 60f;  // degrees per second
        private static float targetTrackingTurningRate = 90f;  // degrees per second
        private static float guardVisionConeWidth = 90f; // degrees
        private static float sightRange = 300f;

        private static float moveGuardSpecifiedDistanceTowardsTargetCell(GameObject guard, float distanceToTravelThisFrame, List<Point> cellsVisitedAlongPatrolRoute)
        {
            float distanceLeftOver = 0;
            int targetX = getTargetX(guard, cellsVisitedAlongPatrolRoute);
            int targetY = getTargetY(guard, cellsVisitedAlongPatrolRoute);
            Vector2 vectorToTarget = getVectorToTarget(guard, targetX, targetY);

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

            if (MyMathHelper.isValuesClose(guard.ScreenCoord.x, (double)targetX, 0.1f) && MyMathHelper.isValuesClose(guard.ScreenCoord.y, (double)targetY, 0.1f))
            {
                guard.ScreenCoord.x = targetX;
                guard.ScreenCoord.y = targetY;
                determineNewTargetIndexInPatrolRoute(guard, cellsVisitedAlongPatrolRoute);
                Vector2 vectorToNewTarget = getVectorToTarget(guard, getTargetX(guard, cellsVisitedAlongPatrolRoute), getTargetY(guard, cellsVisitedAlongPatrolRoute));
                if ( MyMathHelper.angleBetweenTwoVectorsInDegrees( vectorToNewTarget, vectorToTarget ) > 45 )
                {
                    guard.Guard.patrolState = GuardPatrollingState.turning;
                }
            }
            return distanceLeftOver;
        }

        private static Vector2 getVectorToTarget(GameObject guard, int targetX, int targetY)
        {
            return new Vector2((float)targetX - (float)guard.ScreenCoord.x, (float)targetY - (float)guard.ScreenCoord.y);
        }

        private static int getTargetY(GameObject guard, List<Point> cellsVisitedAlongPatrolRoute)
        {
            return UserInterface.convertCellCoordsToVirtualScreenCoords(cellsVisitedAlongPatrolRoute[guard.PatrolRoute.targetCellIndex]).Y + UserInterface.cellWidth / 2;
        }

        private static int getTargetX(GameObject guard, List<Point> cellsVisitedAlongPatrolRoute)
        {
            return UserInterface.convertCellCoordsToVirtualScreenCoords(cellsVisitedAlongPatrolRoute[guard.PatrolRoute.targetCellIndex]).X + UserInterface.cellWidth / 2;
        }

        private static bool isPrisonerInSight( GameObject guard, GameObject prisoner )
        {
            Vector2 vecToPrisoner = getVecToPrisoner( guard, prisoner );
            float angle = MyMathHelper.angleBetweenTwoVectorsInDegrees(vecToPrisoner, new Vector2(guard.Orientation.x, guard.Orientation.y));
            float distance = vecToPrisoner.Length();
            if (angle < guardVisionConeWidth / 2 && distance < sightRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static Vector2 getVecToPrisoner(GameObject guard, GameObject prisoner)
        {
            return new Vector2(prisoner.ScreenCoord.x - guard.ScreenCoord.x, prisoner.ScreenCoord.y - guard.ScreenCoord.y);
        }

        private static Vector2 getVectorToClosestPrisonerInSight(GameObject guard, List<GameObject> prisonersInSight)
        {
            List<GameObject> sortedPrisonersInSight = prisonersInSight.OrderBy(a => getVecToPrisoner( guard, a ).Length()).ToList();
            return getVecToPrisoner(guard, sortedPrisonersInSight[0]);
        }

        private static List<GameObject> getListOfPrisonersInSight( GameObject guard )
        {
            List<GameObject> prisoners = Game.gameObjects.Where(item => item.GameObjectType.enumValue == GameObjectTypeEnum.prisoner).ToList();
            List<GameObject> prisonersInSight = new List<GameObject>();
            foreach ( GameObject prisoner in prisoners )
            {
                if ( isPrisonerInSight( guard, prisoner ) )
                {
                    prisonersInSight.Add(prisoner);
                }
            }
            return prisonersInSight;
        }

        private static void followPatrolRoute(GameObject guard, GameTime gameTime)
        {
            List<Point> cellsVisitedAlongPatrolRoute = UserInterface.buildListOfCellsVisitedAlongTrace(guard.PatrolRoute);

            if (cellsVisitedAlongPatrolRoute.Count == 1)
            {
                guard.Guard.patrolState = GuardPatrollingState.turning;
            }

            float distTotravel = (float)gameTime.ElapsedGameTime.TotalSeconds * movementSpeed;

            if (guard.Guard.patrolState == GuardPatrollingState.walking)
            {
                while (distTotravel > 0)
                {
                    distTotravel = moveGuardSpecifiedDistanceTowardsTargetCell(guard, distTotravel, cellsVisitedAlongPatrolRoute);
                }
            }
            if (guard.Guard.patrolState == GuardPatrollingState.turning)
            {
                Vector2 vectorToTarget = getVectorToTarget(guard, getTargetX(guard, cellsVisitedAlongPatrolRoute), getTargetY(guard, cellsVisitedAlongPatrolRoute));
                turnGuardTowardPoint(guard, gameTime, vectorToTarget, true, cellsVisitedAlongPatrolRoute.Count > 1);
            }
        }

        private static void turnGuardTowardPoint(GameObject guard, GameTime gameTime, Vector2 point, bool pointIsPatrolRouteTarget, bool patrolRouteHasMoreThanOneCell = false)
        {
            float targetOrientation = MyMathHelper.constrainAngleInDegreesToPositive360(
                MathHelper.ToDegrees(MyMathHelper.convertVectorToAngleOfRotationInRadians(point.X, point.Y)));
            float currentOrientation = MyMathHelper.constrainAngleInDegreesToPositive360(
                MathHelper.ToDegrees(MyMathHelper.convertVectorToAngleOfRotationInRadians(guard.Orientation.x, guard.Orientation.y)));
            float degreesToRotate = patrolTurningRate * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (!pointIsPatrolRouteTarget)
            {
                degreesToRotate = targetTrackingTurningRate * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            float positiveDist = MyMathHelper.distanceAlongPositiveDirectionBetweenTwoAngles(currentOrientation, targetOrientation);
            float negativeDist = MyMathHelper.distanceAlongNegativeDirectionBetweenTwoAngles(currentOrientation, targetOrientation);
            float smallestDist = Math.Min(positiveDist, negativeDist);

            if (positiveDist > negativeDist)
            {
                degreesToRotate = degreesToRotate * -1;
            }

            if (pointIsPatrolRouteTarget && !patrolRouteHasMoreThanOneCell)
            {
                degreesToRotate = Math.Abs(degreesToRotate);
            }

            if (Math.Abs(degreesToRotate) < Math.Abs(smallestDist) || (pointIsPatrolRouteTarget && !patrolRouteHasMoreThanOneCell))
            {
                currentOrientation += degreesToRotate;
                Vector2 orientationVector = MyMathHelper.convertPolarCoordsToCartesian(MathHelper.ToRadians(currentOrientation), 1);
                guard.Orientation.x = orientationVector.X;
                guard.Orientation.y = orientationVector.Y;
            }
            else
            {
                currentOrientation = targetOrientation;
                if (pointIsPatrolRouteTarget)
                {
                    guard.Guard.patrolState = GuardPatrollingState.walking;
                }
                else
                {
                    guard.Guard.trackingTargetState = GuardTrackingTargetState.shooting;
                }
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
            List<GameObject> guards = Game.gameObjects.Where(item => item.Guard != null).ToList();
            foreach ( GameObject guard in guards )
            {
                if (guard.Guard.state == GuardState.patrolling)
                {
                    followPatrolRoute(guard, gameTime);
                    if ( getListOfPrisonersInSight( guard ).Count > 0 )
                    {
                        guard.Guard.state = GuardState.trackingTarget;
                        guard.Guard.trackingTargetState = GuardTrackingTargetState.slewing;
                    }
                }
                if (guard.Guard.state == GuardState.trackingTarget)
                {
                    if (guard.Guard.trackingTargetState == GuardTrackingTargetState.slewing)
                    {
                        List<GameObject> prisonersInSight = getListOfPrisonersInSight(guard);
                        if (prisonersInSight.Count > 0)
                        {
                            Vector2 vectorToTarget = getVectorToClosestPrisonerInSight(guard, prisonersInSight);
                           // Vector2 vectorToTarget = new Vector2(Game.currentMouseState.X - guard.ScreenCoord.x, Game.currentMouseState.Y - guard.ScreenCoord.y);
                            turnGuardTowardPoint(guard, gameTime, vectorToTarget, false);
                        }
                        else
                        {
                            guard.Guard.state = GuardState.patrolling;
                            guard.Guard.patrolState = GuardPatrollingState.turning;
                        }
                    }
                    if (guard.Guard.trackingTargetState == GuardTrackingTargetState.shooting)
                    {
                        
                    }
                }
            }
        }

   
    }
}
