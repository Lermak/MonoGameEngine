using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Core.Scripts
{
    public class TestObject : WorldObject
    {
        public TestObject(string texID, string tag) : base(texID, tag, new Vector2(40,40), new Vector2(0,0), 1)
        {
            BehaviorHandler.AddBehavior(new TestControls(this, 0, "testControls", RigidBody));
            BehaviorHandler.AddBehavior(new ManuallyScale(this, 1, "scaler", Transform));
            ComponentHandler.AddComponent(new CollisionBox(this, 0, "myBox", false));
            ComponentHandler.AddComponent(new FontRenderer(this, "Test", "TestFont", Transform, new Vector2(0, 100), new Vector2(1920,1080),1,Color.Red,1));
            SpriteRenderer.Shader = "TestShader";
            ((CollisionHandler)ComponentHandler.GetComponent("collisionHandler")).myActions.Add(new CollisionActions("myBox", new List<string> { "myBox", "TileWall", "tile" }, new List<collisionAction> { CollisionBehaviors.UndoMinPen }));
        }
    }
}
