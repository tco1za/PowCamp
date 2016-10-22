using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowCamp
{
    class PatrolRoutes
    {
        public static void placeGameObjectOnFirstCellOfRoute(GameObject gameObject)
        {
            Point firstPointOfRoute = new Point(gameObject.PatrolRoute.startCellX, gameObject.PatrolRoute.startCellY);
            gameObject.ScreenCoord.x = UserInterface.convertCellCoordsToVirtualScreenCoords(firstPointOfRoute).X + UserInterface.cellWidth / 2;
            gameObject.ScreenCoord.y = UserInterface.convertCellCoordsToVirtualScreenCoords(firstPointOfRoute).Y + UserInterface.cellWidth / 2;
        }
    }
}
