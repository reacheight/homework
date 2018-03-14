using System;
using System.Collections.Generic;

namespace ListFunctions
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var list = new List<int> { 1, 2, 3, 4 };
            var list1 = ListFunctions.Map<int, int>(list, x => x + 1);
            var list2 = ListFunctions.Filter(list, x => x % 2 == 0);
            var list3 = ListFunctions.Fold<int, int>(list, 1, (acc, next) => acc * next);

            foreach (var item in list1)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();

            foreach (var item in list2)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();

            Console.WriteLine(list3);
        }
    }
}
