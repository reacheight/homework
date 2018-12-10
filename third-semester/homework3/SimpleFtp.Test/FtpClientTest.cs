using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;

namespace SimpleFtp.Test
{
    public class FtpClientTest
    {
        private const string Hostname = "localhost";
        private const int Port = 12345;
        private const int MaxConnectionNumber = 5;
        private FtpServer _server;

        private const string InvalidPath = "invalid_path/invlid_path";
        
        [OneTimeSetUp]
        public void SetUp()
        {
            _server = new FtpServer(Port, MaxConnectionNumber);
            _server.Start();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _server.Stop();
        }

        [Test]
        public async Task ListCommandShouldWorkOnValidPath()
        {
            using (var client = new FtpClient(Hostname, Port))
            {
                var result = await client.ListCommand("../../../files");
                result.ShouldBe("3 'file.png' false 'file.txt' false 'directory' true ");
            }
        }

        [Test]
        public async Task ListCommandShouldReturnMinusOneOnInvalidPath()
        {
            Directory.Exists(InvalidPath).ShouldBeFalse();
            using (var client = new FtpClient(Hostname, Port))
            {
                var result = await client.ListCommand(InvalidPath);
                result.ShouldBe("-1");
            }
        }

        [Test]
        public async Task GetCommandShouldDownloadFile()
        {
            const string from = "../../../files/file.png";
            const string to = "tmp.png";
            
            using (var client = new FtpClient(Hostname, Port))
            {
                var size = await client.GetCommand(from, to);
                
                size.ShouldNotBe(-1);
                ChecksumComparision(from, to).ShouldBeTrue();
            }
            
            File.Delete(to);
        }

        [Test]
        public async Task GetCommandShouldReturnSizeOfTheFile()
        {
            const string from = "../../../files/file.png";
            const string to = "tmp.png";
            
            using (var client = new FtpClient(Hostname, Port))
            {
                var size = await client.GetCommand(from, to);

                using (var fileStream = File.OpenRead(from))
                {
                    size.ShouldBe(fileStream.Length);
                }
            }
            
            File.Delete(to);
        }

        [Test]
        public async Task GetCommandShouldReturnMinusOneOnInvalidFromPath()
        {
            const string to = "tmp.png";
            
            File.Exists(InvalidPath).ShouldBeFalse();
            using (var client = new FtpClient(Hostname, Port))
            {
                var size = await client.GetCommand(InvalidPath, to);
                size.ShouldBe(-1);
            }
            
            File.Exists(to).ShouldBeFalse();
        }

        [Test]
        public void GetCommandShouldThrowExceptionOnInvalidToPath()
        {
            const string from = "../../../files/file.png";

            File.Exists(InvalidPath).ShouldBeFalse();
            using (var client = new FtpClient(Hostname, Port))
            {
                Should.Throw<DownloadErrorException>(async () => await client.GetCommand(from, InvalidPath));
            }
            
            File.Exists(InvalidPath).ShouldBeFalse();
        }

        private bool ChecksumComparision(string firstPath, string secondPath)
        {
            string firstPathSum;
            string secondPathSum;
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(firstPath))
                {
                    firstPathSum = BitConverter.ToString(md5.ComputeHash(stream));
                }
                
                using (var stream = File.OpenRead(secondPath))
                {
                    secondPathSum = BitConverter.ToString(md5.ComputeHash(stream));
                }
            }

            return firstPathSum == secondPathSum;
        }
    }
}