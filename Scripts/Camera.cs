using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_Core.Scripts
{
    public static class Camera
    {
        static float CameraSpeed = 200;

        static Vector2 position;
        public static Vector2 Position { get { return position * RenderingManager.WindowScale; } }
        public static Vector2 MinPos;
        public static Vector2 MaxPos;

        public static void Initilize()
        {
            position = new Vector2(0, 0);
            MinPos = new Vector2(0, 0);
            MaxPos = SceneManager.CurrentScene.Size;
        }

        public static void Update(float gt)
        {
            MoveWithArrowKeys(gt);
        }

        private static void MoveWithArrowKeys(float gt)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Up))
                position.Y -= (float)(CameraSpeed * gt);
            else if (state.IsKeyDown(Keys.Down))
                position.Y += (float)(CameraSpeed * gt);
            if (state.IsKeyDown(Keys.Right))
                position.X += (float)(CameraSpeed * gt);
            else if (state.IsKeyDown(Keys.Left))
                position.X -= (float)(CameraSpeed * gt);

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
