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
        public MainMenu() : base()
        {

        }

        protected override void loadContent()
        {
            ResourceManager.AddSong("Melody", "Music/TestSong");
            SoundManager.PlaySong("Melody");

            ResourceManager.AddTexture("BG", "Images/MainMenuBG");

            ResourceManager.AddTexture("Btn", "Images/VisitSystem/ButtonTemplate");
            ResourceManager.AddTexture("BtnHover", "Images/VisitSystem/ButtonTemplateHover");
        }

        protected override void loadObjects()
        {
            InitWorldObject(new WorldObject("BG", "Background", new string[] { }, new Vector2(), 0));

            Vector2 btnSize = ResourceManager.GetTextureSize("Btn");
            InitGameObject(new TextButton("PlayBtn", "Btn", "BtnHover", "Play", 5, new Vector2(Globals.SCREEN_WIDTH / 2 - btnSize.X / 2 - 20, 0), new Vector2(), 1, Behaviors.LoadSceneOnClick));
            TextButton exitBtn = (TextButton)InitGameObject(new TextButton("ExitBtn", "Btn", "BtnHover", "Exit", 5, new Vector2(Globals.SCREEN_WIDTH / 2 - btnSize.X / 2 / 2 - 20, -Globals.SCREEN_HEIGHT/2+btnSize.Y/2/2+20), new Vector2(), 1, Behaviors.QuitOnClick));
            exitBtn.Transform.SetScale(.5f,.5f);
        }
    }
}
