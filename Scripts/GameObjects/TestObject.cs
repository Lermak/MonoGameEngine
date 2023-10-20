using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Core.Scripts
{
    public class TestObject : WorldObject
    {
        public TestObject(string texID, string name) : base(texID, name, new string[] { "testObject" }, new Vector2(0,0), 1)
        {
            //ComponentHandler.Add(new CollisionBox(this, "myBox", false, new Vector2(40,40)));
            ComponentHandler.Add(new CollisionCircle("myBox", this, 10, new Vector2(), false, new List<string> { "myBox" }));
            ComponentHandler.Add(new Movement(this, "Movement", 500, 360));
            ComponentHandler.Add(new FontRenderer("FontRenderer", this, "Test", "TestFont", new Vector2(1920,1080),1));
            Transform.SetScale(3, 3);
            //BehaviorHandler.AddBehavior("MoveControls", Behaviors.WASDcontrols, new Component[] { RigidBody, componentHandler.GetComponent("Movement") });
            BehaviorHandler.Add("FaceMouse", Behaviors.PointAtMouse);
            behaviorHandler.Add("MoveForward", Behaviors.MoveTowardRotation);
            BehaviorHandler.Add("Scaler", Behaviors.ManualScale);
            //SpriteRenderer.Shader = "TestShader";

            ((CollisionHandler)GetComponent("collisionHandler")).myActions.Add(new CollisionActions("myBox", new List<string> { "myBox", "TileWall", "tile" }, new List<CollisionAction> { CollisionBehaviors.UndoMinPen }));

        }
    }
}
