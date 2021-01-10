using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


<<<<<<< HEAD
namespace MonoGame_Core.Scripts
=======
namespace GEJam.Scripts
>>>>>>> c1b8f6f68bc0e41355e957b11df0ccaba139105d
{
    public class WASDmovement : Behavior
    {
        float speed;
        Transform transform;
        public WASDmovement(int uo, Transform t, float s) : base(uo)
        {
            transform = t;
            speed = s;
        }

        public override void Update(GameTime gt)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.W))
                transform.Position.Y -= (float)(speed * gt.ElapsedGameTime.TotalSeconds);
            else if (state.IsKeyDown(Keys.S))
                transform.Position.Y += (float)(speed * gt.ElapsedGameTime.TotalSeconds);

            if (state.IsKeyDown(Keys.A))
                transform.Position.X -= (float)(speed * gt.ElapsedGameTime.TotalSeconds);
            else if (state.IsKeyDown(Keys.D))
                transform.Position.X += (float)(speed * gt.ElapsedGameTime.TotalSeconds);


            base.Update(gt);
        }
    }
}
