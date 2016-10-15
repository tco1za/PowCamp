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
        private static Point convertVirtualScreenCoordsToPartitionIndices( Point point )
        {
            Point cellCoords = UserInterface.convertVirtualScreenCoordsToCellCoords(point);
            int indexX = cellCoords.X * 2;
            int indexY = cellCoords.Y * 2;
            if ((point.X - UserInterface.sidePanelWidth) % UserInterface.cellWidth > 0 )
            {
                indexX += 1;
            }
            if (point.Y % UserInterface.cellWidth > 0)
            {
                indexY += 1;
            }
            return new Point( indexX, indexY );
        }

        private static int determineWallOrientation(int partitionXindex)
        {
            return 1-(partitionXindex % 2);
        }

        public static void draw(List<GameObject> gameObjects, SpriteBatch spriteBatch)
        {
            GameObject[,] partitionIndexGrid = new GameObject[UserInterface.getNumVerticalCells() * 2, UserInterface.getNumHorizontalCells() * 2];

            foreach (var gameObject in gameObjects)
            {
                if (gameObject.CellPartition != null)
                {
                    Point partitionIndex = convertVirtualScreenCoordsToPartitionIndices(new Point( gameObject.CellPartition.partitionMidPointX, gameObject.CellPartition.partitionMidPointY));
                    partitionIndexGrid[partitionIndex.Y, partitionIndex.X] = gameObject;
                }
            }

            for (int x = UserInterface.getNumHorizontalCells() * 2 - 1; x >= 0; x--)
            {
                for (int y = 0; y < UserInterface.getNumVerticalCells() * 2; y++)
                {
                    if (partitionIndexGrid[y, x] != null)
                    {
                        Point drawPosition = new Point(partitionIndexGrid[y, x].CellPartition.partitionMidPointX, partitionIndexGrid[y, x].CellPartition.partitionMidPointY);
                        partitionIndexGrid[y, x].CurrentAnimation.index = determineWallOrientation(x);
                        partitionIndexGrid[y, x].ScreenCoord.x = drawPosition.X - 35;
                        partitionIndexGrid[y, x].ScreenCoord.y = drawPosition.Y - 64;
                        Game.drawGameObject(spriteBatch, partitionIndexGrid[y, x]);
                    }
                }
            }
        }
    }
}
