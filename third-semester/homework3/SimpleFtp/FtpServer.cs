using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleFtp
{
    /// <summary>
    /// Class that implements simple FTP server
    /// </summary>
    public class FtpServer : IDisposable
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly int _port;
        private readonly int _maxConnectionNumber;
        private int _currentConnectionNumber;
        
        /// <summary>
        /// Gets number of connected clients
        /// </summary>
        public int CurrentConnectionNumber =>
            Interlocked.CompareExchange(ref _currentConnectionNumber, 0, 0);

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="FtpServer"/> class.
        /// </summary>
        /// <param name="port">server port number</param>
        /// <param name="maxConnectionNumber">number of max connected clients</param>
        /// <exception cref="ArgumentOutOfRangeException">will throw if port number is out of range</exception>
        public FtpServer(int port, int maxConnectionNumber)
        {
            if (port < IPEndPoint.MinPort || port > IPEndPoint.MaxPort)
            {
                throw new ArgumentOutOfRangeException(nameof(port), "Port number should be from 0 to 65535");
            }
            
            _port = port;
            _maxConnectionNumber = maxConnectionNumber;
        }

        /// <summary>
        /// Runs the server
        /// </summary>
        public async Task Start()
        {
            var listener = new TcpListener(IPAddress.Any, _port);
            listener.Start();
            
            Console.WriteLine($"Server started to listen to port {_port}...");

            try
            {
                while (!_cancellationTokenSource.IsCancellationRequested)
                {
                    var client = await listener.AcceptTcpClientAsync();
                    if (CurrentConnectionNumber == _maxConnectionNumber)
                    {
                        client.Close();
                        continue;
                    }

                    Interlocked.Increment(ref _currentConnectionNumber);
                    Console.WriteLine("New client connected.");

                    Task.Run(async () => await Interact(client));
                }
            }
            finally
            {
                listener.Stop();
            }
        }

        /// <summary>
        /// Interacts with the client
        /// </summary>
        /// <param name="client">client to interact with</param>
        private async Task Interact(TcpClient client)
        {
            using (var reader = new StreamReader(client.GetStream()))
            using (var writer = new StreamWriter(client.GetStream()) {AutoFlush = true})
            {
                while (!_cancellationTokenSource.IsCancellationRequested)
                {
                    var query = await reader.ReadLineAsync();
                    if (query == "dc")
                    {
                        break;
                    }
                    
                    var (command, path) = ParseQuery(query);
                    switch (command)
                    {
                        case "1":
                            await writer.WriteLineAsync(ListCommandResult(path));
                            break;

                        case "2":
                            var (size, stream) = GetCommandResult(path);

                            await writer.WriteLineAsync(size.ToString());
                            stream?.CopyTo(writer.BaseStream);
                            stream?.Close();
                            
                            break;
    
                        default:
                            await writer.WriteLineAsync("Command is not found.");
                            break;
                    }
                }
            }

            client.Close();
            Interlocked.Decrement(ref _currentConnectionNumber);
            Console.WriteLine("Client disconnected.");
        }
        
        /// <summary>
        /// Stops the server
        /// </summary>
        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Stop();
        }

        /// <summary>
        /// Gets result of the server list command:
        /// list of the files and dirictories from the given path
        /// </summary>
        /// <param name="path">given path</param>
        /// <returns>result of list command</returns>
        private string ListCommandResult(string path)
        {
            try
            {
                var di = new DirectoryInfo(path);
                var files = di.GetFiles();
                var dirictories = di.GetDirectories();

                return files.Length + dirictories.Length + " "
                       + string.Join("", files.Select(name => $"'{name.Name}' false "))
                       + string.Join("", dirictories.Select(name => $"'{name.Name}' true "));
            }
            catch (Exception)
            {
                return "-1";
            }
        }

        /// <summary>
        /// Gets result of the server get command:
        /// downloads file and returns it's size
        /// </summary>
        /// <param name="path">downloaded file</param>
        /// <returns>size of the file, -1 if there is no such file</returns>
        private (long, Stream) GetCommandResult(string path)
        {
            try
            {
                var contentStream = File.OpenRead(path);
                return (contentStream.Length, contentStream);
            }
            catch (Exception)
            {
                return (-1, null);
            }
        }
        
        /// <summary>
        /// Parses client query
        /// </summary>
        /// <param name="query">client query</param>
        /// <returns>parsed query</returns>
        private (string, string) ParseQuery(string query)
        {
            var tokens = query.Split(' ');
            return tokens.Length != 2
                ? (null, null)
                : (tokens[0], tokens[1]);
        }
    }
}