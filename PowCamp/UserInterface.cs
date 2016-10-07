using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowCamp
{
    class UserInterface
    {
        private static int virtualScreenWidth = 1920;
        private static int virtualScreenHeight = 1080;
        private static int cellWidth = 54;
        private static int sidePanelWidth = 204;


        private static Point convertScreenPointToVirtualScreenPoint(Point point)
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

        private static Point convertVirtualScreenCoordsToCellCoords(Point virtualScreenCoords)
        {
            return new Point((virtualScreenCoords.X - sidePanelWidth) / cellWidth, virtualScreenCoords.Y / cellWidth);
        }

        public static Point convertCellCoordsToVirtualScreenCoords(Point cellCoords)
        {
            const int cellWidth = 54;
            const int sidePanelWidth = 204;

            return new Point(cellCoords.X * cellWidth + sidePanelWidth, cellCoords.Y * cellWidth);
        }

        public static void update()
        {
            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);
            mousePosition = convertScreenPointToVirtualScreenPoint(mousePosition);
            var cellThatMouseIsCurrentlyIn = convertVirtualScreenCoordsToCellCoords(mousePosition);
           
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (!Game.gameObjects.Where(item => item.CellCoord.x == cellThatMouseIsCurrentlyIn.X && item.CellCoord.y == cellThatMouseIsCurrentlyIn.Y).Any())
                {
                    GameObject newFence = new GameObject();
                    newFence = DataAccess.instantiateEntity(GameObjectTypeEnum.fence);

                    newFence.CellCoord.x = cellThatMouseIsCurrentlyIn.X;
                    newFence.CellCoord.y = cellThatMouseIsCurrentlyIn.Y;

                    Game.gameObjects.Add(newFence);


                    //&& item.CellCoord.y == cellThatMouseIsCurrentlyIn.Y
                }


            }

        }

    }
}
