using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public static class hf_Math
    {
        public static Vector2 getRotationPosition(float angleInDegrees, float radius, float rotation, Vector2 center)
        {
            Vector2 newPosition = center;

            angleInDegrees %= 360;//if the angle goes over 360 loop back around to 0
            double rotationRadians = (angleInDegrees ) * (Math.PI / 180) - rotation;//converts degrees to radians

            newPosition += new Vector2((float)(radius * Math.Cos(rotationRadians)), (float)(radius * Math.Sin(rotationRadians)));

            return newPosition;
        }

        public static float getAngle(Vector2 p1, Vector2 p2)
        {
            float dx = -(p1.X - p2.X);
            // Minus to correct for coord re-mapping
            float dy = -(p1.Y - p2.Y);

            float inRads = (float)Math.Atan2(dy, dx);

            return (float)(inRads * 180/Math.PI);
        }
    }
}
