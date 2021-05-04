using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
namespace MonoGame_Core.Scripts
{
    public static class TimeManager
    {
        private static float deltaTime;
        public static float DeltaTime { get { return deltaTime; } }

        public static void Initilize(GameTime gt)
        {
            deltaTime = (float)gt.ElapsedGameTime.TotalSeconds;
        }

        public static void Update(GameTime gt)
        {
            deltaTime = (float)gt.ElapsedGameTime.TotalSeconds;
        }
    }
}
