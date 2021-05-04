using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MonoGame_Core.Scripts
{
    public class PlayerPaddle : WorldObject
    {
        public PlayerPaddle(string texID, string tag, Vector2 size) : base(texID, tag, size)
        {
            Transform.Place(new Vector2(60 - RenderingManager.WIDTH / 2, 0));

            ComponentHandler.AddComponent(new CollisionBox(this, 0, "Paddle", false));
            behaviorHandler.AddBehavior(new PaddleControlls(0, RigidBody));

            CollisionHandler.myActions.Add(new CollisionActions("Paddle", new List<string>() { "Wall" }, new List<collisionAction>() { CollisionBehaviors.UndoMinPen }));
        }
    }
}
