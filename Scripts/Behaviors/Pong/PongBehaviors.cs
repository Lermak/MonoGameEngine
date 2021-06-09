using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public static class PongBehaviors
    {
        public static void BallLaunch(float gt, Component[] c)
        {
            RigidBody rb = (RigidBody)c[0];
            Movement m = (Movement)c[1];

            if (rb.MoveVelocity == new Vector2() && InputManager.IsKeyPressed(Keys.Space))
            {
                Random r = new Random();
                Vector2 dir = new Vector2((r.Next(-1, 1) + .1f), (r.Next(-1, 1) + .1f));
                dir.Normalize();
                rb.MoveVelocity = (new Vector2(m.Speed, m.Speed) * dir * TimeManager.DeltaTime);
            }
        }

        public static void VerticalFollow(float gt, Component[] c)
        {
            RigidBody rb1 = (RigidBody)c[0];
            RigidBody rb2 = (RigidBody)c[1];
            Movement m = (Movement)c[2];

            Vector2 v = new Vector2();

            v.Y = Math.Min(Math.Abs(rb2.MoveVelocity.Y), m.Speed * TimeManager.DeltaTime);
            if (rb2.MoveVelocity.Y < 1)
                v.Y *= -1;
            rb1.MoveVelocity = v;
        }
        public static void Movement(float gt, Component[] c)
        {
            RigidBody rb = (RigidBody)c[0];
            Movement m = (Movement)c[1];

            Vector2 v = new Vector2();

            if (InputManager.IsKeyPressed(Keys.W))
                v.Y = -(m.Speed * gt);
            else if (InputManager.IsKeyPressed(Keys.S))
                v.Y = (m.Speed * gt);


            rb.MoveVelocity = v;
        }
    }
}
