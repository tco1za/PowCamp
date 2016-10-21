using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowCamp
{
    class Prisoner
    {
        private static float timeBetweenSpawns = 5f;
        private static float timeSinceLastSpawn = 999999f;

        public static void update( GameTime gameTime )
        {
            timeSinceLastSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if ( timeSinceLastSpawn > timeBetweenSpawns )
            {
                GameObject newPrisoner = DataAccess.instantiateEntity(GameObjectTypeEnum.prisoner);
                newPrisoner.PatrolRoute.direction = 0;
                newPrisoner.PatrolRoute.startCellX = 5;
                newPrisoner.PatrolRoute.startCellY = 10;
                newPrisoner.PatrolRoute.middleCellX = 15;
                newPrisoner.PatrolRoute.middleCellY = 17;
                newPrisoner.PatrolRoute.endCellX = 50;
                newPrisoner.PatrolRoute.endCellY = 50;
                Game.gameObjects.Add(newPrisoner);
                timeSinceLastSpawn = 0f;
            }

        }
    }
}
