using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Core.Scripts
{
    /// <summary>
    /// A class used to store publicly accessable variables for the entire application
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// Width of the target BackBuffer
        /// </summary>
        public const float SCREEN_WIDTH = 1920;
        /// <summary>
        /// Heigh of the target BackBuffer
        /// </summary>
        public const float SCREEN_HEIGHT = 1080;
        public static float SCREEN_HYPOTENUSE { get { return hf_Math.Hypot(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2); } }

        public const float TILE_WIDTH = 48;
        public const float TILE_HEIGHT = 48;
    }
}
