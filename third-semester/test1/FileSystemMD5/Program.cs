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
            var path = Console.ReadLine();
            
            Console.WriteLine(MyFileSystemMd5.GetChecksum(path));
        }
    }
}