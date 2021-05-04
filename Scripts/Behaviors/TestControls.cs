using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_Core.Scripts
{
    class TestControls : Behavior
    {
        RigidBody rigidBody;
        float speed = 500;
        public TestControls(int uo, string name, RigidBody rb) : base(uo, name)
        {           
            rigidBody = rb;
        }
        public TestControls(int uo, RigidBody rb) : base(uo, "testControls")
        {
            rigidBody = rb;
        }

        public override void Update(float gt)
        {
            KeyboardState state = Keyboard.GetState();
            Vector2 v = new Vector2();
            float r = 0;
            if (state.IsKeyDown(Keys.W))
                v.Y = -(speed * gt);
            else if (state.IsKeyDown(Keys.S))
                v.Y = (speed * gt);
            if (state.IsKeyDown(Keys.A))
                v.X = -(speed * gt);
            else if (state.IsKeyDown(Keys.D))
                v.X = (speed * gt);

            if (state.IsKeyDown(Keys.Q))
                r = -(1 * gt);
            else if (state.IsKeyDown(Keys.E))
                r = (1 * gt);

            rigidBody.UpdateVelocity(v);
            rigidBody.UpdateRotationalVelocity(r);
            base.Update(gt);
        }
    }
}
