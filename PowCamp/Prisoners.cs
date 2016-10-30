using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Drawing.Rectangle;

namespace PowCamp
{
    class Prisoners
    {
        private static float currentMovementSpeed = 60f;
        private static float normalMovementSpeed = 60;
        private static float wallClimbingMovementSpeed = 15;

        public static bool isPrisonerInContactWithWall(GameObject prisoner, GameObject wall)
        {
            Rectangle prisonerRectangle = new Rectangle((int)prisoner.ScreenCoord.x - UserInterface.cellWidth / 2, (int)prisoner.ScreenCoord.y - UserInterface.cellWidth / 2, UserInterface.cellWidth,
                UserInterface.cellWidth);
            Rectangle wallRectangle = new Rectangle();
            if (Walls.isWallHorizontal(wall))
            {
                wallRectangle = new Rectangle((int)wall.ScreenCoord.x - UserInterface.cellWidth / 2, (int)wall.ScreenCoord.y, UserInterface.cellWidth, 1);
            }
            else
            {
                wallRectangle = new Rectangle((int)wall.ScreenCoord.x , (int)wall.ScreenCoord.y - UserInterface.cellWidth / 2, 1 , UserInterface.cellWidth);
            }
            return prisonerRectangle.Intersects(wallRectangle);
        }

        private static float movePrisonerSpecifiedDistanceTowardTarget(GameObject prisoner, float distanceToTravel)
        {
            float distanceLeftOver = 0;
            Vector2 vectorToTarget = new Vector2(prisoner.TargetScreenCoord.x, prisoner.TargetScreenCoord.y) - new Vector2(prisoner.ScreenCoord.x, prisoner.ScreenCoord.y);
            float distToTarget = vectorToTarget.Length();
            if (distanceToTravel > distToTarget)
            {
                distanceToTravel = distToTarget;
                distanceLeftOver = distanceToTravel - distToTarget;
            }
            vectorToTarget.Normalize();
            if (float.IsNaN(vectorToTarget.X)) vectorToTarget.X = 0;
            if (float.IsNaN(vectorToTarget.Y)) vectorToTarget.Y = 0;
            Vector2 vecToTravel = distanceToTravel * vectorToTarget;
            prisoner.Orientation.x = vecToTravel.X;
            prisoner.Orientation.y = vecToTravel.Y;
            prisoner.ScreenCoord.x += vecToTravel.X;
            prisoner.ScreenCoord.y += vecToTravel.Y;
            if (MyMathHelper.isValuesClose(prisoner.ScreenCoord.x, prisoner.TargetScreenCoord.x) && MyMathHelper.isValuesClose(prisoner.ScreenCoord.y, prisoner.TargetScreenCoord.y))
            {
                prisoner.ScreenCoord.x = prisoner.TargetScreenCoord.x;
                prisoner.ScreenCoord.y = prisoner.TargetScreenCoord.y;
                setNewTarget(prisoner);
            }
            return distanceLeftOver;
        }

        public static void update( GameTime gameTime )
        {
            spawnNewPrisoners(gameTime);

            List<GameObject> prisoners = Game.gameObjects.Where(item => item.GameObjectType.enumValue == GameObjectTypeEnum.prisoner).ToList();
            foreach (GameObject prisoner in prisoners)
            {
                currentMovementSpeed = normalMovementSpeed;
                List<GameObject> walls = Game.gameObjects.Where(item => item.Wall != null).ToList();
                foreach ( GameObject wall in walls  )
                {
                    if ( isPrisonerInContactWithWall( prisoner, wall ) )
                    {
                        currentMovementSpeed = wallClimbingMovementSpeed;
                    }
                }
                float distTotravel = (float)gameTime.ElapsedGameTime.TotalSeconds * currentMovementSpeed;
                while (distTotravel > 0 )
                {
                    distTotravel = movePrisonerSpecifiedDistanceTowardTarget(prisoner, distTotravel);
                }
            }
        }

        private static void setNewTarget(GameObject prisoner)
        {
            Point currentCellCoords = UserInterface.convertVirtualScreenCoordsToCellCoords(new Point((int)prisoner.ScreenCoord.x, (int)prisoner.ScreenCoord.y));
            Point nextTargetScreenCoord = PathFindingGraph.getNextTargetCell(currentCellCoords, new Point(prisoner.TargetPathIndex.x, prisoner.TargetPathIndex.y));
            if (currentCellCoords.X == 0)
            {
                nextTargetScreenCoord.X = -100;
            }
            if (currentCellCoords.X == UserInterface.getNumHorizontalCells() - 1)
            {
                nextTargetScreenCoord.X = UserInterface.virtualScreenWidth + 100;
            }
            if (currentCellCoords.Y == 0)
            {
                nextTargetScreenCoord.Y = -100;
            }
            if (currentCellCoords.Y == UserInterface.getNumVerticalCells() - 1)
            {
                nextTargetScreenCoord.Y = UserInterface.virtualScreenHeight + 100;
            }
            prisoner.TargetScreenCoord.x = nextTargetScreenCoord.X + UserInterface.cellWidth / 2;
            prisoner.TargetScreenCoord.y = nextTargetScreenCoord.Y + UserInterface.cellWidth / 2;
        }

        private static void spawnNewPrisoners(GameTime gameTime)
        {
            Game.scene.timeSinceLastPrisonerSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Game.scene.timeSinceLastPrisonerSpawn > Game.scene.timeToNextPrisonerSpawn)
            {
                GameObject newPrisoner = DataAccess.instantiateEntity(GameObjectTypeEnum.prisoner);
                Point targetGraphIndex = PathFindingGraph.getRandomGraphEdgeNode();
                newPrisoner.TargetPathIndex.x = targetGraphIndex.X;
                newPrisoner.TargetPathIndex.y = targetGraphIndex.Y;
                newPrisoner.ScreenCoord.x = UserInterface.convertCellCoordsToVirtualScreenCoords(new Point( 15, 10) ).X + UserInterface.cellWidth / 2;
                newPrisoner.ScreenCoord.y = UserInterface.convertCellCoordsToVirtualScreenCoords(new Point(15, 10)).Y + UserInterface.cellWidth / 2;
                newPrisoner.TargetScreenCoord.x = newPrisoner.ScreenCoord.x;
                newPrisoner.TargetScreenCoord.y = newPrisoner.ScreenCoord.y;
                Game.gameObjects.Add(newPrisoner);
                Game.scene.timeSinceLastPrisonerSpawn = 0f;
                Game.scene.timeToNextPrisonerSpawn = 0.5f;
            }
        }
    }
}
