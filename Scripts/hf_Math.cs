using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public static class hf_Math
    {
        public static Vector2 getRotationPosition(float angleInDegrees, float radius, Vector2 center)
        {
            Vector2 newPosition = center;

            angleInDegrees %= 360;//if the angle goes over 360 loop back around to 0
            double rotationRadians = (angleInDegrees) * (Math.PI / 180);//converts degrees to radians

            newPosition += new Vector2((float)(radius * Math.Cos(rotationRadians)), (float)(radius * Math.Sin(rotationRadians)));

            return newPosition;
        }

        public static float getAngle(Vector2 p1, Vector2 p2)
        {
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;

            return (float)Math.Atan2(dy, dx);
        }

        public static Vector2 RadiansToUnitVector(float r)
        {
            return new Vector2((float)Math.Cos(r), (float)Math.Sin(r));
        }

        public static float DegreesToRadians(float d)
        {
            return d * (float)Math.PI / 180;
        }

        public static float RadiansToDegres(float r)
        {
            return r / (float)Math.PI * 180;
        }
    }
}
