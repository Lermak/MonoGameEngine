using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

<<<<<<< HEAD
namespace MonoGame_Core.Scripts
=======
namespace GEJam.Scripts
>>>>>>> c1b8f6f68bc0e41355e957b11df0ccaba139105d
{
    public static class Cursor
    {
        static bool wasLeftDown;
        static bool wasRightDown;
        public static bool IsTriggered(int btn)
        {
            if (btn == 1 && Mouse.GetState().LeftButton.HasFlag(ButtonState.Pressed) && wasLeftDown == false)
                return true;

            if (btn == 2 && Mouse.GetState().RightButton.HasFlag(ButtonState.Pressed) && wasRightDown == false)
                return true;

            return false;
        }
        public static void Initilize()
        {
            wasLeftDown = false;
            wasRightDown = false;
            //Mouse.SetCursor(MouseCursor.FromTexture2D(SceneManager.CursorImage,45,45));
        }

        public static void Update(GameTime gt)
        {
            if(IsTriggered(1))
            {
               
            }
            if(Mouse.GetState().LeftButton.HasFlag(ButtonState.Pressed))
            {
                wasLeftDown = true;
            }
            else
            {
                wasLeftDown = false;
            }

            if (Mouse.GetState().RightButton.HasFlag(ButtonState.Pressed))
            {
                wasRightDown = true;
            }
            else
            {
                wasRightDown = false;
            }
        }
    }
}
