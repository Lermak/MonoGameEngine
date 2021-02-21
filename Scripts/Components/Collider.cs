using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public abstract class Collider : Component
    {
        protected bool checkCollision;
        protected bool isTrigger;
        protected Vector2 offset;
        protected Transform myTransform;
        protected GameObject myObject;

        public bool CheckCollision { get { return checkCollision; } }
        public bool IsTrigger { get { return isTrigger; } }
        public Vector2 Offset { get { return offset; } }
        public Transform MyTransform { get { return myTransform; } }
        public GameObject MyObject { get { return myObject; } }


        public abstract List<Vector2> Verticies();

        public abstract List<Vector2> Axies();


        public Collider(int uo, string name) : base(uo, name)
        {

        }
    }
}
