using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoGame_Core.Scripts
{
    class BallLaunch : Behavior
    {
        RigidBody rigidBody;
        float speed = 300;
        public bool isLanch = false;

        public BallLaunch(GameObject go, int uo, RigidBody rb) : base(go, uo, "ballLaunch")
        {
            rigidBody = rb;
        }

        public override void Update(float gt)
        {
            if (!isLanch && InputManager.IsKeyPressed(Keys.Space))
            {
                Random r = new Random();
                isLanch = true;
                Vector2 dir = new Vector2((r.Next(-1, 1) + .1f), (r.Next(-1, 1) + .1f));
                dir.Normalize();
                rigidBody.MoveVelocity = (new Vector2(speed, speed) * dir * TimeManager.DeltaTime);
            }

            base.Update(gt);
        }
    }
}
