using System;

namespace Stack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var stack = new Stack();
            stack.Push(3);
            stack.Push(7);

            Console.WriteLine(stack.Size());
            Console.WriteLine(stack.Pop());

            if (!stack.IsEmpty())
            {
                Console.WriteLine(stack.Top());
            }
        }
    }
}
