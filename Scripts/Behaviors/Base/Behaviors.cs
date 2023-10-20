using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
        public static void Move(float dt, GameObject go, Component[] c = null)
        {
            KeyboardState state = Keyboard.GetState();
            Vector2 v = new Vector2();
            Movement m = (Movement)go.GetComponent("movement");
            if (state.IsKeyDown(InputManager.KeyMap["down"]))
                v.Y = -(m.Speed * dt);
            else if (state.IsKeyDown(InputManager.KeyMap["up"]))
                v.Y = (m.Speed * dt);
            if (state.IsKeyDown(InputManager.KeyMap["left"]))
                v.X = -(m.Speed * dt);
            else if (state.IsKeyDown(InputManager.KeyMap["right"]))
                v.X = (m.Speed * dt);

            ((Transform)go.GetComponent("transform")).Move(v);
        }

        public static void MoveWithRot(float dt, GameObject go, Component[] c = null)
        {
            Movement m = (Movement)go.GetComponent("movement");

            KeyboardState state = Keyboard.GetState();
            Vector2 v = new Vector2();
            float r = 0;
            if (state.IsKeyDown(InputManager.KeyMap["down"]))
                v.Y = -(m.Speed * dt);
            else if (state.IsKeyDown(InputManager.KeyMap["up"]))
                v.Y = (m.Speed * dt);
            if (state.IsKeyDown(InputManager.KeyMap["left"]))
                v.X = -(m.Speed * dt);
            else if (state.IsKeyDown(InputManager.KeyMap["right"]))
                v.X = (m.Speed * dt);

            if (state.IsKeyDown(InputManager.KeyMap["rot_left"]))
                r = (m.RotSpeed * dt);
            else if (state.IsKeyDown(InputManager.KeyMap["rot_right"]))
                r = -(m.RotSpeed * dt);

            RigidBody rb = (RigidBody)go.GetComponent("rigidBody");

            rb.MoveVelocity = v;
            rb.AngularVelocity = r;

            //((Transform)go.GetComponent("transform")).Move(v);
            //((Transform)go.GetComponent("transform")).Rotate(r);
        }
        /// <summary>
        /// moves a gameobject forwards and backwards relative 
        /// to its rotation, and rotates rather than moving left and right
        /// <br/><br/><b><h1>control scheme</h1></b>
        /// <br/>move forwards and backwards with keys [up, down]
        /// <br/>strafe left and right with keys [rot_left,rot_right]
        /// <br/>rotate left and right with keys [left,right]
        /// <br/><br/>
        /// P.S. thank u stackex <br/>
        /// https://gamedev.stackexchange.com/questions/130684/move-object-forward-according-to-rotation
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="go"></param>
        /// <param name="c"></param>
        public static void DriveStrafe(float dt, GameObject go, Component[] c = null)
        {
            Movement m = (Movement)go.GetComponent("movement");
            Transform t = ((WorldObject)go).Transform;
            //
            // drive fw/bw
            KeyboardState state = Keyboard.GetState();
            Vector2 v = new Vector2();
            float r = 0;
            if (state.IsKeyDown(InputManager.KeyMap["down"]))
            {
                v.X = -(float)Math.Cos(t.Radians) * m.Speed * dt;
                v.Y = -(float)Math.Sin(t.Radians) * m.Speed * dt;
            }
            else if (state.IsKeyDown(InputManager.KeyMap["up"]))
            {
                v.X = (float)Math.Cos(t.Radians) * m.Speed * dt;
                v.Y = (float)Math.Sin(t.Radians) * m.Speed * dt;
            }
            //
            // strafe l/r
            if (state.IsKeyDown(InputManager.KeyMap["rot_right"]))
            {
                v = hf_Math.RadToUnit(t.Radians + hf_Math.DegToRad(-90)) * m.Speed * dt;
            }
            else if (state.IsKeyDown(InputManager.KeyMap["rot_left"]))
            {
                v = hf_Math.RadToUnit(t.Radians + hf_Math.DegToRad(90)) * m.Speed * dt;
            }
            //
            // rotate
            if (state.IsKeyDown(InputManager.KeyMap["left"]))
            {
                r = m.RotSpeed * dt;
            }
            else if (state.IsKeyDown(InputManager.KeyMap["right"]))
            {
                r = -(m.RotSpeed * dt);
            }

            RigidBody rb = (RigidBody)go.GetComponent("rigidBody");

            rb.MoveVelocity = v;
            rb.AngularVelocity = r;

        }

        public static void ManualScale(float dt, GameObject go, Component[] c = null)
        {
            Transform t = (Transform)go.GetComponent("transform");
            if (InputManager.IsPressed(InputManager.KeyMap["zoom_in"]) && t.Scale.X < 5)
            { t.SetScale(t.Scale.X + .1f, t.Scale.Y + .1f); }
            if (InputManager.IsPressed(InputManager.KeyMap["zoom_out"]) && t.Scale.X > 0)
            { t.SetScale(t.Scale.X - .1f, t.Scale.Y - .1f); }

        }

        public static void ShakeOnClick(float dt, GameObject go, Component[] c = null)
        {
            SpriteRenderer t = (SpriteRenderer)((WorldObject)go).SpriteRenderer;

            //if (InputManager.IsTriggered(InputManager.KeyMap["space"]))
            Collider col = (Collider)go.GetComponent("myBox");
            Vector2 v = InputManager.MousePos;
            if (InputManager.IsTriggered(InputManager.MouseKeys.Left) &&
                col.ContainsPoint(v))
                Globals.CoroutineManager.Add(Coroutines.Shake(.1f, -10, 10, t), "screenShake", 0, true);
        }

        public static void PointAtMouse(float dt, GameObject go, Component[] c = null)
        {
            Transform t = (Transform)go.GetComponent("transform");
            t.Radians = hf_Math.GetAngleRad(InputManager.MousePos, t.Position) + 90 * (float)Math.PI / 180;
            if (t.Parent != null)
                t.Rotate(t.Radians - t.Parent.Radians);
        }

        public static void FaceTransform(float dt, GameObject go, Component[] c = null)
        {
            Transform t = (Transform)go.GetComponent("transform");
            Transform t2 = (Transform)c[0];
            RigidBody rb = (RigidBody)go.GetComponent("rigidBody");

            float newRot = (hf_Math.GetAngleRad(t.Position - t2.Position, new Vector2(1, 0))) - 90 * (float)Math.PI / 180;
            rb.AngularVelocity = (newRot - t.Radians) / 1 * dt;
        }

        public static void MoveTowardRotation(float dt, GameObject go, Component[] c = null)
        {
            Transform t = (Transform)go.GetComponent("transform");
            RigidBody rb = (RigidBody)go.GetComponent("rigidBody");

            rb.MoveVelocity = hf_Math.RadToUnit(t.Radians + 90 * (float)Math.PI / 180) * dt * 100;
        }

        /// <summary>
        /// Checks if the mouse is over a button collision box and re-assigns ButtonData texture fields if so.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="go"></param>
        /// <param name="c"></param>
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
        /// <summary>
        /// highlights the base of a switch when the mouse hovers over it
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="gameObject"></param>
        /// <param name="c"></param>
        public static void SwitchSwapImagesOnHover(GameObject gameObject)
        {
            Collider collider = (Collider)gameObject.GetComponent("myBox");
            SwitchData switchData = (SwitchData)gameObject.GetComponent("switchData");
            Vector2 v = InputManager.MousePos;

            if (collider.ContainsPoint(v))
            {
                ((WorldObject)switchData.GameObject).SpriteRenderer.Texture = switchData.SwitchOnTexID;
            }
            else
            {
                ((WorldObject)switchData.GameObject).SpriteRenderer.Texture = switchData.SwitchOffTexID;
            }
        }

        public static void SwitchOnClick(float dt, GameObject gameObject, Component[] c = null)
        {
            Collider swapCollider = (Collider)gameObject.GetComponent("myBox");
            Vector2 mousePos = InputManager.MousePos;
            SwitchData switchData = (SwitchData)gameObject.GetComponent("swData");

            // update the state
            if (InputManager.IsTriggered(InputManager.MouseKeys.Left) &&
                swapCollider.ContainsPoint(mousePos))
            {
                switchData.SwitchOn = !switchData.SwitchOn;

                // update the switch image based on state
                if (switchData.SwitchOn)
                {
                    ((WorldObject)switchData.GameObject).SpriteRenderer.Texture = switchData.SwitchOnTexID;
                }
                else
                {
                    ((WorldObject)switchData.GameObject).SpriteRenderer.Texture = switchData.SwitchOffTexID;
                }
            }

        }

        public static void OnClickTemplate(float dt, GameObject go, Component[] c = null)
        {
            Collider col = (Collider)go.GetComponent("myBox");
            Vector2 v = InputManager.MousePos;
            if (InputManager.IsTriggered(InputManager.MouseKeys.Left) &&
                col.ContainsPoint(v))
            {
                // don't do anything, this is a dead button
            }
        }

        public static void QuitOnClick(float dt, GameObject go, Component[] c = null)
        {
            Collider col = (Collider)go.GetComponent("myBox");
            Vector2 v = InputManager.MousePos;
            if (InputManager.IsTriggered(InputManager.MouseKeys.Left) &&
                col.ContainsPoint(v))
                GameManager.Quit();
        }

        public static void LoadSceneOnClick(float dt, GameObject go, Component[] c = null)
        {
            Collider col = (Collider)go.GetComponent("myBox");
            Vector2 v = InputManager.MousePos;
            if (InputManager.IsTriggered(InputManager.MouseKeys.Left) &&
                col.ContainsPoint(v))
            {
                ShakeOnClick(dt, go, c);
                SceneManager.ChangeScene(new CombatScene());
            }
        }

        public static void reloadLevel(float dt, GameObject go, Component[] c = null)
        {
            if (InputManager.IsTriggered(Keys.R))
            {
                SceneManager.ChangeScene(new GalaxyMap());
            }
        }

        public static void DestroyOutOfBounds(float dt, GameObject go, Component[] c = null)
        {
            Transform t = ((WorldObject)go).Transform;

            if (t.Position.X > (Globals.SCREEN_WIDTH / 2)
                || t.Position.X < (-Globals.SCREEN_WIDTH / 2)
                || t.Position.Y > (Globals.SCREEN_HEIGHT / 2)
                || t.Position.Y < (-Globals.SCREEN_HEIGHT / 2))
            {
                go.Destroy();
            }
        }
    }
}
