using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleFtp
{
    public class FtpServer : IDisposable
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly int _port;
        private readonly int _maxConnectionNumber;
        private int _currentConnectionNumber;

        public FtpServer(int port, int maxConnectionNumber)
        {
            if (port < IPEndPoint.MinPort || port > IPEndPoint.MaxPort)
            {
                throw new ArgumentOutOfRangeException(nameof(port), "Port number should be from 0 to 65535");
            }
            
            _port = port;
            _maxConnectionNumber = maxConnectionNumber;
        }

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
                    if (Interlocked.CompareExchange(ref _currentConnectionNumber, 0, 0) == _maxConnectionNumber)
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

        private async Task Interact(TcpClient client)
        {
            using (var reader = new StreamReader(client.GetStream()))
            using (var writer = new StreamWriter(client.GetStream()) {AutoFlush = true})
            {
                var query = await reader.ReadLineAsync();
                while (query != "dc" && !_cancellationTokenSource.IsCancellationRequested)
                {
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
                            await writer.WriteAsync("Command is not found.");
                            break;
                    }

                    query = await reader.ReadLineAsync();
                }
            }

            client.Close();
            Interlocked.Decrement(ref _currentConnectionNumber);
            Console.WriteLine("Client disconnected.");
        }
        
        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }

        public void Dispose()
        {
            Stop();
        }

        private string ListCommandResult(string path)
        {
            try
            {
                var files = Directory.GetFiles(path);
                var dirictories = Directory.GetDirectories(path);

                return files.Length + dirictories.Length + " "
                       + string.Join("", files.Select(name => $"'{name}' false "))
                       + string.Join("", dirictories.Select(name => $"'{name}' true "));
            }
            catch (Exception)
            {
                return "-1";
            }
        }

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
        
        private (string, string) ParseQuery(string query)
        {
            var tokens = query.Split(' ');
            return tokens.Length != 2
                ? (null, null)
                : (tokens[0], tokens[1]);
        }
    }
}