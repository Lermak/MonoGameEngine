using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_Core.Scripts
{
    /// <summary>
    /// A wrapper class to help manage user input functions
    /// </summary>
    public static class InputManager
    {
        const float DOUBLE_CLICK_DELAY = 1f;
        static float timeSinceLastLeftClick = 0;
        static Vector2 mousePos;
        static bool firstClick = false;
        public static bool IsDoubleClick = false;
        public enum MouseKeys { Left, Right, Middle }
        
        static KeyboardState keyboardState;

        public static Vector2 MousePos { get { return mousePos; } }

        static KeyboardState prevKeyboardState;

        static MouseState mouseState;
        static MouseState prevMouseState;

        public static bool IsPressed(Keys k)
        {
            return keyboardState.IsKeyDown(k);
        }

        /// <summary>
        /// A key is triggered if it was pressed this loop
        /// </summary>
        /// <param name="k">The key to check</param>
        /// <returns>true if the key was pressed this loop</returns>
        public static bool IsTriggered(Keys k)
        {
            return keyboardState.IsKeyDown(k) && !prevKeyboardState.IsKeyDown(k);
        }

        public static bool IsPressed(MouseKeys b)
        {
            if (b == MouseKeys.Left)
            {
                return mouseState.LeftButton == ButtonState.Pressed;
            }
            else if (b == MouseKeys.Right)
            {
                return mouseState.RightButton == ButtonState.Pressed;
            }
            else if (b == MouseKeys.Middle)
            {
                return mouseState.MiddleButton == ButtonState.Pressed;
            }

            return false;
        }
        /// <summary>
        /// A mouse button is triggered if it was pressed this loop
        /// </summary>
        /// <param name="k">The mouse button to check</param>
        /// <returns>true if the mouse button was pressed this loop</returns>
        public static bool IsTriggered(MouseKeys b)
        {
            if (b == MouseKeys.Left)
            {
                return mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton != ButtonState.Pressed;
            }
            else if (b == MouseKeys.Right)
            {
                return mouseState.RightButton == ButtonState.Pressed && prevMouseState.RightButton != ButtonState.Pressed;
            }
            else if (b == MouseKeys.Middle)
            {
                return mouseState.MiddleButton == ButtonState.Pressed && prevMouseState.MiddleButton != ButtonState.Pressed;
            }

            return false;
        }

        public static void Initilize()
        {
            keyboardState = new KeyboardState();
            mouseState = new MouseState();
        }

        /// <summary>
        /// Check if a double click has occured, and change the double click flag to true if it has.
        /// </summary>
        /// <param name="dt">Game Time</param>
        private static void checkDoubleClick(float dt)
        {
            if (IsDoubleClick)
                IsDoubleClick = false;

            if (!firstClick)
            {
                if (IsTriggered(MouseKeys.Left))
                {
                    firstClick = true;
                }
            }
            else
            {
                if (timeSinceLastLeftClick < DOUBLE_CLICK_DELAY)
                {
                    timeSinceLastLeftClick += dt;
                    if (IsTriggered(MouseKeys.Left))
                    {
                        timeSinceLastLeftClick = 0;
                        firstClick = false;
                        IsDoubleClick = true;
                    }
                }
                else
                {
                    timeSinceLastLeftClick = 0;
                    firstClick = false;
                }
            }
        }

        /// <summary>
        /// Get the current state of they keyboard, and move the current state to the previous
        /// </summary>
        /// <param name="dt">Game Time</param>
        public static void Update(float dt)
        {
            Point p = Mouse.GetState().Position;
            mousePos = new Vector2(p.X, -p.Y) / RenderingManager.WindowScale - new Vector2(Globals.SCREEN_WIDTH / 2, -Globals.SCREEN_HEIGHT / 2);

            prevKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            checkDoubleClick(dt);
        }
    }
}
