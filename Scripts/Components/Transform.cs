using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public class Transform : Component
    {
        Vector2 position;
        float width;
        float height;
        float rotation;
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
                    return hf_Math.getRotationPosition(degreesFromParent, distanceToParent, -parent.rotation, parent.position);
            } }    
        public float Width { get { return width; } }
        public float Height { get { return height; } }
        public float Rotation { get {
                if (parent == null)
                    return rotation;
                else
                    return rotation + parent.rotation - startingRotation;
            } }
        public Vector2 Scale { get { return scale; } }
        public float Radius { get { return (float)Math.Sqrt(Math.Pow(Height / 2, 2) + Math.Pow(Width / 2, 2)); } }
        public Transform Parent { get { return parent; } }
        public byte Layer { get { return layer; } set { layer = value; } }
        public Transform(int uo, Vector2 pos, float w, float h, float r, byte l) : base(uo, "transform")
        {
            rotation = r;
            position = pos;
            width = w;
            height = h;
            layer = l;
        }

        public void SetScale(float x, float y)
        {
            scale = new Vector2(x, y);
        }

        public void Resize(float x, float y)
        {
            width = x;
            height = y;
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
            rotation += r;
        }

        public Vector2 WorldPosition(Vector2 offSet)
        {
            return (Position
                + hf_Math.getRotationPosition(90, (float)Math.Sqrt(Math.Pow(offSet.X, 2) + Math.Pow(offSet.Y, 2)), -Rotation, new Vector2()) * Scale) * RenderingManager.GameScale;
        }

        public void AttachToTransform(Transform t)
        {
            parent = t;
            startingRotation = t.rotation;
            degreesFromParent = hf_Math.getAngle(t.position, position) - t.rotation;
            distanceToParent = Vector2.Distance(position, t.position);
            position = position - t.position;
        }
        public void DetachFromParent()
        {
            position = Position;
            rotation = Rotation;
            parent = null;
            degreesFromParent = 0;
            startingRotation = 0;
            
        }

    }
}
