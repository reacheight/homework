using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileSystemMD5;
using System.Security.Cryptography;
using System.Text;


namespace FileSystemMd5.Test
{
    [TestClass]
    public class FileSystemMd5Test
    {
        private const string DirectoryPath = "../../../files/test_dir";
        private const string TestFile = "../../../files/test_file.txt";

        [TestMethod]
        public void ChecksumIsCorrect()
        {
            var fileChecksum = MyFileSystemMd5.GetChecksum(TestFile);
            var directoryChecksum = MyFileSystemMd5.GetChecksum(DirectoryPath);

            var testFileChecksum = TestFileChecksum(TestFile);
            var testDirectoryChecksum = TestDirectoryChecksum(DirectoryPath);
            
            Assert.AreEqual(testFileChecksum, fileChecksum);
            Assert.AreEqual(testDirectoryChecksum, directoryChecksum);
        }

        private static string TestFileChecksum(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    var md5Result = md5.ComputeHash(stream);
                    return BitConverter.ToString(md5Result);
                }
            }
        }

        private static string TestDirectoryChecksum(string path)
        {
            var directoryName = "test_dir";

            var files = new[] { "file2.txt", "file3.txt", "test_file.txt" }
                .Select(file => "../../../files/test_dir/" + file).ToArray();
            var fileHash = TestFileChecksum(files[0]) + TestFileChecksum(files[1]) +
                           TestFileChecksum(files[2]);

            using (var md5 = MD5.Create())
            {
                var hash = directoryName + fileHash;
                var bytes = Encoding.UTF8.GetBytes(hash);
                var md5Result = md5.ComputeHash(bytes);
                
                return BitConverter.ToString(md5Result);
            }
        }
    }
}