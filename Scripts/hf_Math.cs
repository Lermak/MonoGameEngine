using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public static class hf_Math
    {
        /// <summary>
        /// Get the World Position from a point. This considers the Game Scale and flips the Y so that +Y is upwards.
        /// </summary>
        /// <param name="v">Point</param>
        /// <returns></returns>
        public static Vector2 WorldPosition(Vector2 v)
        {
            return (v * RenderingManager.GameScale * new Vector2(1, -1));
        }
        /// <summary>
        /// Get the position from a point given a radius and number of degrees, with 0 degrees being straight to the right
        /// </summary>
        /// <param name="angleInDegrees">Angle in Degrees</param>
        /// <param name="radius">Distance from Center</param>
        /// <param name="center">Center Point</param>
        /// <returns></returns>
        public static Vector2 GetPosFromPoint(float angleInDegrees, float radius, Vector2 center)
        {
            Vector2 newPosition = center;

            angleInDegrees %= 360;//if the angle goes over 360 loop back around to 0
            double rotationRadians = (angleInDegrees) * (Math.PI / 180);//converts degrees to radians

            newPosition += new Vector2((float)(radius * Math.Cos(rotationRadians)), (float)(radius * Math.Sin(rotationRadians)));

            return newPosition;
        }
        /// <summary>
        /// Get the Radians from an angle with p1 as the origin
        /// </summary>
        /// <param name="p1">Origin point of the angle</param>
        /// <param name="p2">Point on a circle around p1</param>
        /// <returns></returns>
        public static float GetAngleRad(Vector2 p1, Vector2 p2)
        {
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;

            float rad = (float)Math.Atan2(dy, dx);

            return rad;
        }
        /// <summary>
        /// Get the Unit Vector for a given number of radians
        /// </summary>
        /// <param name="r">Number of radians</param>
        /// <returns></returns>
        public static Vector2 RadToUnit(float r)
        {
            return new Vector2((float)Math.Cos(r), (float)Math.Sin(r));
        }
        /// <summary>
        /// Convert Degrees to Radians
        /// </summary>
        /// <param name="d">Degrees</param>
        /// <returns></returns>
        public static float DegToRad(float d)
        {
            return d * (float)Math.PI / 180;
        }
        /// <summary>
        /// Convert Radians to Degrees
        /// </summary>
        /// <param name="r">Radians</param>
        /// <returns></returns>
        public static float RadToDeg(float r)
        {
            return r / (float)Math.PI * 180;
        }
        /// <summary>
        /// Get the Hypotnuse of a triangle
        /// </summary>
        /// <param name="x">Triangle Length</param>
        /// <param name="y">Triangle Height</param>
        /// <returns></returns>
        public static float Hypot(float x, float y)
        {
            return (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }
        /// <summary>
        /// Get the position of a of a point rotated around another point by a number of degrees
        /// </summary>
        /// <param name="degrees">Amount to rotate</param>
        /// <param name="startPos">Starting Point</param>
        /// <param name="rotateAround">Circle Center</param>
        /// <returns></returns>
        public static Vector2 GetRotatedPosition(float degrees, Vector2 startPos, Vector2 rotateAround)
        {
            return new Vector2((float)(Math.Cos(DegToRad(degrees)) * (rotateAround.X - startPos.X) - Math.Sin(DegToRad(degrees)) * (rotateAround.Y - startPos.Y) + startPos.X),
            (float)(Math.Sin(DegToRad(degrees)) * (rotateAround.X - startPos.X) + Math.Cos(DegToRad(degrees)) * (rotateAround.Y - startPos.Y) + startPos.Y));
        }
    }
}
