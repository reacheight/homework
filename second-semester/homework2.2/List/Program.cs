using System;

namespace List
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var list = new List<int>();

            for (int i = 1; i <= 10; ++i)
            {
                list.Insert(i, i - 1);
            }

            Console.WriteLine(list.Size);

            while (!list.IsEmpty())
            {
                list.Erase(0);
                Console.WriteLine(list.Size);
            }
        }

    }
}
