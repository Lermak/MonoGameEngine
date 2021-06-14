using System;
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

        public CollisionBox(GameObject go, string name, int uo, Transform myTrans, bool isStatic) : base(go, myTrans, uo, name, isStatic)
        {
            checkCollision = true;
            offset = new Vector2();
            width = myTrans.Width;
            height = myTrans.Height;
        }

        public CollisionBox(WorldObject myObj, int uo, string name, bool isStatic) : base(myObj, myObj.Transform, uo, name, isStatic)
        {                       
            checkCollision = true;
            offset = new Vector2();
            width = transform.Width;
            height = transform.Height;
        }

        public CollisionBox(GameObject go, Transform trans, string name) : base(go, trans, 0, name, true)
        {
            checkCollision = true;
            offset = new Vector2();
            width = transform.Width;
            height = transform.Height;
        }

        public override List<Vector2> Axies()
        {
            return new List<Vector2>() { hf_Math.getRotationPosition(hf_Math.RadiansToDegres(transform.Radians), 1, new Vector2()), 
                                        hf_Math.getRotationPosition(hf_Math.RadiansToDegres(transform.Radians) + 90, 1, new Vector2()) };
        }

        public override List<Vector2> Verticies()
        {
            return new List<Vector2>() { TopRight(), TopLeft(), BottomLeft(), BottomRight() };
        }

        public Vector2 TopRight()
        {
            return hf_Math.getRotationPosition(hf_Math.RadiansToDegres(transform.Radians) + Angle, Radius, transform.Position + offset);
        }

        public Vector2 TopLeft()
        {
            return hf_Math.getRotationPosition(hf_Math.RadiansToDegres(transform.Radians) + (180 - Angle), Radius, transform.Position + offset);//use half side angle because at 0 rotation the box should be cut through the middle, so only half the side angle is needed
        }

        public Vector2 BottomLeft()
        {
            return hf_Math.getRotationPosition(hf_Math.RadiansToDegres(transform.Radians) + 180 + Angle, Radius, transform.Position + offset);
        }

        public Vector2 BottomRight()
        {
            return hf_Math.getRotationPosition(hf_Math.RadiansToDegres(transform.Radians) + 360 - Angle, Radius, transform.Position + offset);
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
            //if (isStatic)
                //CollisionManager.ActiveStaticColliders.Remove(this);
            //else
                //CollisionManager.ActiveMovingColliders.Remove(this);
            base.OnDestroy();
        }
    }
}
