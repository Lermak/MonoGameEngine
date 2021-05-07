using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_Core.Scripts
{
    public static class MainCamera
    {
        static float CameraSpeed = 200;

        static Transform transform;

        public static Transform Transform { get { return transform; } }
        public static Vector2 Position { get { return transform.Position * RenderingManager.WindowScale; } }
        public static Vector2 MinPos { get { return new Vector2(-SceneManager.CurrentScene.Size.X / 2 + RenderingManager.WIDTH / 2, -SceneManager.CurrentScene.Size.Y / 2 + RenderingManager.HEIGHT / 2); } }
        public static Vector2 MaxPos { get { return new Vector2(SceneManager.CurrentScene.Size.X / 2 - RenderingManager.WIDTH / 2, SceneManager.CurrentScene.Size.Y / 2 - RenderingManager.HEIGHT / 2); } }

        public static void Initilize()
        {
            transform = new Transform(0, new Vector2(),0,0,0,0);
        }

        public static void Update(float gt)
        {
            MoveWithArrowKeys(gt);
        }

        private static void MoveWithArrowKeys(float gt)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Up))
                transform.Move(new Vector2(0, -(float)(CameraSpeed * gt)));
            else if (state.IsKeyDown(Keys.Down))
                transform.Move(new Vector2(0, (float)(CameraSpeed * gt)));
            if (state.IsKeyDown(Keys.Right))
                transform.Move(new Vector2((float)(CameraSpeed * gt), 0));
            else if (state.IsKeyDown(Keys.Left))
                transform.Move(new Vector2(-(float)(CameraSpeed * gt), 0));

            if (transform.Position.X > MaxPos.X)
                transform.Place(new Vector2(MaxPos.X, transform.Position.Y));
            else if (transform.Position.X < MinPos.X)
                transform.Place(new Vector2(MinPos.X, transform.Position.Y));

            if (transform.Position.Y > MaxPos.Y)
                transform.Place(new Vector2(transform.Position.X ,MaxPos.Y));
            else if (transform.Position.Y < MinPos.Y)
                transform.Place(new Vector2(transform.Position.X, MinPos.Y));
        }
    }
}
