using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

<<<<<<< HEAD
namespace MonoGame_Core.Scripts
=======
namespace GEJam.Scripts
>>>>>>> c1b8f6f68bc0e41355e957b11df0ccaba139105d
{
    public static class SoundManager
    {
        public static Dictionary<string, Song> SongChannels = new Dictionary<string, Song>();
        public static Dictionary<string, SoundEffectInstance> SoundEffectChannels = new Dictionary<string, SoundEffectInstance>();

        public static void Initilize()
        {

        }

        public static void Update(GameTime gt)
        {
        }
    }
}
