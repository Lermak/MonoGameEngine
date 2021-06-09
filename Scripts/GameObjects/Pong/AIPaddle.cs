using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MonoGame_Core.Scripts
{
    public class AIPaddle : WorldObject
    {
        public AIPaddle(string texID, string tag, Vector2 size, Vector2 pos, byte layer) : base(texID, tag, size, pos, layer)
        {

            ComponentHandler.AddComponent(new CollisionBox(this, 0, "Paddle", false));
            ComponentHandler.AddComponent(new Movement(this, 0, "Movement", 1000));

            behaviorHandler.AddBehavior("FollowBall", PongBehaviors.VerticalFollow, new Component[] { RigidBody, (RigidBody)SceneManager.CurrentScene.GameObjects["Ball"].ComponentHandler.GetComponent("rigidBody"), componentHandler.GetComponent("Movement") });
            
            CollisionHandler.myActions.Add(new CollisionActions("Paddle", new List<string>() { "TopWall", "BottomWall" }, new List<collisionAction>() { CollisionBehaviors.UndoMinPen }));
        }
    }
}
