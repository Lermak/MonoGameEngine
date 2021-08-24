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
        protected bool isStatic;
        protected Vector2 offset;
        protected Transform transform;
        
        protected float radius;

        public bool CheckCollision { get { return checkCollision; } }
        public bool IsTrigger { get { return isTrigger; } }
        public bool IsStatic { get { return isStatic; } }
        public Vector2 Offset { get { return offset * transform.Scale; } }
        public Transform Transform { get { return transform; } }
        public virtual float Radius { get { return radius; } }

        public abstract List<Vector2> Verticies();

        public abstract List<Vector2> Axies();


        public Collider(GameObject go, Transform t, int uo, string name, bool isStatic) : base(go, uo, name)
        {
            transform = t;
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
                gameObject.BehaviorHandler.AddBehavior("activeCollider", addToActiveColliders, new Component[] { this });
            }

            base.Initilize();
        }

        private static void addToActiveColliders(float gt, Component[] c)
        {
            if(((Collider)c[0]).checkCollision)
                CollisionManager.ActiveColliders.Insert((Collider)c[0]);
        }
    }
}
