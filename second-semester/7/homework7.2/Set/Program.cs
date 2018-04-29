using System;

namespace Set
{
    class Program
    {
        static void Main(string[] args)
        {
            var set = new Set<int>();

            set.Add(3);
            set.Add(7);
            set.Add(1);
            set.Add(4);
            set.Add(10);

            foreach (var item in set)
            {
                Console.WriteLine(item);
            }

            foreach (var item in set)
            {
                Console.WriteLine(item);
            }
        }
    }
}
