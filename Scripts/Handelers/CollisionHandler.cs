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
        WorldObject myObject;
        public List<CollisionActions> myActions;

        public CollisionHandler(int uo, WorldObject myObj) : base(myObj, uo, "collisionHandler")
        {
            myObject = myObj;
            myActions = new List<CollisionActions>();
        }

        public void RunCollisionActions(Collider b1, Collider b2, Vector2 v)
        {
            foreach(CollisionActions ca in myActions)
            {
                if(ca.MyBox == b1.Name)
                {
                    foreach(string s in ca.OtherBoxs)
                    {
                        if(b2.Name == "" || s == b2.Name)
                        {
                            foreach(collisionAction c in ca.Actions)
                            {
                                c(b1, b2, v);
                            }
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
                    foreach (string s in ca.OtherBoxs)
                    {
                        if(t == s)
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
        public void Update(GameTime gt)
        {
           
        }
    }
}
