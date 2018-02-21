using System;

namespace Stack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var stack = new Stack();
            for (var i = 0; i < 10; ++i)
            {
                stack.Push(i);
            }

            Console.WriteLine($"Stack size is {stack.Size()}");

            var oddStack = new Stack();
            var evenStack = new Stack();

            var size = stack.Size();

            for (var i = 0; i < size; ++i)
            {
                var top = stack.Pop();

                Console.WriteLine($"Top element is {top}");

                if (top % 2 == 0)
                {
                    evenStack.Push(top);
                }
                else
                {
                    oddStack.Push(top);
                }
            }

            Console.WriteLine(oddStack.Top());
            Console.WriteLine(evenStack.Top());

            Console.Read();
        }
    }
}
