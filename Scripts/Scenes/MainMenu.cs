using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;


namespace MonoGame_Core.Scripts
{
    public class MainMenu : Scene
    {
        protected override void loadContent()
        {      
            ResourceManager.AddTexture("Test", "Images/Test");
            ResourceManager.AddTexture("Base", "Images/Base");
        }

        protected override void loadObjects()
        {
            gameObjects = new List<GameObject>();
            //GameObjects.Add(new TestObject("Test", "testObj"));
            InitGameObject(new TestStaticObject("Test", new Vector2(100, 100), "Test1", 1));
            InitGameObject(new Button("Test", "Base", "PlayButton", new Vector2(40, 40), new Vector2(500, 100), 1, null));
            InitGameObject(new Button("Test", "Base", "QuitButton", new Vector2(40, 40), new Vector2(500, 40), 1, Behaviors.QuitOnClick));

        }

        public override void Update(float dt)
        {
            if (SceneManager.SceneState == SceneManager.State.Running)
            {
                KeyboardState state = Keyboard.GetState();
                if (state.GetPressedKeys().Length > 0)
                {
                    SceneManager.ChangeScene(new TestScene());
                }
            }

            base.Update(dt);
        }
    }
}
