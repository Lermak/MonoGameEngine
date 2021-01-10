using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Diagnostics;

<<<<<<< HEAD
namespace MonoGame_Core.Scripts
=======
namespace GEJam.Scripts
>>>>>>> c1b8f6f68bc0e41355e957b11df0ccaba139105d
{
    public static class CollisionManager
    {
        public static List<CollisionHandler> collisionHandlers;

        public static void Initilize()
        {
            collisionHandlers = new List<CollisionHandler>();
        }

        public static Vector2 CheckCollision(CollisionBox a, CollisionBox b)
        {
            if (a.TopLeft().X < b.TopRight().X &&
                a.TopRight().X > b.TopLeft().X &&
                a.TopRight().Y < b.BottomRight().Y &&
                a.BottomRight().Y > b.TopRight().Y)
                return new Vector2(a.TopLeft().X - b.TopRight().X < a.TopRight().X - b.TopLeft().X ? a.TopLeft().X - b.TopRight().X : a.TopRight().X - b.TopLeft().X,
                                    a.TopLeft().Y - b.BottomRight().Y < a.BottomRight().Y - b.TopLeft().Y ? a.TopLeft().Y - b.BottomRight().Y : a.BottomRight().Y - b.TopLeft().Y);

            return new Vector2();
        }

        public static void Update()
        {
            Vector2 v = new Vector2();
            for (int i = 0; i < collisionHandlers.Count; ++i)//check each handler
            {
                for(int k = 0; k < collisionHandlers[i].CollisionBoxs.Count; ++k)//and each box that handler has
                {
                    if (collisionHandlers[i].CollisionBoxs[k].CheckCollision)
                    {
                        for (int z = i+1; z < collisionHandlers.Count; ++z)//against every other handler that hasn't been checked yet
                        {
                            for (int m = 0; m < collisionHandlers[z].CollisionBoxs.Count; ++m)//and each active collision box in that handler
                            {
                                v = CheckCollision(collisionHandlers[i].CollisionBoxs[k], collisionHandlers[z].CollisionBoxs[m]);

                                if (v != Vector2.Zero)
                                {
                                    collisionHandlers[i].AddCollision(collisionHandlers[i].CollisionBoxs[k], collisionHandlers[z].CollisionBoxs[m], v);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
