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
            cameras.Add(new Camera("MainCamera", -1, 0, new Transform(0, new Vector2(), RenderingManager.WIDTH * 2, RenderingManager.HEIGHT * 2, 0), new Vector2(RenderingManager.WIDTH, RenderingManager.HEIGHT) * -1, new Vector2(RenderingManager.WIDTH, RenderingManager.HEIGHT) * 1));
            Cameras.Add(new Camera("CRTCamera", 0, 1, new Transform(0, new Vector2(), RenderingManager.WIDTH * 2, RenderingManager.HEIGHT * 2, 0), new Vector2(RenderingManager.WIDTH, RenderingManager.HEIGHT) * -1, new Vector2(RenderingManager.WIDTH, RenderingManager.HEIGHT) * 1));
            cameras[1].Transform.Resize(480, 270);
            cameras[1].ScreenPosition = new Vector2(480, 270)/2;
            cameras[1].BehaviorHandler.AddBehavior(new ManualCameraControl(0, cameras[0].Transform));            
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
