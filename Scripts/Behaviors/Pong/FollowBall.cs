using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

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

            if (toFollow.Transform.Position.Y < transform.Position.Y + 5)
                v = new Vector2(0, -speed) * TimeManager.DeltaTime;
            else
                v = new Vector2(0, speed) * TimeManager.DeltaTime;


            rigidBody.UpdateVelocity(v);
            base.Update(gt);
        }
    }
}
