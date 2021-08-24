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
            ComponentHandler.AddComponent(new CollisionBox(this, 0, "myBox", false));
            ComponentHandler.AddComponent(new Movement(this, 0, "Movement", 500));
            ComponentHandler.AddComponent(new FontRenderer(this, "Test", "TestFont", Transform, new Vector2(0, 100), new Vector2(1920,1080),1,Color.Red,1));

            //BehaviorHandler.AddBehavior("MoveControls", Behaviors.WASDcontrols, new Component[] { RigidBody, componentHandler.GetComponent("Movement") });
            BehaviorHandler.AddBehavior("FaceMouse", Behaviors.PointAtMouse, new Component[] { Transform });
            behaviorHandler.AddBehavior("MoveForward", Behaviors.MoveTowardRotation, new Component[] { Transform, RigidBody });
            BehaviorHandler.AddBehavior("Scaler", Behaviors.ManualScale, new Component[] { Transform });
            //SpriteRenderer.Shader = "TestShader";
            ((CollisionHandler)ComponentHandler.GetComponent("collisionHandler")).myActions.Add(new CollisionActions("myBox", new List<string> { "myBox", "TileWall", "tile" }, new List<collisionAction> { CollisionBehaviors.UndoMinPen }));
        }
    }
}
