using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public class CollisionSphere : Collider
    {
        public override float Radius { get { return radius; } }
        public CollisionSphere(int uo, string name, GameObject go, Vector2 offset, bool checkCollision, bool isStatic, bool isTrigger) : base(go, uo, name, isStatic)
        {
            this.offset = offset;
            this.checkCollision = checkCollision;
            this.isTrigger = isTrigger;
            gameObject = go;
            transform = (Transform)go.ComponentHandler.GetComponent("transform");
            radius = transform.Width/2;
        }

        public override List<Vector2> Axies()
        {
            List<Vector2> v = new List<Vector2>();
            for (int i = 0; i < 90; ++i)
            {
                v.Add(hf_Math.getRotationPosition(0, 1, -i * 2, new Vector2()));
            }
            return v;
        }
        public override List<Vector2> Verticies()
        {
            List<Vector2> v = new List<Vector2>();
            for(int i = 0; i < 6; ++i)
            {
                v.Add(hf_Math.getRotationPosition(i*30, radius, 0, transform.Position + offset));
            }
            return v;
        }
    }
}
