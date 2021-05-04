using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_Core.Scripts
{
    class ManualCameraControl : Behavior
    {
        Transform transform;
        float speed = 100;

        public ManualCameraControl(int uo, Transform t) : base(uo, "manualControlls")
        {
            transform = t;
        }

        public override void Update(float gt)
        {
            KeyboardState state = Keyboard.GetState();
            Vector2 v = new Vector2();
            float r = 0;
            if (InputManager.IsKeyPressed(Keys.Up))
                v.Y = -(speed * gt);
            else if (InputManager.IsKeyPressed(Keys.Down))
                v.Y = (speed * gt);
            if (InputManager.IsKeyPressed(Keys.Left))
                v.X = -(speed * gt);
            else if (InputManager.IsKeyPressed(Keys.Right))
                v.X = (speed * gt);

            transform.Move(v);
            base.Update(gt);
        }
    }
}
