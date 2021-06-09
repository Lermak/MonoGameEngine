using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MonoGame_Core.Scripts
{
    public class PlayerPaddle : WorldObject
    {
        public PlayerPaddle(string texID, string tag, Vector2 size, Vector2 pos, byte layer) : base(texID, tag, size, pos, layer)
        {
            ComponentHandler.AddComponent(new CollisionBox(this, 0, "Paddle", false));
            ComponentHandler.AddComponent(new Movement(this, 0, "Movement", 500));

            BehaviorHandler.AddBehavior("PaddleControls", PongBehaviors.Movement, new Component[] { RigidBody, componentHandler.GetComponent("Movement") });

            CollisionHandler.myActions.Add(new CollisionActions("Paddle", new List<string>() { "TopWall", "BottomWall" }, new List<collisionAction>() { CollisionBehaviors.UndoMinPen }));
        }
    }
}
