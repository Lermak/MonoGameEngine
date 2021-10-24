using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace MonoGame_Core.Scripts
{
    public class SlimeScene : Scene
    {
        protected override void loadContent()
        {
            ResourceManager.AddSong("Melody", "Music/TestSong");
            ResourceManager.AddSoundEffect("TestHit", "Sound/TestHit");

            ResourceManager.AddTexture("SlimeSpriteSheet", "Slime");
        }

        protected override void loadObjects()
        {
            WorldObject slime = InitWorldObject(new WorldObject("SlimeSpriteSheet", "Slime", new string[] { }, new Vector2(48, 48), new Vector2(), 0));
            slime.ComponentHandler.Add(new AnimationData(slime, .125f));
            slime.BehaviorHandler.Add("Animate", Behaviors.Animate, new Component[] { slime.ComponentHandler.Get("animationData") });

        }
    }
}
