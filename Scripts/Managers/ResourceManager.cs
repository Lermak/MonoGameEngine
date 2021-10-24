using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace MonoGame_Core.Scripts
{
    public static class ResourceManager
    {
        public static ContentManager Content;

        public static Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
        public static Dictionary<string, Song> Songs = new Dictionary<string, Song>();
        public static Dictionary<string, SoundEffect> SoundEffects = new Dictionary<string, SoundEffect>();
        public static Dictionary<string, Effect> Effects = new Dictionary<string, Effect>();
        public static Dictionary<string, SpriteFont> Fonts = new Dictionary<string, SpriteFont>();

        public static void Initilize(ContentManager c)
        {
            Content = c;
            Textures = new Dictionary<string, Texture2D>();
            Songs = new Dictionary<string, Song>();
            SoundEffects = new Dictionary<string, SoundEffect>();
            Effects = new Dictionary<string, Effect>();
            Fonts = new Dictionary<string, SpriteFont>();
        }

        public static void AddTexture(string name, string location)
        {
            Textures[name] = Content.Load<Texture2D>(location);
        }
        public static void AddFont(string name, string location)
        {
            Fonts[name] = Content.Load<SpriteFont>(location);
        }
        public static void AddSong(string name, string location)
        {
            Songs[name] = Content.Load<Song>(location);
        }
        public static void AddEffect(string name, string location)
        {
            Effects[name] = Content.Load<Effect>(location);
        }
        public static void AddSoundEffect(string name, string location)
        {
            SoundEffects[name] = Content.Load<SoundEffect>(location);
        }
        public static Vector2 GetTextureSize(string name)
        {
            if (Textures.ContainsKey(name))
                return new Vector2(Textures[name].Width, Textures[name].Height);
            else
                return new Vector2();
        }
    }
}
