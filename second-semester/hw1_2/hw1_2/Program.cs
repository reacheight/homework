using System;

namespace Fibonacci
{
    public class Program
    {
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

        public static long Fibonacci(int n)
        {
            if (n == 0)
            {
                return 0;
            }

            long previous = 0;
            long current = 1;

            for (var i = 0; i < n - 1; ++i)
            {
                long oldCurrent = current;
                current += previous;
                previous = oldCurrent;
            }

            return current;
        }

        public static void Main(string[] args)
        {
            var n = ReadNonnegativeInt();
            Console.WriteLine($"Число Фибоначчи под номером {n} - {Fibonacci(n)}");
        }
    }
}
