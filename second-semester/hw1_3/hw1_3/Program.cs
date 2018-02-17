using System;

namespace hw1_3
{
    public class Program
    {
        public static void Swap(ref int x, ref int y)
        {
            var t = x;
            x = y;
            y = t;
        }

        public static void SortArray(ref int[] array)
        {
            for (var i = 1; i < array.Length; ++i)
            {
                var j = i;
                while (j > 0 && array[j] < array[j - 1])
                {
                    Swap(ref array[j], ref array[j - 1]);
                    --j;
                }
            }
        }

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

        public static void ReadArray(ref int[] array)
        {
            var size = array.Length;
            Console.WriteLine($"Введите {size} строк, по одному числу в каждой.");
            for (var i = 0; i < size; ++i)
            {
                array[i] = ReadInt();
            }
        }

        public static void PrintArray(ref int[] array)
        {
            Console.WriteLine(string.Join(", ", array));
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Введите количество элементов в массиве");
            int size = ReadInt();

            int[] array = new int[size];
            ReadArray(ref array);
            SortArray(ref array);

            Console.Write("Отсортированный массив: ");
            PrintArray(ref array);
        }
    }
}
