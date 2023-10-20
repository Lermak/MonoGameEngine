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
    public class SoundManager
    {
        public static float MasterVolume;
        public static float SongVolume;
        public static float SoundEffectVolume;
        public string CurrentSong;
        public static float GlobalVolume;

        /// <summary>
        /// List of currently available sound effects
        /// </summary>
        public Dictionary<string, SoundEffectInstance> SoundEffects = new Dictionary<string, SoundEffectInstance>();

        public void Initilize()
        {
            MasterVolume = ConfigurationManager.Configuration.MasterVolume;
            SongVolume = ConfigurationManager.Configuration.SongVolume;
            SoundEffectVolume = ConfigurationManager.Configuration.SoundEffectVolume;
            GlobalVolume = 1;

            Clear();
        }

        public void Clear()
        {
            MediaPlayer.Stop();
            SoundEffects.Clear();
        }

        public void Update(float gt)
        {
            if(SceneManager.CurrentScene != null)
            {

            }
        }

        public static void SetGlobalVolume(float v)
        {
            if (v < 0) v = 0;
            if (v > 1) v = 1;
            GlobalVolume = v;
            SetSEVolume(SoundEffectVolume);
            SetSongVolume(SongVolume);
        }

        public static void SetMasterVolume(float v)
        {
            if (v < 0) v = 0;
            if (v > 1) v = 1;
            MasterVolume = v;
            SetSEVolume(SoundEffectVolume);
            SetSongVolume(SongVolume);
        }

        public static void SetSongVolume(float v)
        {
            if (v < 0) v = 0;
            if (v > 1) v = 1;
            SongVolume = v;
            MediaPlayer.Volume = SongVolume * MasterVolume * GlobalVolume;
        }

        public static void SetSEVolume(float v)
        {
            if (v < 0) v = 0;
            if (v > 1) v = 1;
            SoundEffectVolume = v;

            //foreach (SoundEffectInstance se in SoundEffects.Values)
            //{
            //    se.Volume = SoundEffectVolume * MasterVolume * GlobalVolume;
            //}
        }

        public void PlaySong(string name)
        {
            CurrentSong = name;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(SceneManager.CurrentScene.ResourceManager.Songs[name]);
        }

        public void PlaySoundEffect(string name)
        {
            if (!SoundEffects.ContainsKey(name) || SoundEffects[name].State != SoundState.Playing)
            {
                SoundEffectInstance se = SceneManager.CurrentScene.ResourceManager.SoundEffects[name].CreateInstance();
                se.Volume = MasterVolume * SoundEffectVolume * GlobalVolume;
                se.Play();
                SoundEffects[name] = se;
            }
        }
    }
}
