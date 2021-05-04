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

        public Vector2 Position { get { return position;} }    
        public float Width { get { return width; } }
        public float Height { get { return height; } }
        public float Rotation { get { return rotation; } }
        public Vector2 Scale { get { return scale; } }
        public float Radius { get { return (float)Math.Sqrt(Math.Pow(Height / 2, 2) + Math.Pow(Width / 2, 2)); } }

        public Transform(int uo, Vector2 pos, float w, float h, float r) : base(uo, "transform")
        {
            rotation = r;
            position = pos;
            width = w;
            height = h;
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
    }
}
