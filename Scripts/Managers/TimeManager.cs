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
        private static float gameSpeed;

        public static float GameSpeed { get { return gameSpeed; } set { gameSpeed = value; } }
        public static float DeltaTime { get { return deltaTime; } }
        public static float ProdDelta { get { return deltaTime * gameSpeed; } }

        public static void Initilize()
        {
            deltaTime = 0;
            gameSpeed = 1;
        }

        public static void Update(GameTime dt)
        {
            deltaTime = (float)dt.ElapsedGameTime.TotalSeconds;
        }
    }
}
