namespace List
{
    using System;

    public class Program
    {
        private static void Main(string[] args)
        {
            var list = new List<string>();

            list.Add("one");
            list.Add("two");
            list.Add("Three");
            list.Insert(0, "four");

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(list[1]);

            list.Remove("one");

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            if (list.Contains("two"))
            {
                Console.WriteLine("deleting two");
                list.RemoveAt(list.IndexOf("two"));
            }

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}