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

            var Responses = new List<string>(); // Allocates memory for our responses list
            var Response = ""; // Allocates memory for our chosen response

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

            try
            {
                // Determine the proper response
                // The question/Google thing was first, but I'm not entirely sure how it works so skipping for now
                if (message.ToLower().Contains("how are you"))
                {
                    Responses = new List<string> { "happy.", "sad.", "excited.", "creative.", "rather good about myself today." };
                    Response = $"I'm feeling {Responses[random.Next(Responses.Count + 1)]}";
                }

                if (message.ToLower().Contains("hi") || message.ToLower().Contains("hello"))
                {
                    Responses = new List<string> { "Hello", "Howdy", "Hi", "Greetings", "Hi, let's talk.", "Hello there." };
                    Response = Responses[random.Next(Responses.Count + 1)];
                }

                if (message.ToLower().Contains("yes"))
                {
                    Responses = new List<string> { "Agreed.", "Indeed.", "Okay.", "I agree.", "Absolutely.", "Only if you're sure.", "Really?" };
                    Response = Responses[random.Next(Responses.Count + 1)];
                }

                if (message.ToLower().Contains("no"))
                {
                    Responses = new List<string> { "Alright then.", "Okay.", "No?", "Okay then.", "Are you sure about that?" };
                    Response = Responses[random.Next(Responses.Count + 1)];
                }

                if (message.ToLower().Contains("why not"))
                {
                    Responses = new List<string> { "Why not indeed.", "Maybe.", "Okay." };
                    Response = Responses[random.Next(Responses.Count + 1)];
                }

                if (message.ToLower().Contains("thank"))
                {
                    Responses = new List<string> { "No problem.", "You're Welcome.", "My Pleasure.", "Anytime.", "No trouble at all.", "Happy to help.", "Not a problem." };
                    Response = Responses[random.Next(Responses.Count + 1)];
                }

                if (message.ToLower().Contains("good"))
                {
                    Responses = new List<string> { "Agreed.", "Indeed.", "Okay.", "I agree.", "Absolutely.", "I'm glad." };
                    Response = Responses[random.Next(Responses.Count + 1)];
                }

                if (message.ToLower().Contains("nice"))
                {
                    Responses = new List<string> { "Agreed.", "Indeed.", "Okay.", "I agree.", "Absolutely.", "I thought so.", "Yes.", "It is indeed.", "It is, yes." };
                    Response = Responses[random.Next(Responses.Count + 1)];
                }

                // Actually respond
                await msg.ReplyAsync($"{Response}");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error caught in conversation: {ex.Message}");
                await msg.ReplyAsync($"Sorry, I spaced out for a second. Can you repeat that please?");
            }

            return Task.CompletedTask;
        }
    }
}
