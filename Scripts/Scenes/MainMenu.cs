using System;
using System.Collections.Generic;
using System.Text;
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
            ResourceManager.AddSong("Melody", "Music/TestSong");
            SoundManager.PlaySong("Melody");

            ResourceManager.AddTexture("PlayBtn", "Images/Default UI/btn_play");
            ResourceManager.AddTexture("SelPlayBtn", "Images/Default UI/btn_play_sel");
            ResourceManager.AddTexture("ExitBtn", "Images/Default UI/btn_exit");
            ResourceManager.AddTexture("SelExitBtn", "Images/Default UI/btn_exit_sel");
            ResourceManager.AddTexture("SettingsBtn", "Images/Default UI/btn_settings");
            ResourceManager.AddTexture("SelSettingsBtn", "Images/Default UI/btn_settings_sel");
        }

        protected override void loadObjects()
        {
            gameObjects = new List<GameObject>();
            InitGameObject(new Button("PlayBtn", "SelPlayBtn", "PlayButton", new Vector2(500, 100), 1, null));
            InitGameObject(new Button("ExitBtn", "SelExitBtn", "QuitButton", new Vector2(500, -20), 1, Behaviors.QuitOnClick));
            InitGameObject(new Button("SettingsBtn", "SelSettingsBtn", "SettingsButton", new Vector2(500, 40), 1, null));
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
