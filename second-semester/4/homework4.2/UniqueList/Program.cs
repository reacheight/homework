using System;

namespace UniqueList
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var list = new UniqueList<int>();

            for (var i = 0; i < 10; ++i)
            {
                list.Insert(i, i);
            }

            list.EraseValue(8);

            try
            {
                list.Insert(3, 5);
            }
            catch (ValueAlreadyInListException e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                list.EraseValue(8);
            }
            catch (ValueNotInListException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
