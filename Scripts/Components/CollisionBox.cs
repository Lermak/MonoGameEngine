using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace MonoGame_Core.Scripts
{
    public class CollisionBox : Component
    {
        List<string> tags;
        bool checkCollision;
        Vector2 offset;
        Transform myTransform;
        GameObject myObject;
        float width;
        float height;

        float angle;
        float radius;

        public List<string> Tags { get { return tags; } }
        public bool CheckCollision { get{ return checkCollision; } }
        public Vector2 Offset { get { return offset; } }
        public Transform MyTransform { get { return myTransform; } }
        public GameObject MyObject { get { return myObject; } }
        public float Width { get { return width; } }
        public float Height { get { return height; } }
        public float Angle { get { return angle; } }
        public float Radius { get { return radius; } }

        public List<Vector2> Verticies { get { return new List<Vector2>() { TopRight(), TopLeft(), BottomLeft(), BottomRight() }; } } 
        public List<Vector2> Axies { get { return new List<Vector2>() { getRotationPosition(0, 1, new Vector2()), getRotationPosition(90, 1, new Vector2()) }; } }

        public CollisionBox(List<string> t, bool check, Vector2 off, Transform transform, float width, float height, int uo) : base(uo)
        {
            name = "collisionBox";
            tags = t;
            checkCollision = check;
            offset = off;
            this.myTransform = transform;
            this.width = width;
            this.height = height;

            this.radius = (float)Math.Sqrt(Math.Pow(this.height / 2, 2) + Math.Pow(this.width / 2, 2));
            this.angle = (float)(Math.Acos((this.width / 2) / radius)) * (180 / (float)Math.PI);//Asin finds the angle of a right triangle, multiply by 2 to find the angle of the center to two corners
        }

        public CollisionBox(int uo, List<string> t, GameObject myObj, Transform myTrans) : base(uo)
        {
            tags = t;
            myTransform = myTrans;
            myObject = myObj;
            checkCollision = true;
            offset = new Vector2();
            width = myTrans.Width;
            height = myTrans.Height;

            this.radius = (float)Math.Sqrt(Math.Pow(this.height / 2, 2) + Math.Pow(this.width / 2, 2));
            this.angle = (float)(Math.Acos((this.width / 2) / radius)) * (180 / (float)Math.PI);//Asin finds the angle of a right triangle, multiply by 2 to find the angle of the center to two corners
        }

        public override void Update(float gt)
        {
            base.Update(gt);
        }

        public Vector2 TopRight()
        {
            return getRotationPosition(angle, radius, myTransform.Position + offset);
        }

        public Vector2 TopLeft()
        {
            return getRotationPosition(90 + angle, radius, myTransform.Position + offset);//use half side angle because at 0 rotation the box should be cut through the middle, so only half the side angle is needed
        }

        public Vector2 BottomLeft()
        {
            return getRotationPosition(180 + angle, radius, myTransform.Position + offset);
        }

        public Vector2 BottomRight()
        {
            return getRotationPosition(270 + angle, radius, myTransform.Position + offset);
        }

        public void ReplaceOffset(Vector2 newOff)
        {
            offset = newOff;
        }

        public void UpdateOffset(Vector2 delta)
        {
            offset += delta;
        }

        public void Resize(float width, float height)
        {
            this.width = width;
            this.height = height;

            this.radius = (float)Math.Sqrt(Math.Pow(this.height / 2, 2) + Math.Pow(this.width / 2, 2));
            this.angle = (float)(Math.Acos((this.width / 2) / radius)) * (180 / (float)Math.PI);
        }

        public Vector2 getRotationPosition(float angleInDegrees, float radius, Vector2 center)
        {
            Vector2 newPosition = center;

            angleInDegrees %= 360;//if the angle goes over 360 loop back around to 0
            double rotationRadians = angleInDegrees * (Math.PI / 180) - myTransform.Rotation;//converts degrees to radians

            newPosition += new Vector2((float)(radius * Math.Cos(rotationRadians)), (float)(radius * Math.Sin(rotationRadians)));

            return newPosition;
        }
    }
}
