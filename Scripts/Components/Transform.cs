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

        public Transform(string name, int uo, Vector2 pos, float w, float h, float r) : base(uo, name)
        {
            rotation = r;
            position = pos;
            width = w;
            height = h;
        }
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
    }
}
