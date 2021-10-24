using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public class Transform : Component
    {
        Vector2 position;
        float radians;
        Vector2 scale = new Vector2(1,1);
        Transform parent;
        float degreesFromParent = 0f;
        float startingRotation = 0f;
        float distanceToParent = 0f;
        byte layer;

        public Vector2 Position { get {
                if (parent == null)
                    return position;
                else
                    return hf_Math.getRotationPosition(hf_Math.RadiansToDegres(degreesFromParent + parent.radians), distanceToParent, parent.position);
            } }    
        public float Radians { get {
                if (parent == null)
                    return radians;
                else
                    return radians + parent.radians - startingRotation;
            }
            set { radians = value; }
        }
        public Vector2 Scale { get { return scale; } }
        public Transform Parent { get { return parent; } }
        public byte Layer { get { return layer; } set { layer = value; } }
        public Transform(GameObject go, Vector2 pos, float degrees, byte l) : base(go, "transform")
        {
            radians = hf_Math.DegreesToRadians(degrees);
            position = pos;

            layer = l;
        }

        public void SetScale(float x, float y)
        {
            scale = new Vector2(x, y);
        }
        public void Move(Vector2 dist)
        {
            position += dist;
        }

        public void Place(Vector2 pos)
        {
            position = pos;
        }
        public void Rotate(float r)
        {
            radians += r;
        }
        public Vector2 WorldPosition()
        {
            return Position * RenderingManager.GameScale * new Vector2(1,-1);
        }
        public void AttachToTransform(Transform t)
        {
            parent = t;
            startingRotation = t.radians;
            degreesFromParent = hf_Math.getAngle(t.position, position) - t.radians;
            distanceToParent = Vector2.Distance(position, t.position);
            position = position - t.position;
        }
        public void DetachFromParent()
        {
            position = Position;
            radians = Radians;
            parent = null;
            degreesFromParent = 0;
            startingRotation = 0;          
        }
    }
}
