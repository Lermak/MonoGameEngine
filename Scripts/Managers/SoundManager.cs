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
        public static float volume = 1;

        /// <summary>
        /// List of currently available sound effects
        /// </summary>
        public static Dictionary<string, SoundEffectInstance> SoundEffects = new Dictionary<string, SoundEffectInstance>();

        public static void Initilize()
        {

        }

        public static void Clear()
        {
            SoundEffects.Clear();
        }

        public static void Update(float gt)
        {
            MediaPlayer.Volume = volume;
            //SoundEffect.MasterVolume = volume;
        }

        public static void SetVolume(float v)
        {
            if (v < 0) v = 0;
            if (v > 1) v = 1;
            volume = v;
        }

        public static string CurrentSong;
        public static void PlaySong(string name)
        {
            ////Add logic to transition between songs
            //if(MediaPlayer.State == MediaState.Playing)
            //{
            //    MediaPlayer.Play(ResourceManager.Songs[name]);
            //}
            CurrentSong = name;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(ResourceManager.Songs[name]);
        }

        public static void PlaySoundEffect(string name)
        {
            if (!SoundEffects.ContainsKey(name) || SoundEffects[name].State != SoundState.Playing)
            {
                SoundEffectInstance se = ResourceManager.SoundEffects[name].CreateInstance();
                se.Play();
                SoundEffects[name] = se;
            }
        }
    }
}
