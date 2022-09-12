using Chatbot.Classes;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Kujou_Karen_Bot;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Chatbot
{
    class Program
    {
        // Stores the login information of the Discord bot
        public static Settings settings;

        // Is the Discord backend information
        DiscordSocketClient client;
        public static CommandService commands; // Probably not going to be used
        IServiceProvider services;

        // Starts main as an asycable task
        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        // Actual definition of the main class
        public async Task MainAsync()
        {
            // Get login stuff for Discord
            settings = SettingsLoad.GetSettings();

            // Setup Discord Bot's backend
            client = new DiscordSocketClient();
            commands = new CommandService(); // Again, probably not used. But included just incase we decide to add backend commands

            // Limits our client and command listener services
            services = new ServiceCollection().AddSingleton(client).AddSingleton(commands).BuildServiceProvider();

            // Event subscriptions
            client.Log += Log; // Basic logging on all incoming alerts and messages
            client.MessageReceived += MessageReceived; // Watch for messages to log
            client.MessageReceived += HandleCommands; // Watch for actual commands

            // Registers any command files that extend ModuleBase<SocketCommandContext>
            await RegisterCommands();

            // Login to the API services
            await client.LoginAsync(TokenType.Bot, settings.DiscordToken);
            await client.StartAsync();
            await client.SetGameAsync(settings.DiscordGame); // Sets the game to be played. Can be any string

            await Task.Delay(-1); // This will keep it alive forever
        }

        Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        Task LogIncoming(string msg)
        {
            Console.WriteLine(msg);
            //Logger.Log(msg.ToString());
            return Task.CompletedTask;
        }

        async Task MessageReceived(SocketMessage message)
        {
            string output = "#" + Conversions.ChannelConvert(message) + " " + Conversions.NameConvert(message) + " " + Conversions.TimeConvert(message) + ": " + Conversions.MessageConvert(message);
            await LogIncoming(output);
        }

        /**
         * This uses Reflection to get any file in our namespace that extends ModuleBase<SocketCommandContext>
         * To register a command in the API automated command response
         */
        Task RegisterCommands()
        {
            commands.AddModulesAsync(Assembly.GetEntryAssembly(), services);
            Console.WriteLine(commands.Commands.ToString());

            return Task.CompletedTask;
        }

        // Here's where the magic should happen for the chat bot
        async Task HandleCommands(SocketMessage message)
        {
            var msg = message as SocketUserMessage;

            // Ignore phantom messages, and messages sent from another bot
            if (message is null || message.Author.IsBot)
            {
                return;
            }

            int argPos = 0;
            // If bot was mentioned
            if (/*msg.HasStringPrefix(settings.Prefix, ref argPos) ||*/ msg.HasMentionPrefix(client.CurrentUser, ref argPos))
            {
                // Below is the code to check for the automated command service
                /*
                var context = new SocketCommandContext(clientDiscord, msg);

                var result = await commandsDiscord.ExecuteAsync(context, argPos, services);

                 if (!result.IsSuccess)
                {
                await message.Channel.SendMessageAsync("Error. Reason: " + result.ErrorReason);
                }
                */

                // We want to respond to the user here
                await Conversation.Input(client, msg);
            }
        }
    }
}