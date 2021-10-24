using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public abstract class Collider : Component
    {
        protected float width;
        protected float height;

        protected bool checkCollision;
        protected bool isStatic;
        protected Vector2 offset;
        protected Transform transform;
        
        protected float radius;

        public bool CheckCollision { get { return checkCollision; } }
        public bool IsStatic { get { return isStatic; } }
        public Vector2 Offset { get { return offset * transform.Scale; } }
        public Transform Transform { get { return transform; } }
        public float Hypotenuse { get { return hf_Math.Hypotenuse(Width/2,Height/2); } }
        public float Width { get { return width * transform.Scale.X; } }
        public float Height { get { return height * transform.Scale.Y; } }

        public abstract List<Vector2> Verticies();

        public abstract List<Vector2> Axies();


        public Collider(GameObject go, string name, bool isStatic) : base(go, name)
        {
            transform = (Transform)go.ComponentHandler.Get("transform");
            this.isStatic = isStatic;
        }

        public override void Initilize()
        {
            if (isStatic)
            {
                 CollisionManager.PassiveColliders.Insert(this);
            }
            else
            {
                gameObject.AddBehavior("activeCollider", addToActiveColliders, new Component[] { this });
            }

            base.Initilize();
        }
        public virtual bool ContainsPoint(Vector2 v)
        {
            return v.X > transform.Position.X - Width / 2 &&
                    v.X < transform.Position.X + Width / 2 &&
                    v.Y > transform.Position.Y - Height / 2 &&
                    v.Y < transform.Position.Y + Height / 2;
        }

        private static void addToActiveColliders(float dt, GameObject go, Component[] c)
        {
            if(((Collider)c[0]).checkCollision)
                CollisionManager.ActiveColliders.Insert((Collider)c[0]);
        }
    }
}
