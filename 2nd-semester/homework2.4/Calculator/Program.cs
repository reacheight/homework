using System;

namespace Calculator
{
    public class Program
    {

        private static void Main(string[] args)
        {
            var stack = new ReferenceStack<double>();
            var calculator = new Calculator(stack);
            for (int i = 1; i < 11; ++i)
            {
                stack.Push(i);
            }

            while (stack.Size >= 2)
            {
                calculator.Add();
                Console.WriteLine(stack.Top());
            }
        }
    }
}
