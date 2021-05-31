using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Core.Scripts
{
    /// <summary>
    /// Static management class to handle the collection of cameras in the game and their rendering
    /// </summary>
    public static class CameraManager
    {
        static List<Camera> cameras;
        public static List<Camera> Cameras { get { return cameras; } }

        public static void Initilize()
        {
            cameras = new List<Camera>();
            //MainCamera should always been the 0th element in the cameras list
            cameras.Add(new Camera("MainCamera", -1, 0, RenderingManager.WIDTH, RenderingManager.HEIGHT, new Vector2(RenderingManager.WIDTH, RenderingManager.HEIGHT), new Vector2(RenderingManager.WIDTH, RenderingManager.HEIGHT) * -1, new Vector2(RenderingManager.WIDTH, RenderingManager.HEIGHT) * 1));

        }

        public static void AddCamera(Camera c)
        {
            cameras.Add(c);
        }

        /// <summary>
        /// Render all cameras to the screen, except the MainCamera, which defaults to the BackBuffer
        /// </summary>
        /// <param name="sb">The current spriteBatch</param>
        public static void Draw(SpriteBatch sb)
        {
            //Cameras are unique compaired to other game objects in that they don't require a spriteRenderer component
            //They manage thier own drawing
            for(int i = 0; i < cameras.Count; ++i)
            {
                if(cameras[i].Target != -1)
                    cameras[i].Draw(sb);
            }
        }

        /// <summary>
        /// Perform any behaviors that the cameras have attached
        /// </summary>
        /// <param name="gt">Game Time</param>
        public static void Update(float gt)
        {
            //Cameras are GameObjects and need to be updated like them
            foreach (Camera c in cameras)
            {
                c.Update(gt);
            }
        }
    }
}
