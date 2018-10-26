using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FileSystemMD5
{
    public static class MyFileSystemMd5
    {
        public static string GetChecksum(string path)
        {
            var attr = File.GetAttributes(path);

            return attr.HasFlag(FileAttributes.Directory)
                ? DirectoryMd5(path)
                : FileMd5(path);
        }

        private static string FileMd5(string path)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(path))
                {
                    var md5Result = md5.ComputeHash(stream);
                    return BitConverter.ToString(md5Result);
                }
            }
        }

        private static string DirectoryMd5(string path)
        {
            var directoryName = GetDirectoryName(path);
            var files = Directory.GetFiles(path);
            Array.Sort(files);
            var directories = Directory.GetDirectories(path);
            Array.Sort(directories);

            var fileHash = string.Join("", files.Select(FileMd5));
            var directoryHash = string.Join("", directories.Select(DirectoryMd5));

            using (var md5 = MD5.Create())
            {
                var hash = directoryName + directoryHash + fileHash;
                var bytes = Encoding.UTF8.GetBytes(hash);
                var md5Result = md5.ComputeHash(bytes);
                
                return BitConverter.ToString(md5Result);
            }
        }

        private static string GetDirectoryName(string path)
        {
            var tokens = path.Split('/');
            var last = tokens.Last();
            
            return string.IsNullOrEmpty(last)
                ? tokens[tokens.Length - 2]
                : last;
        }
    }
}