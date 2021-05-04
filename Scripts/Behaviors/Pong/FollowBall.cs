using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
namespace MonoGame_Core.Scripts
{
    class FollowBall : Behavior
    {
        RigidBody rigidBody;
        float speed = 500;
        Ball toFollow;
        Transform transform;
        public FollowBall(int uo, RigidBody rb, Ball b, Transform t) : base(uo, "followBall")
        {
            transform = t;
            rigidBody = rb;
            toFollow = b;
        }

        public override void Update(float gt)
        {
            Vector2 v = new Vector2();

            v.Y = Math.Min(Math.Abs(toFollow.RigidBody.MoveVelocity.Y), speed*TimeManager.DeltaTime);
            if (toFollow.RigidBody.MoveVelocity.Y < 1)
                v.Y *= -1;
            rigidBody.UpdateVelocity(v);
            base.Update(gt);
        }
    }
}
