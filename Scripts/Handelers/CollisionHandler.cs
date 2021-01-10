using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

<<<<<<< HEAD
namespace MonoGame_Core.Scripts
=======
namespace GEJam.Scripts
>>>>>>> c1b8f6f68bc0e41355e957b11df0ccaba139105d
{
    public delegate void collisionAction(CollisionBox a, CollisionBox b, Vector2 p, WorldObject w);

    public struct Collision
    {
        public Vector2 Depth;
        public CollisionBox a;
        public CollisionBox b;

        public Collision(Vector2 d, CollisionBox a, CollisionBox b)
        {
            Depth = d;
            this.a = a;
            this.b = b;
        }
    }

    public struct CollisionActions
    {
        public string a;
        public List<string> b;
        public List<collisionAction> ca;

        public CollisionActions(string a, List<string> b, List<collisionAction> ca)
        {
            this.a = a;
            this.b = b;
            this.ca = ca;
        }
    }

    public class CollisionHandler
    {
        WorldObject myObject;
        List<Collision> collisions;
        List<CollisionBox> collisionBoxs;
        public List<CollisionActions> myActions;

        public List<CollisionBox> CollisionBoxs { get { return collisionBoxs; } }
        public CollisionHandler(WorldObject myObj)
        {
            CollisionManager.collisionHandlers.Add(this);
            myObject = myObj;
            collisionBoxs = new List<CollisionBox>();
            collisions = new List<Collision>();
            myActions = new List<CollisionActions>();
        }

        public void AddCollision(CollisionBox a, CollisionBox b, Vector2 pen)
        {
            collisions.Add(new Collision(pen, a, b));
        }

        public void AddCollisionBox(CollisionBox cb)
        {
            collisionBoxs.Add(cb);
        }

        public void Update(GameTime gt)
        {
            foreach(Collision c in collisions)
            {
                foreach (CollisionActions ca in myActions)
                {
                    if (ca.a == c.a.Tag)
                    {
                        foreach (string s in ca.b)
                        {
                            if (s == c.b.Tag || s == "*")
                            {
                                foreach (collisionAction a in ca.ca)
                                {
                                    a(c.a, c.b, c.Depth, myObject);
                                }
                            }
                        }
                    }
                }

                Debug.WriteLine("Collision");
            }
            collisions.Clear();
        }
    }
}
