using System;

namespace Homework1_5
{
    class Program
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

        public static void Swap(ref int x, ref int y)
        {
            var t = x;
            x = y;
            y = t;
        }

        public static void SwapColumns(int[,] matrix, int firstColumn, int secondColumn)
        {
            var rowsNumber = matrix.GetLength(0);
            var columnsNumber = matrix.GetLength(1);
            for (int i = 0; i < rowsNumber; ++i)
            {
                Swap(ref matrix[i, firstColumn], ref matrix[i, secondColumn]);
            }
        }

        public static void SortMatrix(int[,] matrix)
        {
            var columnsNumber = matrix.GetLength(1);
            for (var i = 1; i < columnsNumber; ++i)
            {
                var j = i;
                while (j > 0 && matrix[0, j] < matrix[0, j - 1])
                {
                    SwapColumns(matrix, j, j - 1);
                    --j;
                }
            }
        }

        public static void PrintMatrix(int[,] matrix)
        {
            var rowsNumber = matrix.GetLength(0);
            var columnsNumber = matrix.GetLength(1);
            for (var i = 0; i < rowsNumber; ++i)
            {
                for (var j = 0; j < columnsNumber; ++j)
                {
                    Console.Write($"{matrix[i, j]} ");
                }

                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите количество строк в матрице:");
            var rowsNumber = ReadInt();
            Console.WriteLine("Введите количество столбцов в матрице:");
            var columnsNumber = ReadInt();

            Console.WriteLine("Введите матрицу:");
            var matrix = ReadMatrix(rowsNumber, columnsNumber);
            SortMatrix(matrix);

            Console.WriteLine("Отсортированная матрица:");
            PrintMatrix(matrix);
        }
    }
}
