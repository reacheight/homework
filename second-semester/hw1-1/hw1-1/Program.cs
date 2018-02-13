using System;

namespace hw1_1
{
    class Program
    {
        static int Factorial(int n) => n <= 1 ? 1 : n * Factorial(n - 1);

        static void Main(string[] args)
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

            Console.WriteLine("{0}! = {1}", n, Factorial(n));
        }
    }
}
