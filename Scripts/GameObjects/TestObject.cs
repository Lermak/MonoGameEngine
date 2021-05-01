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
            SpriteRenderer.DrawArea = new Vector2(40, 40);
            BehaviorHandler.AddBehavior(new TestControls(0, RigidBody));
            BehaviorHandler.AddBehavior(new ManuallyScale(1, "scaler", Transform));
            ComponentHandler.AddComponent(new CollisionBox(this, 0, "myBox", false));

            
            SpriteRenderer.Shader = SceneManager.CurrentScene.Effects["TestShader"];
            SpriteRenderer.Target = RenderingManager.RenderTargets[0];

            ((CollisionHandler)ComponentHandler.GetComponent("collisionHandler")).myActions.Add(new CollisionActions("myBox", new List<string> { "myBox" }, new List<collisionAction> { CollisionBehaviors.UndoMinPen }));
        }
    }
}
