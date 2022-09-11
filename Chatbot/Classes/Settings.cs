using Newtonsoft.Json;
using System;

namespace Kujou_Karen_Bot
{
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

        // Probably not in use
        [JsonProperty("Prefix")]
        public string Prefix { get; set; } = "!";
    }
}