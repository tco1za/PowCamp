using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowCamp
{
    class MyMathHelper
    {
        public static bool isValuesClose(double val1, double val2) 
        {
            double threshold = 0.1f;
            return (Math.Abs(val1 - val2) < threshold);
        }

        public static Vector2 convertPolarCoordsToCartesian(float angleInRadians, float distance)  
        {
            return new Vector2((float)Math.Cos(angleInRadians), -(float)Math.Sin(angleInRadians)) * distance;
        }

        public static float convertVectorToAngleOfRotation(float x, float y) 
        {
            return (float)Math.Atan2(y, x);
        }
    }
}
