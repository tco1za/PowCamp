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

        public static void initialize()
        {
            initializeGraph();
            initializeGraphEdgeIndices();
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
            graph = new Graph();
            for (int y = yIndexStart; y < yIndexEnd; y++)
            {
                for (int x = xIndexStart; x < xIndexEnd; x++)
                {
                    graph.AddNode(x * UserInterface.cellWidth, y * UserInterface.cellWidth, 0);
                }
            }
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
                            graph.AddArc((Node)graph.Nodes[y * xRange + x], (Node)graph.Nodes[yIndex * xRange + xIndex], 1);
                        }
                    }
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
