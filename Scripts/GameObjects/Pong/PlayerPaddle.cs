using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MonoGame_Core.Scripts
{
    public class PlayerPaddle : WorldObject
    {
        public PlayerPaddle(string texID, string tag, Vector2 size, Vector2 pos, byte layer) : base(texID, tag, size, pos, layer)
        {
            ComponentHandler.AddComponent(new CollisionBox(this, 0, "Paddle", false));
            behaviorHandler.AddBehavior(new PaddleControlls(this, 0, "paddleControls", RigidBody));

            CollisionHandler.myActions.Add(new CollisionActions("Paddle", new List<string>() { "TopWall", "BottomWall" }, new List<collisionAction>() { CollisionBehaviors.UndoMinPen }));
        }
    }
}
