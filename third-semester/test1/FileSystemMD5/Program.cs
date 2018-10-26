using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;

namespace FileSystemMD5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь до файла или дириктории:");
            var path = Console.ReadLine();
            
            Console.WriteLine("Checksum: ");
            Console.WriteLine(MyFileSystemMd5.GetChecksum(path));
        }
    }
}