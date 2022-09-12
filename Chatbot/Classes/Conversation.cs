using Discord;
using Discord.WebSocket;
using System.Text.RegularExpressions;

namespace Chatbot.Classes
{
    public static class Conversation
    {
        static Regex regex;
        static Match match;
        static string MentionPattern = "\\s+<@\\d+>\\s+";

        static Random random = new Random();

        async public static Task<Task> Input(DiscordSocketClient client, SocketUserMessage msg)
        {
            // Read input and respond accordingly
            var sender = msg.Author;
            var channel = msg.Channel;
            var message = msg.Content;

            /**
             * Because we use mentioning the bot to trigger the conversation, we need to strip out the mention
             * An incoming message will look something like:
             * <@429746839944822794> Hello
             * Where everything between < and > is the unique Discord userID of the bot
             * The regex pattern includes spaces before and after the mention, to handle the mention being the first
             * and the last thing in the message, just in case, even though the main code only checks for it at begining.
             */
            regex = new Regex(@MentionPattern);
            match = regex.Match(message);

            if (match.Success)
            {
                message = message.Substring(match.Length);
            }

            // At this point, message is just the text, without the userID
            // The question/Google thing was first, but I'm not entirely sure how it works so skipping for now
            if (message.ToLower().Contains("how are you"))
            {
                var FeelingList = new List<string> { "Happy.", "Sad.", "Excited.", "Creative.", "Rather good about myself today." };
                var Feeling = FeelingList[random.Next(FeelingList.Count + 1)];
                await msg.ReplyAsync($"{Feeling}");
            }


            return Task.CompletedTask;
        }
    }
}
