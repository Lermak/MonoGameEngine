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
        public enum State { Running, Paused };
        public static Scene CurrentScene = new TestScene();
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
            }
            CurrentScene = s;
            s.Initilize(cm);
            s.OnLoad();
        }

        public static void Update(float gt)
        {
            CurrentScene.Update(gt);
        }
    }
}
