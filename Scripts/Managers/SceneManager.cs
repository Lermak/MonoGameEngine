﻿using System;
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
            CurrentScene.OnLoad();
        }   

        public static void ChangeScene(Scene s)
        {
            SceneState = State.SceneOut;
            CurrentScene.OnExit();
            NextScene = s;
        }

        public static void Update(float gt)
        {
            if(CurrentScene == null)
            {
                CurrentScene = NextScene;
                NextScene = null;
                CurrentScene.Initilize(cm);
                CurrentScene.OnLoad();
                SceneState = State.SceneIn;
            }
            else
                CurrentScene.Update(gt);         
        }

        public static void InitilizeCurrentScene()
        {
            CurrentScene.Initilize(cm);
        }
    }
}
