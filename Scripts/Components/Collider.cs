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


        public Collider(GameObject go, int uo, string name, bool isStatic) : base(go, uo, name)
        {
            if (isStatic)
                CollisionManager.ActiveStaticColliders.Add(this);
            else
                CollisionManager.ActiveMovingColliders.Add(this);

            this.isStatic = isStatic;
        }
        public override void Update(float gt)
        {
            foreach (Camera c in CameraManager.Cameras)
            {
                if (Vector2.Distance(transform.Position, c.Transform.Position) <= transform.Radius + c.Transform.Radius)
                {
                    if (!isStatic)
                        CollisionManager.ActiveMovingColliders.Add(this);
                    break;
                }
            }
        }
    }
}
