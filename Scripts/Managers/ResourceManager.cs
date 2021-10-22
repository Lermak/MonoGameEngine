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
        public static Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
        public static Dictionary<string, Song> Songs = new Dictionary<string, Song>();
        public static Dictionary<string, SoundEffect> SoundEffects = new Dictionary<string, SoundEffect>();
        public static Dictionary<string, Effect> Effects = new Dictionary<string, Effect>();
        public static Dictionary<string, SpriteFont> Fonts = new Dictionary<string, SpriteFont>();

        public static void Initilize()
        {
            Textures = new Dictionary<string, Texture2D>();
            Songs = new Dictionary<string, Song>();
            SoundEffects = new Dictionary<string, SoundEffect>();
            Effects = new Dictionary<string, Effect>();
            Fonts = new Dictionary<string, SpriteFont>();
        }
    }
}
