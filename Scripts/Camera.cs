using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

<<<<<<< HEAD
namespace MonoGame_Core.Scripts
=======
namespace GEJam.Scripts
>>>>>>> c1b8f6f68bc0e41355e957b11df0ccaba139105d
{
    public static class Camera
    {
        static float CameraSpeed = 100;

        static Vector2 position;
        public static Vector2 Position { get { return position * RenderingManager.Scale; } }
        public static Vector2 MinPos;
        public static Vector2 MaxPos;

        public static void Initilize()
        {
            position = new Vector2(0, 0);
            MinPos = new Vector2(float.MinValue, float.MinValue);
            MaxPos = new Vector2(float.MaxValue, float.MaxValue);
        }

        public static void Update(GameTime gt)
        {
            MoveWithArrowKeys(gt);
        }

        private static void MoveWithArrowKeys(GameTime gt)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Up))
                position.Y -= (float)(CameraSpeed * gt.ElapsedGameTime.TotalSeconds);
            else if (state.IsKeyDown(Keys.Down))
                position.Y += (float)(CameraSpeed * gt.ElapsedGameTime.TotalSeconds);
            if (state.IsKeyDown(Keys.Right))
                position.X += (float)(CameraSpeed * gt.ElapsedGameTime.TotalSeconds);
            else if (state.IsKeyDown(Keys.Left))
                position.X -= (float)(CameraSpeed * gt.ElapsedGameTime.TotalSeconds);

            if (position.X > MaxPos.X)
                position.X = MaxPos.X;
            else if (position.X < MinPos.X)
                position.X = MinPos.X;

            if (position.Y > MaxPos.Y)
                position.Y = MaxPos.Y;
            else if (position.Y < MinPos.Y)
                position.Y = MinPos.Y;
        }
    }
}
