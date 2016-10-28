using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PowCamp
{
    class Prisoners
    {
        private static float prisonerSpeed = 60f;

        public static void update( GameTime gameTime )
        {
            spawnNewPrisoners(gameTime);

            List<GameObject> prisoners = Game.gameObjects.Where(item => item.GameObjectType.enumValue == GameObjectTypeEnum.prisoner).ToList();
            foreach (GameObject prisoner in prisoners)
            {
                Vector2 vectorToTarget = new Vector2(prisoner.TargetScreenCoord.x, prisoner.TargetScreenCoord.y) - new Vector2(prisoner.ScreenCoord.x, prisoner.ScreenCoord.y);
                float lengthOfvectorToTarget = vectorToTarget.Length();
                vectorToTarget.Normalize();
                if (float.IsNaN(vectorToTarget.X)) vectorToTarget.X = 0;
                if (float.IsNaN(vectorToTarget.Y)) vectorToTarget.Y = 0;

                Vector2 velocity = prisonerSpeed * vectorToTarget;

                Vector2 vectorToMoveThisFrame = velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (vectorToMoveThisFrame.Length() >= lengthOfvectorToTarget )
                {
                    Point currentCellCoords = UserInterface.convertVirtualScreenCoordsToCellCoords(new Point((int)prisoner.ScreenCoord.x, (int)prisoner.ScreenCoord.y));
                    Point nextTargetScreenCoord = PathFindingGraph.getNextTargetCell(currentCellCoords, new Point(prisoner.TargetPathIndex.x, prisoner.TargetPathIndex.y));
                    if (currentCellCoords.X == 0)
                    {
                        nextTargetScreenCoord.X = -100;
                    }
                    if (currentCellCoords.X == UserInterface.getNumHorizontalCells()-1 )
                    {
                        nextTargetScreenCoord.X = UserInterface.virtualScreenWidth + 100;
                    }
                    if (currentCellCoords.Y == 0 )
                    {
                        nextTargetScreenCoord.Y = -100;
                    }
                    if (currentCellCoords.Y == UserInterface.getNumVerticalCells()-1)
                    {
                        nextTargetScreenCoord.Y = UserInterface.virtualScreenHeight + 100;
                    }
                    prisoner.TargetScreenCoord.x = nextTargetScreenCoord.X + UserInterface.cellWidth / 2;
                    prisoner.TargetScreenCoord.y = nextTargetScreenCoord.Y + UserInterface.cellWidth / 2;
                }
                else
                {
                    Vector2 oldScreenCoord = new Vector2((float)prisoner.ScreenCoord.x, (float)prisoner.ScreenCoord.y);
                    Vector2 newScreenCoord = oldScreenCoord + vectorToMoveThisFrame;
                    prisoner.ScreenCoord.x = newScreenCoord.X;
                    prisoner.ScreenCoord.y = newScreenCoord.Y;
                    prisoner.Orientation.x = velocity.X;
                    prisoner.Orientation.y = velocity.Y;
                }
            }
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
