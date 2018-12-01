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

            var client = new TcpClient("localhost", 12345);
            var reader = new StreamReader(client.GetStream());
            var writer = new StreamWriter(client.GetStream()) {AutoFlush = true};

            var query = "1 .";
            writer.WriteLine(query);
            
            var data = reader.ReadLine();
            Console.WriteLine($"Server: {data}");
            
            query = "2 file.jpg";
            writer.WriteLine(query);

            var size = reader.ReadLine();
            Console.WriteLine($"Server: {size}");
            /*var content = new byte[long.Parse(size)];
            reader.BaseStream.Read(content);
            File.WriteAllBytes("new.jpg", content);*/
            using (var file = File.OpenWrite("new.jpg"))
            {
                reader.BaseStream.CopyTo(file);
            }

            Console.Read();
        }
    }
}