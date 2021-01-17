using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace MonoGame_Core.Scripts
{
    public static class SoundManager
    {
        public static Dictionary<string, Song> SongChannels = new Dictionary<string, Song>();
        public static Dictionary<string, SoundEffectInstance> SoundEffectChannels = new Dictionary<string, SoundEffectInstance>();

        public static void Initilize()
        {

        }

        public static void Update(float gt)
        {

        }
    }
}
