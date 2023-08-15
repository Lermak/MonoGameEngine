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
            // load switch test textures
            ResourceManager.AddTexture("SwitchBaseDeselected", "Images/Default UI/switch_base");
            ResourceManager.AddTexture("SwitchBaseSelected", "Images/Default UI/switch_base_sel");
            ResourceManager.AddTexture("SwitchOn", "Images/Default UI/switch_on");
            ResourceManager.AddTexture("SwitchOff", "Images/Default UI/switch_off");
        }

        protected override void loadObjects()
        {
            gameObjects = new List<GameObject>();

            InitGameObject(new Button("PlayBtn", "SelPlayBtn", "PlayButton", new Vector2(500, 100), 1, Behaviors.LoadSceneOnClick));
            InitGameObject(new Button("ExitBtn", "SelExitBtn", "QuitButton", new Vector2(500, -20), 1, Behaviors.QuitOnClick));
            InitGameObject(new Button("SettingsBtn", "SelSettingsBtn", "SettingsButton", new Vector2(500, 40), 1, null));
            InitGameObject(new Switch("SwitchOn", "SwitchOff","NoNameSwitch", new Vector2(500, 160), 1, Behaviors.SwitchOnClick));
            
            // hybrid dead hover button + switch combo
            InitGameObject(new Button("SwitchBaseDeselected", "SwitchBaseSelected","ComplexSwitchBase", new Vector2(500, 220), 1, Behaviors.OnClickTemplate)); // for implementation purposes; you can also pass null here
            InitGameObject(new Switch("SwitchOn", "SwitchOff","ComplexSwitch", new Vector2(500, 220), 1, Behaviors.SwitchOnClick)); // TODO make this relative to switch base
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
