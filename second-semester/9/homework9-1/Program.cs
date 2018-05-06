using System;
using System.Collections.Generic;

namespace Primes
{
    public class Program
    {
        public static IEnumerable<T> Filter<T>(IEnumerable<T> collection, Predicate<T> predicate)
        {
            foreach (var item in collection)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        public static bool IsPrime(int number)
        {
            var i = 2;
            while (i * i <= number)
            {
                if (number % i == 0)
                {
                    return false;
                }

                i++;
            }

            return number > 1;
        }

        public static IEnumerable<int> Integers()
        {
            var current = 1;

            while (true)
            {
                yield return current;
                current++;
            }
        }

        public static IEnumerable<int> Primes()
        {
            return Filter(Integers(), IsPrime);
        }

        public static void Main(string[] args)
        {
            foreach (var prime in Primes())
            {
                if (prime > 1000)
                {
                    break;
                }

                Console.WriteLine(prime);
            }
        }
    }
}
