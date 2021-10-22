using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
namespace MonoGame_Core.Scripts
{
    /// <summary>
    /// Class that acts as a single reference point for delta time
    /// </summary>
    public static class TimeManager
    {
        private static float deltaTime;
        public static float DeltaTime { get { return deltaTime; } }

        public static void Initilize(GameTime dt)
        {
            deltaTime = 0;
        }

        public static void Update(GameTime dt)
        {
            deltaTime = (float)dt.ElapsedGameTime.TotalSeconds;
        }
    }
}
