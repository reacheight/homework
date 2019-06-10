using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SimpleFtp
{
    /// <summary>
    /// Class that implement simple ftp client
    /// </summary>
    public class FtpClient : IDisposable
    {
        private readonly TcpClient _client;
        private readonly StreamWriter _writer;
        private readonly StreamReader _reader;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="FtpClient"/> class.
        /// </summary>
        /// <param name="hostname">server hostname</param>
        /// <param name="port">server port number</param>
        /// <exception cref="ArgumentOutOfRangeException">will throw if port number is out of range</exception>
        public FtpClient(string hostname, int port)
        {
            if (port < IPEndPoint.MinPort || port > IPEndPoint.MaxPort)
            {
                throw new ArgumentOutOfRangeException(nameof(port), "Port number should be from 0 to 65535");
            }

            _client = new TcpClient(hostname, port);
            _reader = new StreamReader(_client.GetStream());
            _writer = new StreamWriter(_client.GetStream()) { AutoFlush = true };
        }

        /// <summary>
        /// Executes list command:
        /// gets list of the files and dirictories from the given path
        /// </summary>
        /// <param name="path">given path</param>
        /// <returns>list command result</returns>
        public async Task<string> ListCommand(string path)
        {
            await _writer.WriteLineAsync("1 " + path);
            return await _reader.ReadLineAsync();
        }

        /// <summary>
        /// Executes get command:
        /// downloads file 
        /// </summary>
        /// <param name="path">path to the file to be downloaded</param>
        /// <param name="downloadPath">path where file will be downloaded</param>
        /// <returns>file size, -1 if there is no such file</returns>
        /// <exception cref="DownloadErrorException">will throw if download path is invalid</exception>
        public async Task<long> GetCommand(string path, string downloadPath)
        {
            await _writer.WriteLineAsync("2 " + path);
            var size = long.Parse(await _reader.ReadLineAsync());

            if (size == -1)
            {
                return size;
            }
            
            var content = new byte[size];
            await _reader.BaseStream.ReadAsync(content);
            try
            {
                await File.WriteAllBytesAsync(downloadPath, content);
            }
            catch (Exception e)
            {
                throw new DownloadErrorException("Error occurred while downloading file.", e);
            }

            return size;
        }

        /// <summary>
        /// Closes client connection
        /// </summary>
        public void Close()
        {
            _writer.WriteLine("dc");
            _client.Close();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Close();
        }
    }
}