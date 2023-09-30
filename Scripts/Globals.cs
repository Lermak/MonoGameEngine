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
        public const string GAME_TITLE = "TEST TITLE";
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

        public static List<GameObject> GalaxyMap = null;

        public static CameraManager CameraManager {get{ return SceneManager.CurrentScene.CameraManager;}}
        public static CoroutineManager CoroutineManager { get { return SceneManager.CurrentScene.CoroutineManager; } }
        public static CollisionManager CollisionManager { get { return SceneManager.CurrentScene.CollisionManager; } }
        public static ResourceManager ResourceManager { get { return SceneManager.CurrentScene.ResourceManager; } }
        public static RenderingManager RenderingManager { get { return SceneManager.CurrentScene.RenderingManager; } }
        public static SoundManager SoundManager { get { return SceneManager.CurrentScene.SoundManager; } }

    }
}
