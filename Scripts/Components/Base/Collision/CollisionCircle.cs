using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public class CollisionCircle : Collider
    {        
        public CollisionCircle(string name, GameObject go, float radius, Vector2 offset, bool isStatic) : base(go, name, isStatic)
        {
            this.offset = offset;
            gameObject = go;
            width = radius * 2;
            height = radius * 2;
        }

        public override List<Vector2> Axies()
        {
            List<Vector2> v = new List<Vector2>();
            for (int i = 0; i < 90; ++i)
            {
                v.Add(hf_Math.GetPosFromPoint(i * 2, 1, new Vector2()));
            }
            return v;
        }
        public override List<Vector2> Verticies()
        {
            List<Vector2> v = new List<Vector2>();
            for(int i = 0; i < 6; ++i)
            {
                v.Add(hf_Math.GetPosFromPoint(i*60, Hypotenuse, transform.Position + offset));
            }
            return v;
        }
    }
}
