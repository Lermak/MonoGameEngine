using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

<<<<<<< HEAD
namespace MonoGame_Core.Scripts
=======
namespace GEJam.Scripts
>>>>>>> c1b8f6f68bc0e41355e957b11df0ccaba139105d
{
    static class SceneManager
    {
        public enum State { Running, Paused };
        public static Scene CurrentScene = new Scene();
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

        public static void Update(GameTime gt)
        {
            CurrentScene.Update(gt);
        }
    }
}
