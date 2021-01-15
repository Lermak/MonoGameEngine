using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public class Transform : Component
    {
        Vector2 position;
        Vector2 velocity;
        float width;
        float height;

        public Vector2 Position { get { return position;} }    
        public Vector2 Velocity { get { return velocity; } }
        public float Width { get { return width; } }
        public float Height { get { return height; } }
        
        public Transform(int uo, Vector2 pos, float w, float h) : base(uo)
        {
            position = pos;
            width = w;
            height = h;
        }
        //Manually add to velocity
        public void AddVelocity(float x, float y)
        {
            velocity += new Vector2(x, y);
        }
        public void AddXVelocity(float x)
        {
            velocity.X += x;
        }
        public void AddYVelocity(float y)
        {
            velocity.Y += y;
        }
        //Manually set velocity
        public void SetVelocity(float x, float y)
        {
            velocity = new Vector2(x, y);
        }
        public void SetXVelocity(float x)
        {
            velocity.X = x;
        }
        public void SetYVelocity(float y)
        {
            velocity.Y = y;
        }

        public void Resize(float x, float y)
        {
            width = x;
            height = y;
        }
    }
}
