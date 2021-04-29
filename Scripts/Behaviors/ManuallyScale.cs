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
            if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Z) && myTransform.Scale.X < 5)
            { myTransform.SetScale(myTransform.Scale.X + .5f, myTransform.Scale.Y + .5f); }
            if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.X) && myTransform.Scale.X > 0)
            { myTransform.SetScale(myTransform.Scale.X - .5f, myTransform.Scale.Y - .5f); }

        }
    }
}
