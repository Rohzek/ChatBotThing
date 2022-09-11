using Kujou_Karen_Bot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Classes
{
    public static class SettingsLoad
    {
        public static string file = "Settings.json", version = "0.0.1";

        public static Settings settings;

        public static void Load()
        {
            using (StreamReader reader = new StreamReader(file))
            {
                string json = reader.ReadToEnd();
                settings = JsonConvert.DeserializeObject<Settings>(json);
                reader.Close();
            }
        }

        public static void Create()
        {
            settings = new Settings();
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(file, json);
        }

        public static Settings GetSettings()
        {
            if (File.Exists(file))
            {
                Load();
            }
            else
            {
                Create();
            }

            return settings;
        }
    }
}
