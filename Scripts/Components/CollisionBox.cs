﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace MonoGame_Core.Scripts
{
    public class CollisionBox : Collider
    {
        float width;
        float height;

        public float Width { get { return width * transform.Scale.X; } }
        public float Height { get { return height * transform.Scale.Y; } }
        public float Angle { get { return (float)(Math.Acos((Width / 2) / Radius)) * (180 / (float)Math.PI); } }
        public override float Radius { get { return (float)Math.Sqrt(Math.Pow(Height / 2, 2) + Math.Pow(Width / 2, 2)); } }

        public CollisionBox(GameObject go, string name, List<string> t, bool check, Vector2 off, Transform transform, float width, float height, int uo, bool isStatic) : base(go, uo, name, isStatic)
        {
            checkCollision = check;
            offset = off;
            this.transform = transform;
            this.width = width;
            this.height = height;
        }

        public CollisionBox(GameObject go, string name, int uo, GameObject myObj, Transform myTrans, bool isStatic) : base(go, uo, name, isStatic)
        {
            transform = myTrans;
            gameObject = myObj;
            checkCollision = true;
            offset = new Vector2();
            width = myTrans.Width;
            height = myTrans.Height;
        }

        public CollisionBox(WorldObject myObj, int uo, string name, bool isStatic) : base(myObj, uo, name, isStatic)
        {                       
            transform = myObj.Transform;
            gameObject = myObj;
            checkCollision = true;
            offset = new Vector2();
            width = transform.Width;
            height = transform.Height;
        }

        public CollisionBox(GameObject go, Transform trans, string name) : base(go, 0, name, true)
        {
            transform = trans;
            gameObject = null;
            checkCollision = true;
            offset = new Vector2();
            width = transform.Width;
            height = transform.Height;
        }

        public override List<Vector2> Axies()
        {
            return new List<Vector2>() { hf_Math.getRotationPosition(0, 1, -transform.Rotation, new Vector2()), hf_Math.getRotationPosition(90, 1, -transform.Rotation, new Vector2()) };
        }

        public override List<Vector2> Verticies()
        {
            return new List<Vector2>() { TopRight(), TopLeft(), BottomLeft(), BottomRight() };
        }

        public Vector2 TopRight()
        {
            return hf_Math.getRotationPosition(Angle, Radius, -transform.Rotation, transform.Position + offset);
        }

        public Vector2 TopLeft()
        {
            return hf_Math.getRotationPosition((180 - Angle), Radius, -transform.Rotation, transform.Position + offset);//use half side angle because at 0 rotation the box should be cut through the middle, so only half the side angle is needed
        }

        public Vector2 BottomLeft()
        {
            return hf_Math.getRotationPosition(180 + Angle, Radius, -transform.Rotation, transform.Position + offset);
        }

        public Vector2 BottomRight()
        {
            return hf_Math.getRotationPosition(360 - Angle, Radius, -transform.Rotation, transform.Position + offset);
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
        }

        public override void OnDestroy()
        {
            if (isStatic)
                CollisionManager.ActiveStaticColliders.Remove(this);
            else
                CollisionManager.ActiveMovingColliders.Remove(this);
            base.OnDestroy();
        }
    }
}
