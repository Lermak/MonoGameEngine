using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_Core.Scripts
{
    class PaddleControlls : Behavior
    {
        RigidBody rigidBody;
        float speed = 500;
        public PaddleControlls(int uo, string name, RigidBody rb) : base(uo, name)
        {
            rigidBody = rb;
        }
        public PaddleControlls(int uo, RigidBody rb) : base(uo, "testControls")
        {
            rigidBody = rb;
        }

        public override void Update(float gt)
        { 
            Vector2 v = new Vector2();

            if (InputManager.IsKeyPressed(Keys.W))
                v.Y = -(speed * gt);
            else if (InputManager.IsKeyPressed(Keys.S))
                v.Y = (speed * gt);


            rigidBody.MoveVelocity = v;
            base.Update(gt);
        }
    }
}
