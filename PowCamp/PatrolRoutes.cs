using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowCamp
{
    class PatrolRoutes
    {
        public static bool isPatrolRouteObstructed(List<Point> cellsToVisitAlongPatrolRoute)
        {
            List<GameObject> walls = Game.gameObjects.Where(a => a.Wall != null).ToList();

            int index = 0;
            foreach ( Point cell in cellsToVisitAlongPatrolRoute  )
            {
                if (index < cellsToVisitAlongPatrolRoute.Count - 1)
                {
                     Point partitionMidPointBetweenCells = getPartitionMidpointBetweenCells(cellsToVisitAlongPatrolRoute, index);
                    foreach ( GameObject wall in walls )
                    {
                        if ( partitionMidPointBetweenCells.X == wall.CellPartition.partitionMidPointX && partitionMidPointBetweenCells.Y == wall.CellPartition.partitionMidPointY)
                        {
                            return true;
                        }
                    }
                    index++;
                }
            }
            return false;
        }

        private static Point getPartitionMidpointBetweenCells(List<Point> cellsToVisitAlongPatrolRoute, int index)
        {
            Point nextCellScreenCoords = UserInterface.convertCellCoordsToVirtualScreenCoords(cellsToVisitAlongPatrolRoute[index + 1]);
            Point currentCellScreenCoords = UserInterface.convertCellCoordsToVirtualScreenCoords(cellsToVisitAlongPatrolRoute[index]);
            Point partitionMidPointBetweenCells = new Point();
            if (cellsToVisitAlongPatrolRoute[index + 1].X > cellsToVisitAlongPatrolRoute[index].X)
            {
                partitionMidPointBetweenCells.X = nextCellScreenCoords.X;
                partitionMidPointBetweenCells.Y = nextCellScreenCoords.Y + UserInterface.cellWidth / 2;
            }
            if (cellsToVisitAlongPatrolRoute[index + 1].X < cellsToVisitAlongPatrolRoute[index].X)
            {
                partitionMidPointBetweenCells.X = currentCellScreenCoords.X;
                partitionMidPointBetweenCells.Y = currentCellScreenCoords.Y + UserInterface.cellWidth / 2;
            }
            if (cellsToVisitAlongPatrolRoute[index + 1].Y > cellsToVisitAlongPatrolRoute[index].Y)
            {
                partitionMidPointBetweenCells.X = nextCellScreenCoords.X + UserInterface.cellWidth / 2;
                partitionMidPointBetweenCells.Y = nextCellScreenCoords.Y;
            }
            if (cellsToVisitAlongPatrolRoute[index + 1].Y < cellsToVisitAlongPatrolRoute[index].Y)
            {
                partitionMidPointBetweenCells.X = currentCellScreenCoords.X + UserInterface.cellWidth / 2;
                partitionMidPointBetweenCells.Y = currentCellScreenCoords.Y;
            }
            return partitionMidPointBetweenCells;
        }

        public static List<Point> buildListOfCellsVisitedAlongTrace(PatrolRoute trace)  // TODO: break this method up in two
        {
            List<Point> cellsToVisitAlongPatrolRoute = new List<Point>();

            int horizontalDistanceBetweenStartAndMiddlePatrolRouteCells = Math.Abs(trace.middleCellX - trace.startCellX);
            int verticalDistanceBetweenStartAndMiddlePatrolRouteCells = Math.Abs(trace.middleCellY - trace.startCellY);
            if (horizontalDistanceBetweenStartAndMiddlePatrolRouteCells > verticalDistanceBetweenStartAndMiddlePatrolRouteCells)
            {
                foreach (int x in MyMathHelper.interpolatePoints(trace.startCellX, trace.middleCellX, false))
                {
                    cellsToVisitAlongPatrolRoute.Add(new Point(x, trace.startCellY));
                }
                foreach (int y in MyMathHelper.interpolatePoints(trace.startCellY, trace.middleCellY, false))
                {
                    cellsToVisitAlongPatrolRoute.Add(new Point(trace.middleCellX, y));
                }
            }
            else
            {
                foreach (int y in MyMathHelper.interpolatePoints(trace.startCellY, trace.middleCellY, false))
                {
                    cellsToVisitAlongPatrolRoute.Add(new Point(trace.startCellX, y));
                }
                foreach (int x in MyMathHelper.interpolatePoints(trace.startCellX, trace.middleCellX, false))
                {
                    cellsToVisitAlongPatrolRoute.Add(new Point(x, trace.middleCellY));
                }
            }

            int horizontalDistanceBetweenMiddleAndEndPatrolRouteCells = Math.Abs(trace.middleCellX - trace.endCellX);
            int verticalDistanceBetweenMiddleAndEndPatrolRouteCells = Math.Abs(trace.middleCellY - trace.endCellY);
            if (horizontalDistanceBetweenMiddleAndEndPatrolRouteCells > verticalDistanceBetweenMiddleAndEndPatrolRouteCells)
            {
                foreach (int x in MyMathHelper.interpolatePoints(trace.middleCellX, trace.endCellX, false))
                {
                    cellsToVisitAlongPatrolRoute.Add(new Point(x, trace.middleCellY));
                }
                foreach (int y in MyMathHelper.interpolatePoints(trace.middleCellY, trace.endCellY, true))
                {
                    cellsToVisitAlongPatrolRoute.Add(new Point(trace.endCellX, y));
                }
            }
            else
            {
                foreach (int y in MyMathHelper.interpolatePoints(trace.middleCellY, trace.endCellY, false))
                {
                    cellsToVisitAlongPatrolRoute.Add(new Point(trace.middleCellX, y));
                }
                foreach (int x in MyMathHelper.interpolatePoints(trace.middleCellX, trace.endCellX, true))
                {
                    cellsToVisitAlongPatrolRoute.Add(new Point(x, trace.endCellY));
                }
            }
            return cellsToVisitAlongPatrolRoute;
        }

        public static void placeGameObjectOnFirstCellOfRoute(GameObject gameObject)
        {
            Point firstPointOfRoute = new Point(gameObject.PatrolRoute.startCellX, gameObject.PatrolRoute.startCellY);
            gameObject.ScreenCoord.x = UserInterface.convertCellCoordsToVirtualScreenCoords(firstPointOfRoute).X + UserInterface.cellWidth / 2;
            gameObject.ScreenCoord.y = UserInterface.convertCellCoordsToVirtualScreenCoords(firstPointOfRoute).Y + UserInterface.cellWidth / 2;
        }
    }
}
