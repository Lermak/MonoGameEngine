using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MonoGame_Core.Scripts
{
    public class Ball : WorldObject
    {
        public Ball(string texID, string tag, Vector2 position, Vector2 size, byte layer) : base(texID, tag, size, position, layer)
        {
            ComponentHandler.AddComponent(new CollisionSphere(0, "Ball", this, new Vector2(), true, false, false));
            ComponentHandler.AddComponent(new Movement(this, 0, "Movement", 700));

            BehaviorHandler.AddBehavior("BallLaunch", PongBehaviors.BallLaunch, new Component[] { RigidBody, componentHandler.GetComponent("Movement") });

            CollisionHandler.myActions.Add(new CollisionActions("Ball", new List<string>() { "TopWall", "BottomWall", "Paddle" }, new List<collisionAction>() { CollisionBehaviors.Reflect }));
            CollisionHandler.myActions.Add(new CollisionActions("Ball", new List<string>() { "LeftWall", "RightWall" }, new List<collisionAction>() { CollisionBehaviors.ScorePoints }));
        }

        public void Score(bool forPlayer)
        {
            Transform.Place(new Vector2());
            RigidBody.MoveVelocity = new Vector2();
            if (forPlayer)
                ((Score)SceneManager.CurrentScene.GameObjects["PlayerScore"]).Points++;
            else
                ((Score)SceneManager.CurrentScene.GameObjects["OpponentScore"]).Points++;
        }
    }
}
