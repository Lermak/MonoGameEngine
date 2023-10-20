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

        protected bool isStatic;
        protected Vector2 offset;
        protected Transform transform;
        
        protected float radius;

        public bool IsStatic { get { return isStatic; } }
        public Vector2 Offset { get { return offset * transform.Scale; } }
        public Transform Transform { get { return transform; } }
        public float Hypotenuse { get { return hf_Math.Hypot(Width/2,Height/2); } }
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
            GameObject g = new GameObject("mouse", new string[] { });
            g.AddComponent(new Transform(g, InputManager.MousePos, 0, this.transform.Layer));
            Collider c = (Collider)g.AddComponent(new CollisionCircle("mouse", g, 1, new Vector2(), false));

            Vector2 pen = new Vector2();
            CollisionManager.SATcollision(c, this, out pen);
            return CollisionManager.SATcollision(c, this, out pen);
        }

        private static void addToActiveColliders(float dt, GameObject go, Component[] c)
        {
            CollisionManager.ActiveColliders.Insert((Collider)c[0]);
        }
    }
}
