using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Core.Scripts
{
    public class TestObject : WorldObject
    {
        public TestObject(string texID, string tag) : base(texID, tag)
        {
            Transform.Resize(40, 40);
            SpriteRenderer.SetDrawArea(40, 40);
            BehaviorHandler.AddBehavior(new TestControls(0, RigidBody));
            BehaviorHandler.AddBehavior(new ManuallyScale(1, "scaler", Transform));
            ComponentHandler.AddComponent(new CollisionBox(this, 0, "myBox", false));
            ComponentHandler.AddComponent(new FontRenderer("Test", "TestFont", "TestFont", Transform, new Vector2(0, 100), new Vector2(1920,1080),0,Color.White,0,1));
            
            SpriteRenderer.Shader = "TestShader";

            ((CollisionHandler)ComponentHandler.GetComponent("collisionHandler")).myActions.Add(new CollisionActions("myBox", new List<string> { "myBox" }, new List<collisionAction> { CollisionBehaviors.UndoMinPen }));
        }
    }
}
