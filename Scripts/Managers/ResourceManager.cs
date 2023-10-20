using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace MonoGame_Core.Scripts
{
    public class ResourceManager
    {
        public ContentManager Content;

        public Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
        public Dictionary<string, Song> Songs = new Dictionary<string, Song>();
        public Dictionary<string, SoundEffect> SoundEffects = new Dictionary<string, SoundEffect>();
        public Dictionary<string, Effect> Effects = new Dictionary<string, Effect>();
        public Dictionary<string, SpriteFont> Fonts = new Dictionary<string, SpriteFont>();
        public void ContendManager()
        {
            Initilize();
        }
        public void Initilize()
        {
            Content = GameManager.Game.Content;
            Textures = new Dictionary<string, Texture2D>();
            Songs = new Dictionary<string, Song>();
            SoundEffects = new Dictionary<string, SoundEffect>();
            Effects = new Dictionary<string, Effect>();
            Fonts = new Dictionary<string, SpriteFont>();
        }

        public void AddTexture(string name, string location)
        {
            Textures[name] = Content.Load<Texture2D>(location);
        }
        public void AddFont(string name, string location)
        {
            Fonts[name] = Content.Load<SpriteFont>(location);
        }
        public void AddSong(string name, string location)
        {
            Songs[name] = Content.Load<Song>(location);
        }
        public void AddEffect(string name, string location)
        {
            Effects[name] = Content.Load<Effect>(location);
        }
        public void AddSoundEffect(string name, string location)
        {
            SoundEffects[name] = Content.Load<SoundEffect>(location);
        }
        public Vector2 GetTextureSize(string name)
        {
            if (Textures.ContainsKey(name))
                return new Vector2(Textures[name].Width, Textures[name].Height);
            else
                return new Vector2();
        }
    }
}
