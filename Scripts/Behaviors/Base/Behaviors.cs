using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public static class Behaviors
    {
        public static void Sample(float dt, GameObject go, Component[] c = null)
        {

        }

        public static void Animate(float dt, GameObject go, Component[] c = null)
        {
            AnimationData ad = (AnimationData)go.GetComponent("animationData");
            if (ad.SpriteRenderer.Frames > 1)
            {
                ad.TimeSinceLastFrameChange += dt;
                if (ad.TimeSinceLastFrameChange >= ad.AnimationSpeed)
                {
                    ad.TimeSinceLastFrameChange = 0;

                    if ((ad.SpriteRenderer.CurrentFrame == 0 && !ad.Flip) ||
                        (ad.SpriteRenderer.CurrentFrame == ad.SpriteRenderer.Frames - 1 && ad.Flip))
                    {
                        ad.Flip = !ad.Flip;
                    }

                    ad.SpriteRenderer.CurrentFrame += ad.Flip ? 1 : -1;
                }
            }
        }
        public static void ArrowControls(float dt, GameObject go, Component[] c = null)
        {
            KeyboardState state = Keyboard.GetState();
            Vector2 v = new Vector2();
            Movement m = (Movement)go.GetComponent("movement");
            if (InputManager.IsPressed(Keys.Up))
                v.Y = -m.Speed * dt;
            else if (InputManager.IsPressed(Keys.Down))
                v.Y = m.Speed * dt;
            if (InputManager.IsPressed(Keys.Left))
                v.X = -m.Speed * dt;
            else if (InputManager.IsPressed(Keys.Right))
                v.X = m.Speed * dt;

            ((Transform)go.GetComponent("transform")).Move(v);
        }

        public static void WASDcontrols(float dt, GameObject go, Component[] c = null)
        {
            Movement m = (Movement)go.GetComponent("movement");

            KeyboardState state = Keyboard.GetState();
            Vector2 v = new Vector2();
            float r = 0;
            if (state.IsKeyDown(Keys.W))
                v.Y = -(m.Speed * dt);
            else if (state.IsKeyDown(Keys.S))
                v.Y = (m.Speed * dt);
            if (state.IsKeyDown(Keys.A))
                v.X = -(m.Speed * dt);
            else if (state.IsKeyDown(Keys.D))
                v.X = (m.Speed * dt);

            if (state.IsKeyDown(Keys.Q))
                r = (1 * dt);
            else if (state.IsKeyDown(Keys.E))
                r = -(1 * dt);

            RigidBody rb = (RigidBody)go.GetComponent("rigidBody");

            rb.MoveVelocity = v;
            rb.AngularVelocity = r;
        }

        public static void ManualScale(float dt, GameObject go, Component[] c = null)
        {
            Transform t = (Transform)go.GetComponent("transform");
            if (InputManager.IsPressed(Microsoft.Xna.Framework.Input.Keys.Add) && t.Scale.X < 5)
            { t.SetScale(t.Scale.X + .1f, t.Scale.Y + .1f); }
            if (InputManager.IsPressed(Microsoft.Xna.Framework.Input.Keys.Subtract) && t.Scale.X > 0)
            { t.SetScale(t.Scale.X - .1f, t.Scale.Y - .1f); }

        }

        public static void ScreenShake(float dt, GameObject go, Component[] c = null)
        {
            Transform t = (Transform)go.GetComponent("transform");

            if (InputManager.IsTriggered(Keys.Space))
                CoroutineManager.Add(Coroutines.ScreenShake(.1f, -10, 10, t), "screenShake", 0, true);
        }

        public static void PointAtMouse(float dt, GameObject go, Component[] c = null)
        {
            Transform t = (Transform)go.GetComponent("transform");
            t.Radians = hf_Math.GetAngleDeg(InputManager.MousePos, t.Position) + 90 * (float)Math.PI / 180;
            if (t.Parent != null)
                t.Rotate(t.Radians - t.Parent.Radians);
        }

        public static void FaceTransform(float dt, GameObject go, Component[] c = null)
        {
            Transform t = (Transform)go.GetComponent("transform");
            Transform t2 = (Transform)c[0];
            RigidBody rb = (RigidBody)go.GetComponent("rigidBody");

            float newRot = (hf_Math.GetAngleDeg(t.Position - t2.Position, new Vector2(1, 0))) - 90 * (float)Math.PI / 180;
            rb.AngularVelocity = (newRot - t.Radians) / 1 * dt;
        }

        public static void MoveTowardRotation(float dt, GameObject go, Component[] c = null)
        {
            Transform t = (Transform)go.GetComponent("transform");
            RigidBody rb = (RigidBody)go.GetComponent("rigidBody");

            rb.MoveVelocity = hf_Math.RadToUnit(t.Radians + 90 * (float)Math.PI / 180) * dt * 100;
        }

        public static void ButtonSwapImagesOnHover(float dt, GameObject go, Component[] c = null)
        {
            Collider col = (Collider)go.GetComponent("myBox");
            ButtonData b = (ButtonData)go.GetComponent("buttonData");
            Vector2 v = InputManager.MousePos;

            if (col.ContainsPoint(v))
                ((WorldObject)b.GameObject).SpriteRenderer.Texture = b.SelectedTexID;
            else
                ((WorldObject)b.GameObject).SpriteRenderer.Texture = b.DeselectedTexID;
        }

        public static void QuitOnClick(float dt, GameObject go, Component[] c = null)
        {
            Collider col = (Collider)go.GetComponent("myBox");
            Vector2 v = InputManager.MousePos;
            if (InputManager.IsTriggered(InputManager.MouseKeys.Left) &&
                col.ContainsPoint(v))
                GameManager.Quit();
        }
    }
}
