using System;

namespace HashTable
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var table = new HashTable();

            table.Add("one");
            table.Add("one");

            Console.Read();
        }
    }
}
