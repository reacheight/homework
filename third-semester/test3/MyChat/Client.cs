using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace MyChat
{
    /// <summary>
    /// Class that represent chat client
    /// </summary>
    public class Client
    {
        private readonly ManualResetEvent _resetEvent = new ManualResetEvent(false);
        private readonly TcpClient _client;
        
        /// <summary>
        /// Initilise new instance of Client class
        /// </summary>
        /// <param name="hostname">server hostname</param>
        /// <param name="port">server port number</param>
        /// <exception cref="ArgumentOutOfRangeException">will throw if port number is out of range</exception>
        public Client(string hostname, int port)
        {
            if (port < IPEndPoint.MinPort || port > IPEndPoint.MaxPort)
            {
                throw new ArgumentOutOfRangeException(nameof(port), "Port number should be from 0 to 65535");
            }
            
            _client = new TcpClient(hostname, port);
        }

        /// <summary>
        /// Starts the client
        /// </summary>
        public void Start()
        {
            Console.WriteLine("You are connected. Start chat!");

            Read();
            Write();

            _resetEvent.WaitOne();

        }
        
        /// <summary>
        /// Reads incoming messages
        /// </summary>
        private void Read()
        {
            var reader = new StreamReader(_client.GetStream());
            Task.Run(async () =>
            {
                while (true)
                {
                    var message = await reader.ReadLineAsync();
                    if (message == "exit")
                    {
                        _resetEvent.Set();
                        Console.WriteLine("Server stopped the chat.");
                        break;
                    }

                    if (!string.IsNullOrWhiteSpace(message))
                    {
                        Console.WriteLine($"Server: {message}");
                        Console.Write("> ");
                    }
                }
            });
        }

        /// <summary>
        /// Writes messages to server
        /// </summary>
        private void Write()
        {
            var writer = new StreamWriter(_client.GetStream()) { AutoFlush = true };
            Task.Run(async () =>
            {
                while (true)
                {
                    Console.Write("> ");
                    var query = Console.ReadLine();
                    await writer.WriteLineAsync(query);
                    
                    if (query == "exit")
                    {
                        Console.WriteLine("You stopped the chat.");
                        _resetEvent.Set();
                        break;
                    }
                }
            });
        }
    }
}