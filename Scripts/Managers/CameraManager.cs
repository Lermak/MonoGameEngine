using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Core.Scripts
{
    public static class CameraManager
    {
        static List<Camera> cameras;
        public static List<Camera> Cameras { get { return cameras; } }

        public static void Initilize()
        {
            cameras = new List<Camera>();
            cameras.Add(new Camera("MainCamera", -1, 0, new Transform(0, new Vector2(), RenderingManager.WIDTH, RenderingManager.HEIGHT, 0), new Vector2(RenderingManager.WIDTH, RenderingManager.HEIGHT), new Vector2(RenderingManager.WIDTH, RenderingManager.HEIGHT) * -1, new Vector2(RenderingManager.WIDTH, RenderingManager.HEIGHT) * 1));
        }

        public static void AddCamera(Camera c)
        {
            cameras.Add(c);
        }

        public static void Draw(SpriteBatch sb)
        {
            for(int i = 0; i < cameras.Count; ++i)
            {
                if(cameras[i].Target != -1)
                    cameras[i].Draw(sb);
            }
        }

        public static void Update(float gt)
        {
            foreach (Camera c in cameras)
            {
                c.Update(gt);
            }
        }
    }
}
