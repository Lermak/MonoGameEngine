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
        public enum MouseKeys { LeftButton, RightButton, MiddleButton }
        
        static KeyboardState currentKeyboardState;

        public static Vector2 MousePos { get { return mousePos; } }

        static KeyboardState previousKeyboardState;

        static MouseState currentMouseState;
        static MouseState previousMouseState;

        public static bool IsKeyPressed(Keys k)
        {
            return currentKeyboardState.IsKeyDown(k);
        }

        /// <summary>
        /// A key is triggered if it was pressed this loop
        /// </summary>
        /// <param name="k">The key to check</param>
        /// <returns>true if the key was pressed this loop</returns>
        public static bool IsKeyTriggered(Keys k)
        {
            return currentKeyboardState.IsKeyDown(k) && !previousKeyboardState.IsKeyDown(k);
        }

        public static bool IsMouseDown(MouseKeys b)
        {
            if (b == MouseKeys.LeftButton)
            {
                return currentMouseState.LeftButton == ButtonState.Pressed;
            }
            else if (b == MouseKeys.RightButton)
            {
                return currentMouseState.RightButton == ButtonState.Pressed;
            }
            else if (b == MouseKeys.MiddleButton)
            {
                return currentMouseState.MiddleButton == ButtonState.Pressed;
            }

            return false;
        }
        /// <summary>
        /// A mouse button is triggered if it was pressed this loop
        /// </summary>
        /// <param name="k">The mouse button to check</param>
        /// <returns>true if the mouse button was pressed this loop</returns>
        public static bool IsMouseTriggered(MouseKeys b)
        {
            if (b == MouseKeys.LeftButton)
            {
                return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton != ButtonState.Pressed;
            }
            else if (b == MouseKeys.RightButton)
            {
                return currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton != ButtonState.Pressed;
            }
            else if (b == MouseKeys.MiddleButton)
            {
                return currentMouseState.MiddleButton == ButtonState.Pressed && previousMouseState.MiddleButton != ButtonState.Pressed;
            }

            return false;
        }

        public static void Initilize()
        {
            currentKeyboardState = new KeyboardState();
            currentMouseState = new MouseState();
        }

        /// <summary>
        /// Check if a double click has occured, and change the double click flag to true if it has.
        /// </summary>
        /// <param name="gt">Game Time</param>
        private static void checkDoubleClick(float gt)
        {
            if (IsDoubleClick)
                IsDoubleClick = false;

            if (!firstClick)
            {
                if (IsMouseTriggered(MouseKeys.LeftButton))
                {
                    firstClick = true;
                }
            }
            else
            {
                if (timeSinceLastLeftClick < DOUBLE_CLICK_DELAY)
                {
                    timeSinceLastLeftClick += gt;
                    if (IsMouseTriggered(MouseKeys.LeftButton))
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
        /// <param name="gt">Game Time</param>
        public static void Update(float gt)
        {
            Point p = Mouse.GetState().Position;
            mousePos = new Vector2(p.X, -p.Y) / RenderingManager.WindowScale - new Vector2(RenderingManager.WIDTH / 2, -RenderingManager.HEIGHT / 2);

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            checkDoubleClick(gt);
        }
    }
}
