using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public static class Behaviors
    {
        public static void ArrowControls(float gt, Component[] c)
        {
            KeyboardState state = Keyboard.GetState();
            Vector2 v = new Vector2();
            Movement m = (Movement)c[1];
            if (InputManager.IsKeyPressed(Keys.Up))
                v.Y = -m.Speed * gt;
            else if (InputManager.IsKeyPressed(Keys.Down))
                v.Y = m.Speed * gt;
            if (InputManager.IsKeyPressed(Keys.Left))
                v.X = -m.Speed * gt;
            else if (InputManager.IsKeyPressed(Keys.Right))
                v.X = m.Speed * gt;

            ((Transform)c[0]).Move(v);
        }

        public static void WASDcontrols(float gt, Component[] c)
        {
            Movement m = (Movement)c[1];

            KeyboardState state = Keyboard.GetState();
            Vector2 v = new Vector2();
            float r = 0;
            if (state.IsKeyDown(Keys.W))
                v.Y = -(m.Speed * gt);
            else if (state.IsKeyDown(Keys.S))
                v.Y = (m.Speed * gt);
            if (state.IsKeyDown(Keys.A))
                v.X = -(m.Speed * gt);
            else if (state.IsKeyDown(Keys.D))
                v.X = (m.Speed * gt);

            if (state.IsKeyDown(Keys.Q))
                r = (1 * gt);
            else if (state.IsKeyDown(Keys.E))
                r = -(1 * gt);

            RigidBody rb = (RigidBody)c[0];

            rb.MoveVelocity = v;
            rb.AngularVelocity = r;
        }

        public static void ManualScale(float gt, Component[] c)
        {
            Transform t = (Transform)c[0];
            if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Add) && t.Scale.X < 5)
            { t.SetScale(t.Scale.X + .1f, t.Scale.Y + .1f); }
            if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Subtract) && t.Scale.X > 0)
            { t.SetScale(t.Scale.X - .1f, t.Scale.Y - .1f); }

        }

        public static void ScreenShake(float gt, Component[] c)
        {
            Transform t = (Transform)c[0];

            if (InputManager.IsKeyTriggered(Microsoft.Xna.Framework.Input.Keys.Space))
                CoroutineManager.AddCoroutine(Coroutines.ScreenShake(.1f, -10, 10, t), "screenShake", 0, true);
        }

        public static void PointAtMouse(float gt, Component[] c)
        {
            Transform t = (Transform)c[0];
            if (t.Parent.Parent != null)
                t.Rotate(hf_Math.getAngle(t.Position, InputManager.MousePos) - 90 * (float)Math.PI / 180 - t.Parent.Radians);
        }

        public static void FaceTransform(float gt, Component[] c)
        {
            Transform t = (Transform)c[0];
            Transform t2 = (Transform)c[1];
            RigidBody rb = (RigidBody)c[2];


            float newRot = (hf_Math.getAngle(t.Position - t2.Position, new Vector2(1, 0))) - 90 * (float)Math.PI / 180;
            rb.AngularVelocity = (newRot - t.Radians) / 1 * gt;
        }

        public static void MoveTowardRotation(float gt, Component[] c)
        {
            Transform t = (Transform)c[0];
            RigidBody rb = (RigidBody)c[1];

            rb.MoveVelocity = hf_Math.RadiansToUnitVector(t.Radians + 90 * (float)Math.PI / 180) * gt * 100;
        }
    }
}
