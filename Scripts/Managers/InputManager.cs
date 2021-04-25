using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_Core.Scripts
{
    public static class InputManager
    {
        const float DOUBLE_CLICK_DELAY = 1f;
        static float timeSinceLastLeftClick = 0;
       
        static bool firstClick = false;
        public static bool IsDoubleClick = false;
        public enum MouseKeys { LeftButton, RightButton, MiddleButton }
        
        static KeyboardState currentKeyboardState;
        static KeyboardState previousKeyboardState;

        static MouseState currentMouseState;
        static MouseState previousMouseState;

        public static bool IsKeyPressed(Keys k)
        {
            return currentKeyboardState.IsKeyDown(k);
        }

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

        public static void Update(float gt)
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = new KeyboardState();

            previousMouseState = currentMouseState;
            currentMouseState = new MouseState();

            checkDoubleClick(gt);
        }
    }
}
