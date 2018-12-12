using System;
using System.Threading.Tasks;

namespace MyChat
{
    class Program
    {
        /// <summary>
        /// Parses given args
        /// </summary>
        /// <param name="args">given args</param>
        /// <returns>parsed args</returns>
        private static (int, string) ParseArgs(string[] args)
        {
            if (args.Length < 1)
            {
                return (-1, null);
            }
            
            var firstInt = int.TryParse(args[0], out var port);
            var hostname = string.Empty;

            if (args.Length == 2)
            {
                hostname = args[1];
            }

            return (firstInt ? port : -1, hostname);
        }
        
        /// <summary>
        /// Runs server or client
        /// </summary>
        /// <param name="args">given command line args</param>
        static async Task Main(string[] args)
        {
            var (port, hostname) = ParseArgs(args);

            if (port != -1 && !string.IsNullOrEmpty(hostname))
            {
                try
                {
                    var client = new Client(hostname, port);
                    client.Start();
                }
                catch (Exception)
                {
                    Console.WriteLine("Unable to find server.");
                }
            }
            else if (port != -1)
            {
                var server = new Server(port);
                await server.Start();
            }
            else
            {
                Console.WriteLine("Wrong arguments.");
            }
        }
    }
}