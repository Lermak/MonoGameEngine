using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace MonoGame_Core.Scripts
{
    public delegate void collisionAction(Collider a, Collider b, Vector2 p);

    public struct Collision
    {
        public Vector2 Depth;
        public Collider a;
        public Collider b;

        public Collision(Vector2 d, Collider a, Collider b)
        {
            Depth = d;
            this.a = a;
            this.b = b;
        }
    }

    public struct CollisionActions 
    {
        public string MyBox;
        public List<string> OtherBoxs;
        public List<collisionAction> Actions;

        public CollisionActions(string a, List<string> b, List<collisionAction> ca)
        {
            this.MyBox = a;
            this.OtherBoxs = b;
            this.Actions = ca;
        }
    }

    public class CollisionHandler : Component
    {
        public List<CollisionActions> myActions;

        public CollisionHandler(GameObject myObj) : base(myObj, "collisionHandler")
        {
            myActions = new List<CollisionActions>();
        }

        public void RunCollisionActions(Collider b1, Collider b2, Vector2 v)
        {
            foreach(CollisionActions ca in myActions)
            {
                if(ca.MyBox == b1.Name)
                {
                    if(ca.OtherBoxs.Contains(b2.Name))
                    {
                        foreach(collisionAction c in ca.Actions)
                        {
                            c(b1, b2, v);
                        }
                    }
                }
            }
        }
        public void RunCollisionActions(Collider b1, string t, Vector2 v)
        {
            foreach (CollisionActions ca in myActions)
            {
                if (ca.MyBox == b1.Name)
                {
                    if (ca.OtherBoxs.Contains(t))
                    {
                        foreach (collisionAction c in ca.Actions)
                        {
                            c(b1, null, v);
                        }
                    }
                }
            }
        }
    }
}
