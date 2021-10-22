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
            SoundManager.SongChannels["Melody"] = Content.Load<Song>("Music/TestSong");
            SoundManager.SoundEffectChannels["TestHit"] = Content.Load<SoundEffect>("Sound/TestHit").CreateInstance();

            ResourceManager.Textures = new Dictionary<string, Texture2D>();
            ResourceManager.Textures["SlimeSpriteSheet"] = Content.Load<Texture2D>("Slime");

            gameObjects = new List<GameObject>();
            gameObjects.Add(new WorldObject("SlimeSpriteSheet", "Slime", new string[] { }, new Vector2(48,48), new Vector2(), 0));
            base.loadContent();
        }
    }
}
