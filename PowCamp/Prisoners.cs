using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowCamp
{
    class Prisoners
    {
        public static Vector2 convertPolarCoordsToCartesian( float angleInRadians, float distance )  // TODO: move to helper class
        {
            return new Vector2( (float)Math.Cos(angleInRadians), -(float)Math.Sin(angleInRadians)) * distance;
        }

        public static void update( GameTime gameTime )
        {
            Game.scene.timeSinceLastPrisonerSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Game.scene.timeSinceLastPrisonerSpawn > Game.scene.timeToNextPrisonerSpawn)
            {
                GameObject newPrisoner = DataAccess.instantiateEntity(GameObjectTypeEnum.prisoner);
                newPrisoner.PatrolRoute.direction = 0;
                newPrisoner.PatrolRoute.startCellX = UserInterface.getNumHorizontalCells()/2;
                newPrisoner.PatrolRoute.startCellY = UserInterface.getNumVerticalCells()/2;

                float angleForMidPoint = (float)Game.randomNumberGenerator.Next(360);
                float distanceFromFirstPoint = (float)5;

                Vector2 coordsForMidPoint = convertPolarCoordsToCartesian(MathHelper.ToRadians(angleForMidPoint), distanceFromFirstPoint);

                newPrisoner.PatrolRoute.startCellX = (int)coordsForMidPoint.X + newPrisoner.PatrolRoute.startCellX;
                newPrisoner.PatrolRoute.startCellY = (int)coordsForMidPoint.Y + newPrisoner.PatrolRoute.startCellY;

                //newPrisoner.PatrolRoute.middleCellX = (int)coordsForMidPoint.X;
                //newPrisoner.PatrolRoute.middleCellY = (int)coordsForMidPoint.Y;
                newPrisoner.PatrolRoute.endCellX = Game.randomNumberGenerator.Next(UserInterface.getNumHorizontalCells());
                newPrisoner.PatrolRoute.endCellY = Game.randomNumberGenerator.Next(UserInterface.getNumVerticalCells());
                PatrolRoutes.placeGameObjectOnFirstCellOfRoute(newPrisoner);
                Game.gameObjects.Add(newPrisoner);
                Game.scene.timeSinceLastPrisonerSpawn = 0f;
                Game.scene.timeToNextPrisonerSpawn = 0.2f;
            }

        }

    }
}
