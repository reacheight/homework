using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SimpleFtp
{
    public class FtpClient : IDisposable
    {
        private readonly TcpClient _client;
        private readonly StreamWriter _writer;
        private readonly StreamReader _reader;

        public FtpClient(string hostname, int port)
        {
            if (port < IPEndPoint.MinPort || port > IPEndPoint.MaxPort)
            {
                //TODO add comment for exception
                throw new ArgumentOutOfRangeException();
            }

            _client = new TcpClient(hostname, port);
            _reader = new StreamReader(_client.GetStream());
            _writer = new StreamWriter(_client.GetStream()) { AutoFlush = true };
        }

        public async Task<string> ListCommand(string path)
        {
            await _writer.WriteLineAsync("1 " + path);
            return await _reader.ReadLineAsync();
        }

        public async Task<long> GetCommand(string path, string downloadPath)
        {
            await _writer.WriteLineAsync("2 " + path);
            var size = long.Parse(await _reader.ReadLineAsync());

            if (size != -1)
            {
                var content = new byte[size];
                _reader.BaseStream.Read(content);
                try
                {
                    File.WriteAllBytes(downloadPath, content);
                }
                catch (Exception)
                {
                    //TODO replace by proper custom exception
                    throw new Exception();
                }
            }

            return size;
        }

        public void Close()
        {
            _writer.WriteLine("dc");
            _client.Close();
        }

        public void Dispose()
        {
            Close();
        }
    }
}