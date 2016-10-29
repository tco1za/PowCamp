using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMK.Cartography;
using Microsoft.Xna.Framework;

namespace PowCamp
{
    class PathFindingGraph
    {
        private static Graph graph;
        private static List<Point> graphEdgeIndexes;
        private static int xIndexStart = 0;
        private static int yIndexStart = 0;
        private static int xIndexEnd = UserInterface.getNumHorizontalCells();
        private static int yIndexEnd = UserInterface.getNumVerticalCells();

        private static int xRange = xIndexEnd - xIndexStart;

        private static int[,] arcsLookupTable;

        public static void initialize()
        {
            initializeGraph();
            initializeGraphEdgeIndices();
        }

        private static int convertCellCoordToNodeIndex( Point cellCoord )
        {
            return cellCoord.Y * xRange + cellCoord.X;
        }

        private static bool isCoordValid( Point coord)
        {
            if ( coord.X >= xIndexStart && coord.X < xIndexEnd && coord.Y >= yIndexStart && coord.Y < yIndexEnd )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void addWall( GameObject wall )
        {
            Point cellCoordOfVerticalWall = UserInterface.convertVirtualScreenCoordsToCellCoords(new Point(wall.CellPartition.partitionMidPointX, wall.CellPartition.partitionMidPointY));

            Point rightCoord = new Point(cellCoordOfVerticalWall.X, cellCoordOfVerticalWall.Y);
            Point leftCoord = new Point(cellCoordOfVerticalWall.X-1, cellCoordOfVerticalWall.Y);
            Point rightDownCoord = new Point(cellCoordOfVerticalWall.X, cellCoordOfVerticalWall.Y + 1);
            Point rightUpCoord = new Point(cellCoordOfVerticalWall.X, cellCoordOfVerticalWall.Y - 1);
            Point leftDownCoord = new Point(cellCoordOfVerticalWall.X - 1, cellCoordOfVerticalWall.Y + 1);
            Point leftUpCoord = new Point(cellCoordOfVerticalWall.X - 1, cellCoordOfVerticalWall.Y - 1);

            int rightIndex = convertCellCoordToNodeIndex(rightCoord);
            int leftIndex = convertCellCoordToNodeIndex(leftCoord);
            int rightDownIndex = convertCellCoordToNodeIndex(rightDownCoord);
            int rightUpIndex = convertCellCoordToNodeIndex(rightUpCoord);
            int leftDownIndex = convertCellCoordToNodeIndex(leftDownCoord);
            int leftUpIndex = convertCellCoordToNodeIndex(leftUpCoord);

            List<Tuple<int, int>> arcsToRemove = new List<Tuple<int, int>>();

            if (isCoordValid(rightCoord) && isCoordValid(leftUpCoord)) arcsToRemove.Add(new Tuple<int, int>(rightIndex, leftUpIndex));
            if (isCoordValid(rightCoord) && isCoordValid(leftDownCoord)) arcsToRemove.Add(new Tuple<int, int>(rightIndex, leftDownIndex));

            if (isCoordValid(rightCoord) && isCoordValid(leftUpCoord)) arcsToRemove.Add(new Tuple<int, int>(leftUpIndex, rightIndex));
            if (isCoordValid(rightCoord) && isCoordValid(leftDownCoord)) arcsToRemove.Add(new Tuple<int, int>(leftDownIndex, rightIndex));

            if (isCoordValid(leftCoord) && isCoordValid(rightUpCoord)) arcsToRemove.Add(new Tuple<int, int>(leftIndex, rightUpIndex));
            if (isCoordValid(leftCoord) && isCoordValid(rightDownCoord)) arcsToRemove.Add(new Tuple<int, int>(leftIndex, rightDownIndex));

            if (isCoordValid(leftCoord) && isCoordValid(rightUpCoord)) arcsToRemove.Add(new Tuple<int, int>(rightUpIndex, leftIndex));
            if (isCoordValid(leftCoord) && isCoordValid(rightDownCoord)) arcsToRemove.Add(new Tuple<int, int>(rightDownIndex, leftIndex));

            foreach ( Tuple<int,int> arcTuple in arcsToRemove )
            {
                if ( arcTuple.Item1 >= 0 && arcTuple.Item1 < graph.Nodes.Count && arcTuple.Item2 >= 0 && arcTuple.Item2 < graph.Nodes.Count )
                {
                    ((Arc)graph.Arcs[arcsLookupTable[arcTuple.Item1, arcTuple.Item2]]).Passable = false;
                }
            }
        }

        private static void initializeGraphEdgeIndices()
        {
            graphEdgeIndexes = new List<Point>();

            int topLeftCornerX = 0;
            int topLeftCornerY = 0;
            int bottomRightCornerX = UserInterface.getNumHorizontalCells()-1;
            int bottomRightCornerY = UserInterface.getNumVerticalCells()-1;

            int y = topLeftCornerY;
            int x = topLeftCornerX;
            for (x = topLeftCornerX + 1; x <= bottomRightCornerX - 1; x++)
            {
                graphEdgeIndexes.Add(new Point(x, y));
            }
            y = bottomRightCornerY;
            for (x = topLeftCornerX + 1; x <= bottomRightCornerX - 1; x++)
            {
                graphEdgeIndexes.Add(new Point(x, y));
            }
            x = topLeftCornerX;
            for (y = topLeftCornerY; y <= bottomRightCornerY; y++)
            {
                graphEdgeIndexes.Add(new Point(x, y));
            }
            x = bottomRightCornerX;
            for (y = topLeftCornerY; y <= bottomRightCornerY; y++)
            {
                graphEdgeIndexes.Add(new Point(x, y));
            }
        }

        private static void initializeGraph()
        {
            initializeNodes();
            initializeArcs();
        }

        private static void initializeArcs()
        {
            arcsLookupTable = new int[graph.Nodes.Count, graph.Nodes.Count];
            for (int y = yIndexStart; y < yIndexEnd; y++)
            {
                for (int x = xIndexStart; x < xIndexEnd; x++)
                {
                    List<Point> indexOffsets = new List<Point>();
                    indexOffsets.Add(new Point(-1, -1));
                    indexOffsets.Add(new Point(0, -1));
                    indexOffsets.Add(new Point(1, -1));
                    indexOffsets.Add(new Point(-1, 0));
                    indexOffsets.Add(new Point(1, 0));
                    indexOffsets.Add(new Point(-1, 1));
                    indexOffsets.Add(new Point(0, 1));
                    indexOffsets.Add(new Point(1, 1));
                    foreach (Point offset in indexOffsets)
                    {
                        int xIndex = offset.X + x;
                        int yIndex = offset.Y + y;

                        if (xIndex >= xIndexStart && xIndex < xIndexEnd && yIndex >= yIndexStart && yIndex < yIndexEnd)
                        {
                            Arc arcAdded = graph.AddArc((Node)graph.Nodes[y * xRange + x], (Node)graph.Nodes[yIndex * xRange + xIndex], 1);
                            arcsLookupTable[y * xRange + x, yIndex * xRange + xIndex] = graph.Arcs.Count - 1;
                        }
                    }
                }
            }
        }

        private static void initializeNodes()
        {
            graph = new Graph();
            for (int y = yIndexStart; y < yIndexEnd; y++)
            {
                for (int x = xIndexStart; x < xIndexEnd; x++)
                {
                    graph.AddNode(x * UserInterface.cellWidth, y * UserInterface.cellWidth, 0);
                }
            }
        }

        public static Point getRandomGraphEdgeNode()
        {
            return graphEdgeIndexes[Game.randomNumberGenerator.Next(graphEdgeIndexes.Count)];
        }

        public static Point getNextTargetCell( Point currentCell, Point targetCell  )
        {
            AStar aStar = new AStar(graph);
            aStar.SearchPath((Node)graph.Nodes[(currentCell.Y) * xRange + (currentCell.X)], (Node)graph.Nodes[(targetCell.Y) * xRange + (targetCell.X)]);
            int returnedIndex = 0;
            if (aStar.PathByCoordinates.Count() > 1)
            {
                returnedIndex = 1;
            }
            return new Point((int)aStar.PathByCoordinates[returnedIndex].X + UserInterface.sidePanelWidth, (int)aStar.PathByCoordinates[returnedIndex].Y);
        }
    }
}
