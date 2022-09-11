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
        public static Settings settings;

        DiscordSocketClient clientDiscord;
        public static CommandService commandsDiscord;
        IServiceProvider services;

        // Starts main as an asycable task
        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            // Get login stuff for Discord
            settings = SettingsLoad.GetSettings();

            // Setup Discord Bot
            clientDiscord = new DiscordSocketClient();
            commandsDiscord = new CommandService();

            // Limits our client and command listener services
            services = new ServiceCollection().AddSingleton(clientDiscord).AddSingleton(commandsDiscord).BuildServiceProvider();

            // Event subscriptions
            clientDiscord.Log += Log; // Basic logging
            clientDiscord.MessageReceived += MessageReceived; // Watch for messages to log
            clientDiscord.MessageReceived += HandleCommands; // Watch for commands

            // Login to the bot services
            await RegisterCommands();
            await clientDiscord.LoginAsync(TokenType.Bot, settings.DiscordToken);
            await clientDiscord.StartAsync();
            await clientDiscord.SetGameAsync(settings.DiscordGame);

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

        Task RegisterCommands()
        {
            commandsDiscord.AddModulesAsync(Assembly.GetEntryAssembly(), services);
            Console.WriteLine(commandsDiscord.Commands.ToString());

            return Task.CompletedTask;
        }

        // Here's where the magic should happen
        async Task HandleCommands(SocketMessage message)
        {
            var msg = message as SocketUserMessage;

            if (message is null || message.Author.IsBot)
            {
                return;
            }

            int argPos = 0;
            // If command starts with ! or with a mention
            if (msg.HasStringPrefix(settings.Prefix, ref argPos) || msg.HasMentionPrefix(clientDiscord.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(clientDiscord, msg);

                var result = await commandsDiscord.ExecuteAsync(context, argPos, services);

                if (!result.IsSuccess)
                {
                    await message.Channel.SendMessageAsync("Error. Reason: " + result.ErrorReason);
                }
            }
        }
    }
}