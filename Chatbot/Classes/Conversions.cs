using Discord.WebSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Chatbot.Classes
{
    // Handles conversions from the Discord API objects into normal strings that can be printed for logging purposes
    public class Conversions
    {
        public static string ChannelConvert(SocketMessage msg)
        {
            return msg.Channel.ToString();
        }

        public static string MessageConvert(SocketMessage msg)
        {
            return msg.Content;
        }

        public static string NameConvert(SocketMessage msg)
        {
            return $"{msg.Author.Username}#{msg.Author.Discriminator}";
        }

        public static string TimeConvert(SocketMessage msg)
        {
            return "[" + msg.Timestamp.ToLocalTime().Hour + ":" + msg.Timestamp.ToLocalTime().Minute + ":" + msg.Timestamp.ToLocalTime().Second + "]";
        }

        /*
         *Makes JSON objects spread out and easy to read like example:
         *{
         *    "thing": "Thing",
         *    "another_thing": "Something Else"
         *}
         */
        public static string JsonConvertBeautiful(string json)
        {
            JToken parsedJson = JToken.Parse(json);
            var beautified = parsedJson.ToString(Formatting.Indented);

            return beautified;
        }

        /*
         *Makes JSON objects on a single line for sending through the internet like example:
         *{"thing":"Thing","another_thing":"Something Else"}
         */
        public static string JsonConvertMinified(string json)
        {
            JToken parsedJson = JToken.Parse(json);
            var minified = parsedJson.ToString(Formatting.None);

            return minified;
        }
    }
}
