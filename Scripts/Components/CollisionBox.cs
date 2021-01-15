using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

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

        public List<string> Tags { get { return tags; } }
        public bool CheckCollision { get{ return checkCollision; } }
        public Vector2 Offset { get { return offset; } }
        public Transform MyTransform { get { return myTransform; } }
        public GameObject MyObject { get { return myObject; } }
        public float Width { get { return width; } }
        public float Height { get { return height; } }

        public CollisionBox(List<string> t, bool check, Vector2 off, Transform transform, float width, float height, int uo) : base(uo)
        {
            tags = t;
            checkCollision = check;
            offset = off;
            this.myTransform = transform;
            this.width = width;
            this.height = height;
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
            
        }

        public Vector2 TopLeft()
        {
            return myTransform.Position - new Vector2(width/2, height/2) + offset;
        }

        public Vector2 TopRight()
        {
            return myTransform.Position + offset + new Vector2(width/2, -height/2);
        }

        public Vector2 BottomLeft()
        {
            return myTransform.Position + offset + new Vector2(-width/2, height/2);
        }

        public Vector2 BottomRight()
        {
            return myTransform.Position + offset + new Vector2(width/2, height/2);
        }

        public void Resize(float width, float height)
        {
            this.width = width;
            this.height = height;
        }
    }
}
