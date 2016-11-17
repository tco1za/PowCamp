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
        private enum TracingState { beforePointsSelected, firstCellSelected, middleCellSelected };
        private static TracingState currentTraceState = TracingState.beforePointsSelected;
        private static Point firstPointOfTrace;
        private static Point middlePointOfTrace;
        private static Point endPointOfTrace;
        private static Point mousePosition;
        private static GameObject builderGlyphObject;
        private static GameObject redWall;
        private static GameObject greenWall;

        private static State currentState = State.neutral;

        public static void initialize()
        {
            redWall = DataAccess.instantiateEntity(GameObjectTypeEnum.redWall);
            greenWall = DataAccess.instantiateEntity(GameObjectTypeEnum.greenWall);
        }

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

            if (currentState == State.neutral)
            {
                checkButtonsForMouseClicks();
            }
            if (currentState == State.placingWall)
            {
                updatePlacingWall();
            }
            if (currentState == State.tracingPatrolRoute)
            {
                updateTracingPatrolRoute();
            }
        }

        private static void checkButtonsForMouseClicks()
        {
            List<GameObject> tools = Game.gameObjects.Where(a => a.Tool != null).ToList();
            int index = 0;
            foreach (GameObject tool in tools)
            {
                GameObject button = Game.gameObjectTypes.Where(a => a.GameObjectType.enumValue == tool.Tool.buttonEnum).FirstOrDefault();
                setButtonPosition(index, button);
                if (isMouseOver(button))
                {
                    if (Game.isLeftMouseClicked)
                    {
                        Game.isLeftMouseClicked = false;
                        if (button.GameObjectType.enumValue == GameObjectTypeEnum.buildFenceButton)
                        {
                            currentState = State.placingWall;
                            currentTraceState = TracingState.beforePointsSelected;
                        }
                        if (button.GameObjectType.enumValue == GameObjectTypeEnum.hireGuardButton)
                        {
                            currentState = State.tracingPatrolRoute;
                            currentTraceState = TracingState.beforePointsSelected;
                        }
                    }
                }
                index++;
            }
        }

        private static bool isMouseOver(GameObject button)
        {
            Rectangle buttonRectangle = new Rectangle((int)button.ScreenCoord.x - button.CurrentAnimation.Animation.frameWidth/2, (int)button.ScreenCoord.y - button.CurrentAnimation.Animation.frameHeight/2, button.CurrentAnimation.Animation.frameWidth, button.CurrentAnimation.Animation.frameHeight);
            return buttonRectangle.Contains(mousePosition.X, mousePosition.Y);
        }

        private static void setButtonPosition(int index, GameObject button)
        {
            button.ScreenCoord.x = UserInterface.sidePanelWidth / 2;
            button.ScreenCoord.y = 100 + index * button.CurrentAnimation.Animation.frameHeight;
        }

        private static void updateTracingPatrolRoute()
        {
            mousePosition = constrainMousePositionToPatrolableArea(mousePosition);
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
                        GameObject newGuard = DataAccess.instantiateEntity(GameObjectTypeEnum.guard);
                        Game.gameObjects.Add(newGuard);
                        assignTracePatrolRouteToGuard(newGuard);
                        currentState = State.neutral;
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
                        List<Point> cellsToVisitAlongTrace = getCellsVisitedAlongTrace();
                        List<Point> distinctPartitions = getDistinctPartitions(cellsToVisitAlongTrace);
                        foreach (Point partitionMidPoint in distinctPartitions) 
                        {
                            GameObject newWall = DataAccess.instantiateEntity(GameObjectTypeEnum.concreteWall);
                            newWall.CellPartition.partitionMidPointX = partitionMidPoint.X;
                            newWall.CellPartition.partitionMidPointY = partitionMidPoint.Y;
                            newWall.ScreenCoord.x = partitionMidPoint.X;
                            newWall.ScreenCoord.y = partitionMidPoint.Y;
                            if (!Prisoners.isAnyLivePrisonersInContactWithWall(newWall))
                            {
                                removeExistingWallInNewWallPosition(newWall);
                                Game.gameObjects.Add(newWall);
                                PathFindingGraph.addWall(newWall);
                            }
                        }
                        currentState = State.neutral;
                    }
                    break;
            }
        }

        private static void removeExistingWallInNewWallPosition(GameObject newWall)
        {
            Game.gameObjects.RemoveAll(a => a.Wall != null && a.CellPartition != null && a.CellPartition.partitionMidPointX == newWall.CellPartition.partitionMidPointX
               && a.CellPartition.partitionMidPointY == newWall.CellPartition.partitionMidPointY);
        }

        private static void assignTracePatrolRouteToGuard(GameObject guard)
        {
            guard.PatrolRoute.startCellX = firstPointOfTrace.X;
            guard.PatrolRoute.startCellY = firstPointOfTrace.Y;
            guard.PatrolRoute.middleCellX = middlePointOfTrace.X;
            guard.PatrolRoute.middleCellY = middlePointOfTrace.Y;
            guard.PatrolRoute.endCellX = endPointOfTrace.X;
            guard.PatrolRoute.endCellY = endPointOfTrace.Y;
            guard.PatrolRoute.targetCellIndex = 0;
            guard.Guard.state = GuardState.patrolling;
            guard.Guard.patrolState = GuardPatrollingState.walking;
            guard.Orientation.x = 1;
            guard.Orientation.y = 0;
            PatrolRoutes.placeGameObjectOnFirstCellOfRoute(guard);
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

        private static Point constrainMousePositionToPatrolableArea(Point selectedCellCorner)
        {
            return new Point(constrainValueToMinMax(selectedCellCorner.X, sidePanelWidth+5, virtualScreenWidth - sidePanelWidth-5),
                constrainValueToMinMax(selectedCellCorner.Y, 5, virtualScreenHeight - 5));
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

            drawSidePanels(spriteBatch);
            drawButtons(spriteBatch);
            drawMouseCursor(spriteBatch);
        }

        private static void drawSidePanels(SpriteBatch spriteBatch)
        {
            GameObject sidePanel = Game.gameObjectTypes.Where(a => a.GameObjectType.enumValue == GameObjectTypeEnum.leftSidePanel).FirstOrDefault();
            sidePanel.ScreenCoord.x = UserInterface.sidePanelWidth / 2;
            sidePanel.ScreenCoord.y = UserInterface.virtualScreenHeight / 2;  
            Game.drawGameObject(spriteBatch, sidePanel);

            GameObject rightSidePanel = Game.gameObjectTypes.Where(a => a.GameObjectType.enumValue == GameObjectTypeEnum.rightSidePanel).FirstOrDefault();
            rightSidePanel.ScreenCoord.x = UserInterface.virtualScreenWidth - (UserInterface.sidePanelWidth / 2);
            rightSidePanel.ScreenCoord.y = UserInterface.virtualScreenHeight / 2;
            Game.drawGameObject(spriteBatch, rightSidePanel);
        }

        private static void drawButtons(SpriteBatch spriteBatch)
        {
            List<GameObject> tools = Game.gameObjects.Where(a => a.Tool != null).ToList();
            int index = 0;
            foreach ( GameObject tool in tools )
            {
                GameObject button = Game.gameObjectTypes.Where(a => a.GameObjectType.enumValue == tool.Tool.buttonEnum).FirstOrDefault();
                setButtonPosition(index, button);
                if (currentState == State.placingWall && button.GameObjectType.enumValue == GameObjectTypeEnum.buildFenceButton)
                {
                    button.CurrentAnimation.index = 1;
                }
                if (currentState == State.tracingPatrolRoute && button.GameObjectType.enumValue == GameObjectTypeEnum.hireGuardButton)
                {
                    button.CurrentAnimation.index = 3;
                }
                if ( currentState == State.neutral && button.GameObjectType.enumValue == GameObjectTypeEnum.buildFenceButton)
                {
                    button.CurrentAnimation.index = 0;
                }
                if (currentState == State.neutral && button.GameObjectType.enumValue == GameObjectTypeEnum.hireGuardButton)
                {
                    button.CurrentAnimation.index = 2;
                }
                Game.drawGameObject(spriteBatch, button);
                index++;
            }
        }

        private static void drawMouseCursor(SpriteBatch spriteBatch)
        {
            GameObject mouseCursorObject = Game.gameObjectTypes.Where(a => a.GameObjectType.enumValue == GameObjectTypeEnum.mouseCursor).FirstOrDefault(); 
            mouseCursorObject.ScreenCoord.x = mousePosition.X;
            mouseCursorObject.ScreenCoord.y = mousePosition.Y;
            Game.drawGameObject(spriteBatch, mouseCursorObject);
        }

        private static List<Point> getCellsVisitedAlongTrace()
        {
            List<Point> cellsToVisitAlongTrace = PatrolRoutes.buildListOfCellsVisitedAlongTrace(createPatrolRouteObjectFromCurrentTrace());
            return cellsToVisitAlongTrace;
        }

        private static List<Point> getDistinctPartitions(List<Point> cellsVisitedAlongTrace)
        {
            List<Point> distinctPartitions = removeDuplicatesCellPartitionList(extractPartitionsFromTracedCellCenters((cellsVisitedAlongTrace)));
            return distinctPartitions;
        }

        private static void drawWallTrace(SpriteBatch spriteBatch)
        {
            Point cellThatMouseIsIn = UserInterface.convertVirtualScreenCoordsToCellCoords(mousePosition);
            List<Point> cellsToVisitAlongTrace = getCellsVisitedAlongTrace();
            drawBuilderGlyph(spriteBatch, cellsToVisitAlongTrace);
            List<Point> distinctPartitions = getDistinctPartitions(cellsToVisitAlongTrace);
            foreach (Point partitionMidPoint in distinctPartitions)
            {
                redWall.CellPartition.partitionMidPointX = partitionMidPoint.X;
                redWall.CellPartition.partitionMidPointY = partitionMidPoint.Y;
                redWall.ScreenCoord.x = partitionMidPoint.X;
                redWall.ScreenCoord.y = partitionMidPoint.Y;
                greenWall.CellPartition.partitionMidPointX = partitionMidPoint.X;
                greenWall.CellPartition.partitionMidPointY = partitionMidPoint.Y;
                greenWall.ScreenCoord.x = partitionMidPoint.X;
                greenWall.ScreenCoord.y = partitionMidPoint.Y;
                if (Prisoners.isAnyLivePrisonersInContactWithWall(redWall))
                {
                    Walls.draw(redWall, spriteBatch);
                }
                else
                {
                    Walls.draw(greenWall, spriteBatch);
                }
            }
        }

        private static void drawBuilderGlyph(SpriteBatch spriteBatch, List<Point> cellsVisitedAlongTrace)
        {
            builderGlyphObject = Game.gameObjectTypes.Where(a => a.GameObjectType.enumValue == GameObjectTypeEnum.mouseCellCornerGlyph).FirstOrDefault();
            Point builderGlyphPosition = UserInterface.convertCellCoordsToVirtualScreenCoords(cellsVisitedAlongTrace[0]);
            builderGlyphObject.ScreenCoord.x = builderGlyphPosition.X;
            builderGlyphObject.ScreenCoord.y = builderGlyphPosition.Y;
            Game.drawGameObject(spriteBatch, builderGlyphObject);
        }

        private static void drawPatrolRoute(SpriteBatch spriteBatch)
        {
            List<Point> cellsToVisitAlongPatrolRoute = PatrolRoutes.buildListOfCellsVisitedAlongTrace(createPatrolRouteObjectFromCurrentTrace());
            GameObject patrolRouteGlyphObject;
            patrolRouteGlyphObject = Game.gameObjectTypes.Where(a => a.GameObjectType.enumValue == GameObjectTypeEnum.patrolRouteGreenGlyph).FirstOrDefault();
            foreach (Point cell in cellsToVisitAlongPatrolRoute)
            {
                Point patrolRouteGlyphPosition = UserInterface.convertCellCoordsToVirtualScreenCoords(cell);
                patrolRouteGlyphObject.ScreenCoord.x = patrolRouteGlyphPosition.X + cellWidth / 2;
                patrolRouteGlyphObject.ScreenCoord.y = patrolRouteGlyphPosition.Y + cellWidth / 2;
                Game.drawGameObject(spriteBatch, patrolRouteGlyphObject);
            }
        }

        private static List<Point> extractPartitionsFromTracedCellCenters( List<Point> tracedCellCorners )
        {
            List<Point> cellPartitions = new List<Point>();
            for ( int i = 0; i < tracedCellCorners.Count(); i++)
            {
                if (i < tracedCellCorners.Count() - 1)
                {
                    cellPartitions.Add(MyMathHelper.middleOfTwoPoints(convertCellCoordsToVirtualScreenCoords(tracedCellCorners[i]), convertCellCoordsToVirtualScreenCoords(tracedCellCorners[i + 1])));
                }
            }
            return cellPartitions;
        }

        private static List<Point> removeDuplicatesCellPartitionList(List<Point> cellPartitions)
        {
            HashSet<Point> distinctCellPartitions = new HashSet<Point>(cellPartitions);
            return distinctCellPartitions.ToList();
        }


    }
}
