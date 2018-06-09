using System;
using System.Collections.Generic;

namespace Primes
{
    /// <summary>
    /// Main class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Returns a new enumerable collection composed of elements of given collection
        /// for which the value of given predicate is true
        /// </summary>
        /// <typeparam name="T">type of given collection's elements</typeparam>
        /// <param name="collection">given collection</param>
        /// <param name="predicate">given predicate</param>
        /// <returns>collection composed of elements of given list for which the value of given predicate is true</returns>
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

        /// <summary>
        /// Checks whether integer is prime
        /// </summary>
        /// <param name="number">integer to be checked</param>
        /// <returns>true if given integer is prime, false otherwise</returns>
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

        /// <summary>
        /// Implements infinite sequence of integers
        /// </summary>
        /// <returns>IEnumerable infinite sequence of integers</returns>
        public static IEnumerable<int> Integers()
        {
            var current = 1;

            while (true)
            {
                yield return current;
                current++;
            }
        }

        /// <summary>
        /// Implements infinite sequence of primes
        /// </summary>
        /// <returns>IEnumerable infinite sequence of primes</returns>
        public static IEnumerable<int> Primes()
        {
            return Filter(Integers(), IsPrime);
        }

        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args">method args</param>
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
