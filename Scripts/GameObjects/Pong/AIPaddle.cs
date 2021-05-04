using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MonoGame_Core.Scripts
{
    public class AIPaddle : WorldObject
    {
        public AIPaddle(string texID, string tag, Vector2 size) : base(texID, tag, size)
        {
            Transform.Place(new Vector2(RenderingManager.WIDTH / 2 - 60, 0));

            ComponentHandler.AddComponent(new CollisionBox(this, 0, "Paddle", false));
            behaviorHandler.AddBehavior(new FollowBall(0, RigidBody, (Ball)SceneManager.CurrentScene.GameObjects["Ball"], Transform));

            CollisionHandler.myActions.Add(new CollisionActions("Paddle", new List<string>() { "Wall" }, new List<collisionAction>() { CollisionBehaviors.UndoMinPen }));
        }
    }
}
