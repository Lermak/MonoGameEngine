using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_Core.Scripts
{
    public class Config
    {
        public float MasterVolume { get; set; }
        public float SongVolume { get; set; }
        public float SoundEffectVolume { get; set; }
        public Dictionary<string, Keys> Keybindings { get; set; }
        public bool TestBool { get; set; }

        public Config(float mv, float sv, float sev, Dictionary<string, Keys> k, bool tb)
        {
            MasterVolume = mv;
            SongVolume = sv;
            SoundEffectVolume = sev;
            Keybindings = k;
            TestBool = tb;
        }

        public Config()
        {

        }

        public static Config NewConfig()
        {
            float mv = .2f;
            Dictionary<string, Keys> k = new Dictionary<string, Keys>() {
                { "left", Keys.A },
                { "right", Keys.D },
                { "up", Keys.W },
                { "down", Keys.S },
                { "rot_left", Keys.Q },
                { "rot_right", Keys.E },
                { "zoom_in", Keys.Add },
                { "zoom_out", Keys.Subtract },
                { "space", Keys.Space }
            };
            bool tb = false;
            float sv = 1;
            float sev = 1;

            return new Config(mv, sv, sev, k, tb);
        }

    }

    public static class ConfigurationManager
    {

        public static Config Configuration;

        public static void Initilize()
        {
            LoadConfig();
            SaveConfig();
        }

        public static void LoadConfig()
        {
            string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string gameDir = "\\" + Globals.GAME_TITLE + "\\";
            appDataDir = appDataDir + gameDir;
            string settingsFileName = Path.Combine(appDataDir, "config.ini");
            if (File.Exists(settingsFileName))
            {
                using (FileStream f = File.OpenRead(settingsFileName))
                using(StreamReader fs = new StreamReader(f))
                {
                    try
                    {
                        string s = fs.ReadToEnd();
                        Configuration = JsonSerializer.Deserialize<Config>(s);
                    }
                    catch (Exception e)
                    {
                        Configuration = Config.NewConfig();
                    }
                }
            }
            else
            {
                Configuration = Config.NewConfig();
            }
        }
        
        public static void SaveConfig()
        {
            string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string gameDir = "\\" + Globals.GAME_TITLE + "\\";
            appDataDir = appDataDir + gameDir;
            Directory.CreateDirectory(appDataDir.Replace(":", ""));
            if (Directory.Exists(appDataDir))
            {
                try
                {
                    string settingsFileName = Path.Combine(appDataDir, "config.ini");
                    File.Create(settingsFileName).Close();
                    using (FileStream f = new FileStream(settingsFileName, FileMode.OpenOrCreate))
                    using (StreamWriter fw = new StreamWriter(f))
                    {
                        string jsonString = JsonSerializer.Serialize(Configuration);
                        fw.Write(jsonString);
                    }
                }
                catch (Exception e)
                {

                }
            }
        }
    }
}
