using System;

namespace Factorial
{
    public class Program
    {
        public static int Factorial(int n) => n <= 1 ? 1 : n * Factorial(n - 1);

        public static int ReadNonnegativeInt()
        {
            Console.WriteLine("Введите целое неотрицательное число:");
            var inputData = Console.ReadLine();
            int n;

            while (!int.TryParse(inputData, out n) || n < 0)
            {
                if (n < 0)
                {
                    Console.WriteLine("Вы ввели отрицательное целое число.\nПопробуйте ещё раз:");
                }
                else
                {
                    Console.WriteLine("Вы ввели не целое число.\nПопробуйте ещё раз:");
                }

                inputData = Console.ReadLine();
            }

            return n;
        }

        public static void Main(string[] args)
        {
            var n = ReadNonnegativeInt();
            Console.WriteLine("{0}! = {1}", n, Factorial(n));
        }
    }
}
