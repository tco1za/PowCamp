using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowCamp
{
    class UserInterface
    {
        public static int virtualScreenWidth = 1920;
        public static int virtualScreenHeight = 1080;
        public static int cellWidth = 54;
        public static int sidePanelWidth = 204;
        private enum State { neutral , placingWall, tracingPatrolRoute, deletingWall };
        private enum TracingState { beforePointsSelected, firstCellSelected, middleCellSelected, endCellSelected };
        private static State currentState = State.placingWall;
        private static TracingState currentTraceState = TracingState.beforePointsSelected;
        private static Point firstPointOfTrace;
        private static Point middlePointOfTrace;
        private static Point endPointOfTrace;
        private static Point mousePosition;
        private static GameObject builderGlyphObject = DataAccess.db.GameObjects.Where(a => a.GameObjectType.enumValue == GameObjectTypeEnum.mouseCellCornerGlyph).FirstOrDefault();
        public static GameObject guardToAssignPatrolRouteTo;

        public static Point convertScreenPointToVirtualScreenPoint(Point point)
        {
            var matrix = Matrix.Invert(GetScaleMatrix());
            return Vector2.Transform(point.ToVector2(), matrix).ToPoint();
        }

        public static Matrix GetScaleMatrix()
        {
            var scaleX = (float)Game.graphics.GraphicsDevice.PresentationParameters.BackBufferWidth / (float)virtualScreenWidth;
            var scaleY = (float)Game.graphics.GraphicsDevice.PresentationParameters.BackBufferHeight / (float)virtualScreenHeight;
            return Matrix.CreateScale(scaleX, scaleY, 1.0f);
        }

        public static Point convertVirtualScreenCoordsToCellCoords(Point virtualScreenCoords)
        {
            return new Point((int)Math.Floor(((float)virtualScreenCoords.X - (float)sidePanelWidth) / (float)cellWidth), (int)Math.Floor((float)virtualScreenCoords.Y / (float)cellWidth));
        }

        public static Point convertCellCoordsToVirtualScreenCoords(Point cellCoords)
        {
            const int cellWidth = 54;
            const int sidePanelWidth = 204;

            return new Point(cellCoords.X * cellWidth + sidePanelWidth, cellCoords.Y * cellWidth);
        }

        public static void update()
        {
            MouseState mouseState = Game.currentMouseState;
            mousePosition = new Point(mouseState.X, mouseState.Y);
            mousePosition = convertScreenPointToVirtualScreenPoint(mousePosition);

            if (currentState == State.placingWall)
            {
                updatePlacingWall();
            }
            if (currentState == State.tracingPatrolRoute)
            {
                updateTracingPatrolRoute();
            }
        }

        private static void updateTracingPatrolRoute()
        {
            mousePosition = constrainMousePositionToWallBuildableArea(mousePosition);
            Point cellThatMouseIsCurrentlyIn = convertVirtualScreenCoordsToCellCoords(mousePosition);
            switch (currentTraceState)
            {
                case (TracingState.beforePointsSelected):
                    firstPointOfTrace = cellThatMouseIsCurrentlyIn;
                    middlePointOfTrace = cellThatMouseIsCurrentlyIn;
                    endPointOfTrace = cellThatMouseIsCurrentlyIn;
                    if (Game.isLeftMouseClicked)
                    {
                        currentTraceState = TracingState.firstCellSelected;
                    }
                    break;
                case (TracingState.firstCellSelected):
                    middlePointOfTrace = cellThatMouseIsCurrentlyIn;
                    endPointOfTrace = cellThatMouseIsCurrentlyIn;
                    if (Game.isLeftMouseClicked)
                    {
                        currentTraceState = TracingState.middleCellSelected;
                    }
                    break;
                case (TracingState.middleCellSelected):
                    endPointOfTrace = cellThatMouseIsCurrentlyIn;
                    if (Game.isLeftMouseClicked)
                    {
                        currentTraceState = TracingState.endCellSelected;
                        assignTracePatrolRouteToGuard();
                    }
                    break;
            }
        }

        private static void updatePlacingWall()
        {
            mousePosition = constrainMousePositionToWallBuildableArea(mousePosition);
            Point cellThatMouseIsCurrentlyIn = convertVirtualScreenCoordsToCellCoords(mousePosition);
            Point currentCellCornerMouseIsClosestTo = convertVirtualScreenCoordsToCellCoords(cellCornerThatMouseCursorIsClosestTo(cellThatMouseIsCurrentlyIn, mousePosition));
            switch (currentTraceState)
            {
                case (TracingState.beforePointsSelected):
                    firstPointOfTrace = currentCellCornerMouseIsClosestTo;
                    middlePointOfTrace = currentCellCornerMouseIsClosestTo;
                    endPointOfTrace = currentCellCornerMouseIsClosestTo;
                    if (Game.isLeftMouseClicked)
                    {
                        currentTraceState = TracingState.firstCellSelected;
                    }
                    break;
                case (TracingState.firstCellSelected):
                    middlePointOfTrace = currentCellCornerMouseIsClosestTo;
                    endPointOfTrace = currentCellCornerMouseIsClosestTo;
                    if (Game.isLeftMouseClicked)
                    {
                        currentTraceState = TracingState.middleCellSelected;
                    }
                    break;
                case (TracingState.middleCellSelected):
                    endPointOfTrace = currentCellCornerMouseIsClosestTo;
                    if (Game.isLeftMouseClicked)
                    {
                        currentTraceState = TracingState.endCellSelected;
                    }
                    break;
            }
        }

        private static void assignTracePatrolRouteToGuard()
        {
            guardToAssignPatrolRouteTo.PatrolRoute.startCellX = firstPointOfTrace.X;
            guardToAssignPatrolRouteTo.PatrolRoute.startCellY = firstPointOfTrace.Y;
            guardToAssignPatrolRouteTo.PatrolRoute.middleCellX = middlePointOfTrace.X;
            guardToAssignPatrolRouteTo.PatrolRoute.middleCellY = middlePointOfTrace.Y;
            guardToAssignPatrolRouteTo.PatrolRoute.endCellX = endPointOfTrace.X;
            guardToAssignPatrolRouteTo.PatrolRoute.endCellY = endPointOfTrace.Y;
            guardToAssignPatrolRouteTo.PatrolRoute.targetCellIndex = 0;
            PatrolRoutes.placeGameObjectOnFirstCellOfRoute(guardToAssignPatrolRouteTo);
        }

        private static PatrolRoute createPatrolRouteObjectFromCurrentTrace()
        {
            PatrolRoute patrolRoute = new PatrolRoute();
            patrolRoute.startCellX = firstPointOfTrace.X;
            patrolRoute.startCellY = firstPointOfTrace.Y;
            patrolRoute.middleCellX = middlePointOfTrace.X;
            patrolRoute.middleCellY = middlePointOfTrace.Y;
            patrolRoute.endCellX = endPointOfTrace.X;
            patrolRoute.endCellY = endPointOfTrace.Y;
            return patrolRoute;
        }

        public static int getNumHorizontalCells()
        {
            return (virtualScreenWidth - (sidePanelWidth * 2)) / cellWidth;
        }

        public static int getNumVerticalCells()
        {
            return virtualScreenHeight / cellWidth;
        }

        public static bool isSelectedCellWithinBounds(Point cellThatMouseIsCurrentlyIn)
        {
            return (cellThatMouseIsCurrentlyIn.X >= 0 && cellThatMouseIsCurrentlyIn.Y >= 0
                && cellThatMouseIsCurrentlyIn.X < getNumHorizontalCells() && cellThatMouseIsCurrentlyIn.Y < getNumVerticalCells());
        }

        private static List<int> interpolatePoints(int start, int end, bool includeEnd)
        {
            List<int> interpolatedPoints = new List<int>();

            if ( end > start )
            {
                if (includeEnd)
                {
                    end = end + 1;
                }
                for ( int i = start; i < end; i++ )
                {
                    interpolatedPoints.Add(i);
                }
            }
            else
            {
                if (includeEnd)
                {
                    end = end - 1;
                }
                for (int i = start; i > end; i--)
                {
                    interpolatedPoints.Add(i);
                }
            }
            return interpolatedPoints;
        }

        private static double distanceBetweenTwoPoints(Point firstPoint, Point secondPoint)
        {
            return Math.Sqrt((Math.Pow(firstPoint.X - secondPoint.X, 2) + Math.Pow(firstPoint.Y - secondPoint.Y, 2)));
        }

        private static Point cellCornerThatMouseCursorIsClosestTo( Point cellThatMouseIsIn, Point mousePosition )
        {
            List<Point> cornersOfCell = new List<Point>();
            Point topLeftCornerOfCellThatMouseIsIn = convertCellCoordsToVirtualScreenCoords(cellThatMouseIsIn);
            Point topRightCornerOfCellThatMouseIsIn = new Point(topLeftCornerOfCellThatMouseIsIn.X + cellWidth, topLeftCornerOfCellThatMouseIsIn.Y);
            Point bottomLeftCornerOfCellThatMouseIsIn = new Point(topLeftCornerOfCellThatMouseIsIn.X, topLeftCornerOfCellThatMouseIsIn.Y + cellWidth);
            Point bottomRightCornerOfCellThatMouseIsIn = new Point(bottomLeftCornerOfCellThatMouseIsIn.X + cellWidth, bottomLeftCornerOfCellThatMouseIsIn.Y);
            cornersOfCell.Add(topLeftCornerOfCellThatMouseIsIn);
            cornersOfCell.Add(topRightCornerOfCellThatMouseIsIn);
            cornersOfCell.Add(bottomLeftCornerOfCellThatMouseIsIn);
            cornersOfCell.Add(bottomRightCornerOfCellThatMouseIsIn);
            cornersOfCell.Sort((x, y) => distanceBetweenTwoPoints(mousePosition, x).CompareTo(distanceBetweenTwoPoints(mousePosition, y)));
            return cornersOfCell[0];
        }

        private static int constrainValueToMinMax( int value, int min, int max )
        {
            if (value > max) return max;
            if (value < min) return min;
            return value;
        }

        private static Point constrainMousePositionToWallBuildableArea( Point selectedCellCorner )
        {
            return new Point(constrainValueToMinMax(selectedCellCorner.X, sidePanelWidth+cellWidth, virtualScreenWidth - sidePanelWidth - cellWidth), 
                constrainValueToMinMax(selectedCellCorner.Y, cellWidth, virtualScreenHeight-cellWidth));
        }

        public static void draw(SpriteBatch spriteBatch)
        {
            if (currentState == State.placingWall)
            {
                drawWallTrace(spriteBatch);
            }

            if (currentState == State.tracingPatrolRoute)
            {
                drawPatrolRoute(spriteBatch);
            }
            GameObject mouseCursorObject = DataAccess.db.GameObjects.Where(a => a.GameObjectType.enumValue == GameObjectTypeEnum.mouseCursor).FirstOrDefault();
            mouseCursorObject.ScreenCoord.x = mousePosition.X;
            mouseCursorObject.ScreenCoord.y = mousePosition.Y;
            Game.drawGameObject(spriteBatch, mouseCursorObject);
        }

        private static void drawWallTrace(SpriteBatch spriteBatch)
        {
            Point cellThatMouseIsIn = UserInterface.convertVirtualScreenCoordsToCellCoords(mousePosition);
            List<Point> cellsToVisitAlongTrace = buildListOfCellsVisitedAlongTrace(createPatrolRouteObjectFromCurrentTrace());

            foreach (Point cell in cellsToVisitAlongTrace)
            {
                Point builderGlyphPosition = UserInterface.convertCellCoordsToVirtualScreenCoords(cell);
                builderGlyphObject.ScreenCoord.x = builderGlyphPosition.X;
                builderGlyphObject.ScreenCoord.y = builderGlyphPosition.Y;
                Game.drawGameObject(spriteBatch, builderGlyphObject);
            }

            List<GameObject> wallObjects = new List<GameObject>();
            List<Point> distinctPartitions = removeDuplicatesCellPartitionList(extractPartitionsFromTracedCellCenters(cellsToVisitAlongTrace));
            foreach (Point partitionMidPoint in distinctPartitions)
            {
                GameObject newWall = DataAccess.fetchGameObjectFromPool(GameObjectTypeEnum.concreteWall);
                newWall.CellPartition.partitionMidPointX = partitionMidPoint.X;
                newWall.CellPartition.partitionMidPointY = partitionMidPoint.Y;
                wallObjects.Add(newWall);
            }
            Walls.draw(wallObjects, spriteBatch);
            foreach (GameObject wall in wallObjects)
            {
                DataAccess.releaseGameObjectFromPool(GameObjectTypeEnum.concreteWall, wall);
            }

            if (currentTraceState == TracingState.endCellSelected)
            {
                Game.gameObjects.AddRange(wallObjects);  // TODO: move this to update method
                wallObjects.ForEach(a => PathFindingGraph.addWall(a));
                currentState = State.neutral;
            }
        }

        private static void drawPatrolRoute(SpriteBatch spriteBatch)
        {
            List<Point> cellsToVisitAlongPatrolRoute = buildListOfCellsVisitedAlongTrace(createPatrolRouteObjectFromCurrentTrace());

            foreach (Point cell in cellsToVisitAlongPatrolRoute)
            {
                Point builderGlyphPosition = UserInterface.convertCellCoordsToVirtualScreenCoords(cell);

                GameObject builderGlyphObject = DataAccess.db.GameObjects.Where(a => a.GameObjectType.enumValue == GameObjectTypeEnum.mouseBuildGlyph).FirstOrDefault();
                builderGlyphObject.ScreenCoord.x = builderGlyphPosition.X + cellWidth / 2;
                builderGlyphObject.ScreenCoord.y = builderGlyphPosition.Y + cellWidth / 2;
                Game.drawGameObject(spriteBatch, builderGlyphObject);
            }
        }

        private static Point middleOfTwoPoints( Point p1, Point p2)
        {
            Point vector = p2 - p1;
            Point middle = p1 + new Point(vector.X / 2, vector.Y/2);
            return middle;
        }

        private static List<Point> extractPartitionsFromTracedCellCenters( List<Point> tracedCellCorners )
        {
            List<Point> cellPartitions = new List<Point>();
            for ( int i = 0; i < tracedCellCorners.Count(); i++)
            {
                if (i < tracedCellCorners.Count() - 1)
                {
                    cellPartitions.Add(middleOfTwoPoints(convertCellCoordsToVirtualScreenCoords(tracedCellCorners[i]), convertCellCoordsToVirtualScreenCoords(tracedCellCorners[i + 1])));
                }
            }
            return cellPartitions;
        }

        private static List<Point> removeDuplicatesCellPartitionList(List<Point> cellPartitions)
        {
            HashSet<Point> distinctCellPartitions = new HashSet<Point>(cellPartitions);
            return distinctCellPartitions.ToList();
        }

        public static List<Point> buildListOfCellsVisitedAlongTrace(PatrolRoute trace)  // TODO: break this method up in two
        {
            List<Point> cellsToVisitAlongPatrolRoute = new List<Point>();

            int horizontalDistanceBetweenStartAndMiddlePatrolRouteCells = Math.Abs(trace.middleCellX - trace.startCellX);
            int verticalDistanceBetweenStartAndMiddlePatrolRouteCells = Math.Abs(trace.middleCellY - trace.startCellY);
            if (horizontalDistanceBetweenStartAndMiddlePatrolRouteCells > verticalDistanceBetweenStartAndMiddlePatrolRouteCells)
            {
                foreach (int x in interpolatePoints(trace.startCellX, trace.middleCellX, false))
                {
                    cellsToVisitAlongPatrolRoute.Add(new Point(x, trace.startCellY));
                }
                foreach (int y in interpolatePoints(trace.startCellY, trace.middleCellY, false))
                {
                    cellsToVisitAlongPatrolRoute.Add(new Point(trace.middleCellX, y));
                }
            }
            else
            {
                foreach (int y in interpolatePoints(trace.startCellY, trace.middleCellY, false))
                {
                    cellsToVisitAlongPatrolRoute.Add(new Point(trace.startCellX, y));
                }
                foreach (int x in interpolatePoints(trace.startCellX, trace.middleCellX, false))
                {
                    cellsToVisitAlongPatrolRoute.Add(new Point(x, trace.middleCellY));
                }
            }

            int horizontalDistanceBetweenMiddleAndEndPatrolRouteCells = Math.Abs(trace.middleCellX - trace.endCellX);
            int verticalDistanceBetweenMiddleAndEndPatrolRouteCells = Math.Abs(trace.middleCellY - trace.endCellY);
            if (horizontalDistanceBetweenMiddleAndEndPatrolRouteCells > verticalDistanceBetweenMiddleAndEndPatrolRouteCells)
            {
                foreach (int x in interpolatePoints(trace.middleCellX, trace.endCellX, false))
                {
                    cellsToVisitAlongPatrolRoute.Add(new Point(x, trace.middleCellY));
                }
                foreach (int y in interpolatePoints(trace.middleCellY, trace.endCellY, true))
                {
                    cellsToVisitAlongPatrolRoute.Add(new Point(trace.endCellX, y));
                }
            }
            else
            {
                foreach (int y in interpolatePoints(trace.middleCellY, trace.endCellY, false))
                {
                    cellsToVisitAlongPatrolRoute.Add(new Point(trace.middleCellX, y));
                }
                foreach (int x in interpolatePoints(trace.middleCellX, trace.endCellX, true))
                {
                    cellsToVisitAlongPatrolRoute.Add(new Point(x, trace.endCellY));
                }
            }
            return cellsToVisitAlongPatrolRoute;
        }
    }
}
