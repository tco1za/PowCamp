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
            List<GameObject>[,] partitionIndexGrid = new List<GameObject>[UserInterface.getNumVerticalCells() * 2, UserInterface.getNumHorizontalCells() * 2];

            foreach (var gameObject in gameObjects)
            {
                if (gameObject.CellPartition != null)
                {
                    Point partitionIndex = convertVirtualScreenCoordsToPartitionIndices(new Point(gameObject.CellPartition.partitionMidPointX, gameObject.CellPartition.partitionMidPointY));
                    addObjectToPartitionIndex(partitionIndexGrid, gameObject, partitionIndex);
                }
                else
                {
                    if (gameObject.ScreenCoord != null)
                    {
                        Point partitionIndex = UserInterface.convertVirtualScreenCoordsToCellCoords(new Point((int)gameObject.ScreenCoord.x, (int)gameObject.ScreenCoord.y));
                        partitionIndex.X = partitionIndex.X * 2;
                        partitionIndex.Y = partitionIndex.Y * 2;
                        if (partitionIndex.X > 0 && partitionIndex.Y > 0 && partitionIndex.X < UserInterface.getNumHorizontalCells() * 2
                            && partitionIndex.Y < UserInterface.getNumVerticalCells() * 2)
                        {
                            addObjectToPartitionIndex(partitionIndexGrid, gameObject, partitionIndex);
                        }
                    }
                }
            }
           
            for (int y = 0; y < UserInterface.getNumVerticalCells() * 2; y++)
            {
                for (int x = UserInterface.getNumHorizontalCells() * 2 - 1; x >= 0; x--)
                {
                    if (partitionIndexGrid[y, x] != null)
                    {
                        foreach (GameObject gameObject in partitionIndexGrid[y, x])
                        {
                            if (gameObject.CellPartition != null)
                            { 
                                Point drawPosition = new Point(gameObject.CellPartition.partitionMidPointX, gameObject.CellPartition.partitionMidPointY);
                                gameObject.CurrentAnimation.index = determineWallOrientation(x);
                                gameObject.ScreenCoord.x = drawPosition.X + 0;
                                gameObject.ScreenCoord.y = drawPosition.Y - 0;
                                Game.drawGameObject(spriteBatch, gameObject);
                            }
                            else
                            {
                                Game.drawGameObject(spriteBatch, gameObject);
                            }
                        }
                    }
                }
            }
        }

        private static void addObjectToPartitionIndex(List<GameObject>[,] partitionIndexGrid, GameObject gameObject, Point partitionIndex)
        {
            if (partitionIndexGrid[partitionIndex.Y, partitionIndex.X] == null) partitionIndexGrid[partitionIndex.Y, partitionIndex.X] = new List<GameObject>();
            partitionIndexGrid[partitionIndex.Y, partitionIndex.X].Add(gameObject);
        }
    }
}
