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
        public static Scene NextScene = null;
        public static State SceneState;
        static ContentManager cm;

        public static void Initilize(ContentManager c, Scene s)
        {
            SceneState = State.SceneIn;
            cm = c;
            CurrentScene = s;
            InitilizeCurrentScene();
        }   

        public static void ChangeScene(Scene s)
        {
            if (CurrentScene != null)
            {
                SceneState = State.SceneOut;
                NextScene = s;
            }
            else
            {
                SceneState = State.SceneIn;
                CurrentScene = s;
            }
        }

        public static void Update(float gt)
        {
            CurrentScene.Update(gt);         
        }

        public static void InitilizeCurrentScene()
        {
            CurrentScene.Initilize(cm);
        }
    }
}
