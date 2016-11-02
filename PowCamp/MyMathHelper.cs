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
        public static bool isValuesClose(double val1, double val2, float threshold) 
        {
            return (Math.Abs(val1 - val2) < threshold);
        }

        public static float angleBetweenTwoVectorsInDegrees( Vector2 vec1, Vector2 vec2 )
        {
            vec1.Normalize();
            if (float.IsNaN(vec1.X)) vec1.X = 0;
            if (float.IsNaN(vec1.Y)) vec1.Y = 0;
            vec2.Normalize();
            if (float.IsNaN(vec2.X)) vec2.X = 0;
            if (float.IsNaN(vec2.Y)) vec2.Y = 0;
            return MathHelper.ToDegrees((float)Math.Acos(Vector2.Dot(vec1, vec2)));
        }

        public static Vector2 convertPolarCoordsToCartesian(float angleInRadians, float distance)  
        {
            return new Vector2((float)Math.Cos(angleInRadians), (float)Math.Sin(angleInRadians)) * distance;
        }

        public static float distanceAlongPositiveDirectionBetweenTwoAngles( float sourceAngle, float destinationAngle )
        {
            sourceAngle = constrainAngleInDegreesToPositive360(sourceAngle);
            destinationAngle = constrainAngleInDegreesToPositive360(destinationAngle);

            if ( destinationAngle > sourceAngle )
            {
                return destinationAngle - sourceAngle;
            }
            else
            {
                return (360 - sourceAngle) + destinationAngle;
            }
        }

        public static float distanceAlongNegativeDirectionBetweenTwoAngles(float sourceAngle, float destinationAngle)
        {
            sourceAngle = constrainAngleInDegreesToPositive360(sourceAngle);
            destinationAngle = constrainAngleInDegreesToPositive360(destinationAngle);

            if (destinationAngle < sourceAngle)
            {
                return sourceAngle - destinationAngle;
            }
            else
            {
                return (360 - destinationAngle) + sourceAngle;
            }
        }

        public static float constrainAngleInDegreesToPositive360( float angleInDegrees )
        {
            if ( angleInDegrees < 0 )
            {
                return 360 + angleInDegrees;
            }
            else
            {
                return angleInDegrees;
            }
        }

        public static float convertVectorToAngleOfRotationInRadians(float x, float y) 
        {
            return (float)Math.Atan2(y, x);
        }
    }
}
