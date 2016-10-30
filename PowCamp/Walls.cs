using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowCamp
{
    class Walls
    {
        public static bool isWallHorizontal( GameObject wall )
        {
            return ((wall.CellPartition.partitionMidPointX - UserInterface.sidePanelWidth) % UserInterface.cellWidth) > 0;
        }

        public static void draw(List<GameObject> gameObjects, SpriteBatch spriteBatch)
        {
            List<GameObject> walls = gameObjects.Where(item => item.CellPartition != null).ToList();

            foreach (GameObject wall in walls)
            {
                if ( isWallHorizontal( wall) )
                {
                    wall.CurrentAnimation.index = 0;
                }
                else
                {
                    wall.CurrentAnimation.index = 1;
                }
                Game.drawGameObject(spriteBatch, wall);
            }
        }
    }
}
