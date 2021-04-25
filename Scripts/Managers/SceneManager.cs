using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Core.Scripts
{
    static class SceneManager
    {
        public enum State { Running, Paused, SceneOut, SceneIn };
        public static Scene CurrentScene = new TestScene();
        private static Scene nextScene = null;
        public static State SceneState;
        static ContentManager cm;

        public static void Initilize(ContentManager c)
        {
            SceneState = State.Running;
            cm = c;
        }   

        public static void ChangeScene(Scene s)
        {
            if (CurrentScene != null)
            {
                CurrentScene.OnExit();
                SceneState = State.SceneOut;
            }
            else
            {
                SceneState = State.SceneIn;
            }
            nextScene = s;
        }

        public static void Update(float gt)
        {
            CurrentScene.Update(gt);
            
            if(SceneState == State.SceneIn)
            {
                RenderingManager.GlobalFade -= 128 * gt;
                if (RenderingManager.GlobalFade < 0)
                {
                    RenderingManager.GlobalFade = 0;
                    SceneState = State.Running;
                }
            }
            else if(SceneState == State.SceneOut)
            {
                RenderingManager.GlobalFade += 128 * gt;
                if(RenderingManager.GlobalFade > 255)
                {
                    RenderingManager.GlobalFade = 255;
                    SceneState = State.SceneIn;

                    CurrentScene = nextScene;
                    nextScene = null;
                    CurrentScene.Initilize(cm);
                    CurrentScene.OnLoad();
                }
            }
        }
    }
}
