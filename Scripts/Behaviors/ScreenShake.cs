using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public class ScreenShake : Behavior
    {
        Transform transform;

        public ScreenShake(int uo, string name, Transform t) : base(uo, name)
        {
            transform = t;
        }

        public override void Update(float gt)
        {
            if (InputManager.IsKeyTriggered(Microsoft.Xna.Framework.Input.Keys.Space))
                CoroutineManager.AddCoroutine(Coroutines.ScreenShake(.1f, -10, 10, transform), "screenShake", 0, true);
        }
    }
}
