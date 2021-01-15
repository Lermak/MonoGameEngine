using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public class TestPlayer:WorldObject
    {
        public TestPlayer(string texID, string tag) : base(texID, tag)
        {
            CollisionHandler = new CollisionHandler(this);
            CollisionHandler.AddCollisionBox(new CollisionBox("testBox", true, new Vector2(), Transform, 40, 40, CollisionHandler, 0));

            componentHandler.Components["spriteRenderer"] = new SpriteRenderer(texID,
                                    Transform,
                                    new Vector2(0, 0),
                                    new Vector2(40, 40),
                                    0,
                                    0,
                                    0);
            behaviorHandler.AddBehavior("movement", new WASDmovement(2, Transform, 60));

            Transform.Position = new Vector2(100, 100);

            CollisionHandler.myActions.Add(new CollisionActions("testBox", new List<string> { "testPassiveBox" }, new List<collisionAction> { CollisionBehaviors.UndoMinPen, CollisionBehaviors.PlayHitSound }));
        }
}
}
