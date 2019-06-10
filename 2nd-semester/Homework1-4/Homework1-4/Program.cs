using System;

namespace Homework1_4
{
    public class Program
    {
        public static int ReadInt()
        {
            var inputData = Console.ReadLine();
            int integer;
            while (!int.TryParse(inputData, out integer))
            {
                Console.WriteLine("Вы ввели не целое число, попробуйте ещё раз:");
                inputData = Console.ReadLine();
            }

            return integer;
        }

        public static int ReadOddInt()
        {
            int integer = ReadInt();
            while (integer % 2 != 1)
            {
                Console.WriteLine("Вы ввели чётное целое чило. Попробуйте ещё раз:");
                integer = ReadInt();
            }

            return integer;
        }

        public static int[,] ReadMatrix(int rowsNumber, int columnsNumber)
        {
            var matrix = new int[rowsNumber, columnsNumber];
            for (var i = 0; i < rowsNumber; ++i)
            {
                var inputData = Console.ReadLine().Split();
                for (var j = 0; j < columnsNumber; ++j)
                {
                    matrix[i, j] = int.Parse(inputData[j]);
                }
            }

            return matrix;
        }

        public static void PrintSpiral(int[,] matrix)
        {
            var size = matrix.GetLength(0);

            int x = size / 2;
            int y = size / 2;
            Console.Write($"{matrix[x, y]} ");
            
            for (var i = 1; i < size; ++i)
            {
                for (var j = 0; j < i; ++j)
                {
                    y += (i % 2 == 1) ? 1 : -1;
                    Console.Write($"{matrix[x, y]} ");
                }
                for (var j = 0; j < i; ++j)
                {
                    x += (i % 2 == 1) ? 1 : -1; ;
                    Console.Write($"{matrix[x, y]} ");
                }
            }

            while (y + 1 < size)
            {
                Console.Write($"{matrix[x, y + 1]} ");
                ++y;
            }
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Введите размер квадратной матрицы - целое нечетное число:");
            var size = ReadOddInt();
            Console.WriteLine("Введите матрицу:");
            var matrix = ReadMatrix(size, size);

            Console.WriteLine("Элементы матрицы при обходе по спирали:");
            PrintSpiral(matrix);
        }
    }
}
