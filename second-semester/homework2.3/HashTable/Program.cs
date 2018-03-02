using System;

namespace HashTable
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var integerTable = new HashTable<int>();

            for (var i = 1; i <= 100; ++i)
            {
                if (i % 2 == 1)
                {
                    integerTable.Add(i);
                }
            }

            for (var i = 1; i <= 100; ++i)
            {
                if (i % 4 == 0 && integerTable.Contains(i))
                {
                    integerTable.Erase(i);
                }
            }

            var stringTable = new HashTable<string>();

            for (var i = 1; i <= 100; ++i)
            {
                stringTable.Add("word" + i.ToString());
            }

            Console.WriteLine("tralala");
            Console.Read();
        }
    }
}
