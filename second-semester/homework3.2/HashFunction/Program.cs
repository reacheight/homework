using System;

namespace HashTable
{
    public class Program
    {
        public static int IntHashFunction(int value) => value + 7;

        public static int StringHashFunction(string value) => value.Length;

        private static void Main(string[] args)
        {
            var table = new HashTable<char>();
            var intTable = new HashTable<int>(IntHashFunction);
            var stringTable = new HashTable<string>(StringHashFunction);

            for (var i = 0; i < 50; ++i)
            {
                intTable.Add(i);
                stringTable.Add(i.ToString());
                table.Add(i.ToString()[0]);
            }

            try
            {
                intTable.Erase(183);
            }
            catch (ValueNotInHashTableException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
