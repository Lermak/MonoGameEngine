using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public class ManuallyScale : Behavior
    {
        Transform myTransform;

        public ManuallyScale(int uo, string name, Transform t) : base(uo, name)
        {
            myTransform = t;
        }

        public override void Update(float gt)
        {
            if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Add) && myTransform.Scale.X < 5)
            { myTransform.SetScale(myTransform.Scale.X + .1f, myTransform.Scale.Y + .1f); }
            if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Subtract) && myTransform.Scale.X > 0)
            { myTransform.SetScale(myTransform.Scale.X - .1f, myTransform.Scale.Y - .1f); }

        }
    }
}
