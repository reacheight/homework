using System;

namespace List
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<double>();

            for (int i = 0; i < 10; ++i)
            {
                list.Insert(3.14, i);
            }

            list.Erase(4);
        }

    }
}
