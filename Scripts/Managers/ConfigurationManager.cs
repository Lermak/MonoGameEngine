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
        public float Volume { get; set; }
        public Dictionary<string, Keys> Keybindings { get; set; }
        public bool TestBool { get; set; }

        public Config(float v, Dictionary<string, Keys> k, bool tb)
        {
            Volume = v;
            Keybindings = k;
            TestBool = tb;
        }

        public Config()
        {

        }

        public static Config NewConfig()
        {
            float v = .5f;
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

            return new Config(v, k, tb);
        }

    }

    public static class ConfigurationManager
    {

        public static Config Configuration;

        public static void Initilize()
        {
            LoadConfig();
        }

        public static void LoadConfig()
        {
            string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string gameDir = "\\" + Globals.GAME_TITLE + "\\";
            appDataDir = appDataDir + gameDir;
            string settingsFileName = Path.Combine(appDataDir, "Settings.ini");
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
            Directory.CreateDirectory(appDataDir);
            if (Directory.Exists(appDataDir))
            {
                try
                {
                    string settingsFileName = Path.Combine(appDataDir, "Settings.ini");
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
