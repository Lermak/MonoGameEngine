using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace MonoGame_Core.Scripts
{
    public class CollisionBox : Collider
    {
        private float cornerAngle { get { return (float)(Math.Acos((Width / 2) / Hypotenuse)) * (180 / (float)Math.PI); } }

        public CollisionBox(GameObject go, string name, bool isStatic, Vector2 size) : base(go, name, isStatic)
        {
            checkCollision = true;
            offset = new Vector2();
            width = size.X;
            height = size.Y;
        }

        public override List<Vector2> Axies()
        {
            return new List<Vector2>() { hf_Math.GetPosFromPoint(hf_Math.RadToDeg(transform.Radians), 1, new Vector2()), 
                                        hf_Math.GetPosFromPoint(hf_Math.RadToDeg(transform.Radians) + 90, 1, new Vector2()) };
        }

        public override List<Vector2> Verticies()
        {
            return new List<Vector2>() { TopRight(), TopLeft(), BottomLeft(), BottomRight() };
        }

        public Vector2 TopRight()
        {
            return hf_Math.GetPosFromPoint(hf_Math.RadToDeg(transform.Radians) + cornerAngle, Hypotenuse, transform.Position + offset);
        }

        public Vector2 TopLeft()
        {
            return hf_Math.GetPosFromPoint(hf_Math.RadToDeg(transform.Radians) + (180 - cornerAngle), Hypotenuse, transform.Position + offset);//use half side angle because at 0 rotation the box should be cut through the middle, so only half the side angle is needed
        }

        public Vector2 BottomLeft()
        {
            return hf_Math.GetPosFromPoint(hf_Math.RadToDeg(transform.Radians) + 180 + cornerAngle, Hypotenuse, transform.Position + offset);
        }

        public Vector2 BottomRight()
        {
            return hf_Math.GetPosFromPoint(hf_Math.RadToDeg(transform.Radians) + 360 - cornerAngle, Hypotenuse, transform.Position + offset);
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
