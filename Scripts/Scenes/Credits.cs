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
    public class Credits : Scene
    {
        protected override void loadContent()
        {
            ResourceManager.AddSong("Melody", "Music/TestSong");
            SoundManager.PlaySong("Melody");

            ResourceManager.AddTexture("BG", "Images/GalaxyMap/SpaceBG");


            ResourceManager.AddFont("BaseFont", "Fonts/TestFont");
        }
        protected override void loadObjects()
        {   
            InitWorldObject(new WorldObject("BG", "Background", new string[] { }, new Vector2(), 0));
            InitWorldObject(new ScrollingCredits("BG", "Credits", new Vector2()));
        }
        public override void SceneRunning(float dt)
        {
            if (InputManager.IsPressed(Keys.Space))
                SceneManager.ChangeScene(new MainMenu());
            base.SceneRunning(dt);
        }
    }
}
