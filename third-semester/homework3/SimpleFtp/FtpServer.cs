using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleFtp
{
    public class FtpServer
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly int _port;
        private readonly int _maxConnectionNumber;
        private int _currentConnectionNumber;

        public FtpServer(int port, int maxConnectionNumber)
        {
            if (port < IPEndPoint.MinPort || port > IPEndPoint.MaxPort)
            {
                //TODO add comment for exception
                throw new ArgumentOutOfRangeException();
            }
            
            _port = port;
            _maxConnectionNumber = maxConnectionNumber;
        }

        public async Task Start()
        {
            var listener = new TcpListener(IPAddress.Any, _port);
            listener.Start();
            
            Console.WriteLine($"Server started to listen to port {_port}...");

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
                            var (size, content) = GetCommandResult(path);

                            await writer.WriteLineAsync(size.ToString());
                            if (content != null)
                            {
                                using (var stream = new MemoryStream(content))
                                {
                                    stream.CopyTo(writer.BaseStream);
                                }
                            }

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

        private (long, byte[]) GetCommandResult(string path)
        {
            try
            {
                var content = File.ReadAllBytes(path);
                return (content.Length, content);
            }
            catch (Exception)
            {
                return (-1, null);
            }
        }
        
        private (string, string) ParseQuery(string query)
        {
            var tokens = query.Split(' ');
            if (tokens.Length != 2)
            {
                //TODO proper exception
                throw new Exception();
            }

            return(tokens[0], tokens[1]);
        }
    }
}