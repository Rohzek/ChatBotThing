using Newtonsoft.Json;
using System;

namespace Kujou_Karen_Bot
{
    // An object that represents the settings to be saved and read out of the JSON file for bot login info
    [Serializable]
    public class Settings
    {
        [JsonProperty("DiscordName")]
        public string DiscordName { get; set; }
        [JsonProperty("DiscordNumber")]
        public string DiscordNumber { get; set; }
        [JsonProperty("DiscordAppID")]
        public string DiscordAppID { get; set; }
        [JsonProperty("DiscordPublicKey")]
        public string DiscordPublicKey { get; set; }
        [JsonProperty("DiscordToken")]
        public string DiscordToken { get; set; }
        [JsonProperty("DiscordGame")]
        public string DiscordGame { get; set; }
        [JsonProperty("Connection")]
        public string Connection { get; set; }

        // Probably not in use, this is for the automated commands services
        [JsonProperty("Prefix")]
        public string Prefix { get; set; } = "!";
    }
}