using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

<<<<<<< HEAD
namespace MonoGame_Core.Scripts
=======
namespace GEJam.Scripts
>>>>>>> c1b8f6f68bc0e41355e957b11df0ccaba139105d
{
    class SampleWorldObject: WorldObject
    {
        public CollisionHandler CollisionHandler;

        public SampleWorldObject(string texID, string tag) : base(texID, tag)
        {
            CollisionHandler = new CollisionHandler(this);
            CollisionHandler.AddCollisionBox(new CollisionBox("testPassiveBox", false, new Vector2(), Transform, 40, 40, CollisionHandler, 0));
            componentHandler.Components["spriteRenderer"] = new SpriteRenderer(texID,
                                    Transform,
                                    new Vector2(0, 0),
                                    new Vector2(40, 40),
                                    0,
                                    0,
                                    0);
            
        }
    }
}
