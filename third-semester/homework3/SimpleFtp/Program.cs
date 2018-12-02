using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleFtp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var server = new FtpServer(12345, 5);
            server.Start();

            var client = new FtpClient("localhost", 12345);
            Console.WriteLine(client.ListCommand("files").Result);
            Console.WriteLine(client.GetCommand("files/file.png", "new.png").Result);
            client.Close();
        
            Console.ReadLine();
        }
    }
}