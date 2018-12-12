using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace MyChat
{
    /// <summary>
    /// Class that represent chat server
    /// </summary>
    public class Server
    {
        private readonly ManualResetEvent _resetEvent = new ManualResetEvent(false);
        private readonly int _port;
        
        /// <summary>
        /// Initilise new instance of Server class
        /// </summary>
        /// <param name="port">server port number</param>
        /// <exception cref="ArgumentOutOfRangeException">will throw if port number is out of range</exception>
        public Server(int port)
        {
            if (port < IPEndPoint.MinPort || port > IPEndPoint.MaxPort)
            {
                throw new ArgumentOutOfRangeException(nameof(port), "Port number should be from 0 to 65535");
            }
            
            _port = port;
        }

        /// <summary>
        /// Starts server
        /// </summary>
        public async Task Start()
        {
            var listener = new TcpListener(IPAddress.Any, _port);
            listener.Start();
            
            Console.WriteLine($"Server started to listen to port {_port}...");

            using (var client = await listener.AcceptTcpClientAsync())
            {
                Console.WriteLine("Client connected. Start chat!");
                
                Read(client.GetStream());
                Write(client.GetStream());

                _resetEvent.WaitOne();
            }
        }

        /// <summary>
        /// Reads incoming messages
        /// </summary>
        /// <param name="stream">client network stream</param>
        private void Read(NetworkStream stream)
        {
            var reader = new StreamReader(stream);
            Task.Run(async () =>
            {
                while (true)
                {
                    var message = await reader.ReadLineAsync();
                    if (message == "exit")
                    {
                        _resetEvent.Set();
                        Console.WriteLine("Client stopped the chat.");
                        break;
                    }

                    if (!string.IsNullOrWhiteSpace(message))
                    {
                        Console.WriteLine($"Client: {message}");
                        Console.Write("> ");
                    }
                }
            });
        }

        /// <summary>
        /// Writes messages to client
        /// </summary>
        /// <param name="stream">client network stream</param>
        private void Write(NetworkStream stream)
        {
            var writer = new StreamWriter(stream) { AutoFlush = true };
            Task.Run(async () =>
            {
                while (true)
                {
                    Console.Write("> ");
                    var query = Console.ReadLine();
                    await writer.WriteLineAsync(query);
                    
                    if (query == "exit")
                    {
                        _resetEvent.Set();
                        Console.WriteLine("You stopped the chat.");
                        break;
                    }
                }
            });
        }
    }
}