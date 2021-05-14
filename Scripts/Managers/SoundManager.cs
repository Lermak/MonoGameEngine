using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace MonoGame_Core.Scripts
{
    /// <summary>
    /// Manages the different sound and song channels
    /// </summary>
    public static class SoundManager
    {
        /// <summary>
        /// List of currently available songs
        /// </summary>
        public static Dictionary<string, Song> SongChannels = new Dictionary<string, Song>();
        /// <summary>
        /// List of currently available sound effects
        /// </summary>
        public static Dictionary<string, SoundEffectInstance> SoundEffectChannels = new Dictionary<string, SoundEffectInstance>();

        public static void Initilize()
        {

        }

        public static void Clear()
        {
            SongChannels.Clear();
            SoundEffectChannels.Clear();
        }

        public static void Update(float gt)
        {

        }
    }
}
