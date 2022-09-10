using System;
using System.Threading.Tasks;

namespace Chatbot
{
    class Program
    {
        // Starts main as an asycable task
        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();
        
        public async Task MainAsync()
        {
            await Task.Delay(-1); // This will keep it alive forever
        }
    }
}
