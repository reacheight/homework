using System;

namespace Calculator
{
    public class Program
    {

        private static void Main(string[] args)
        {
            var calculator = new Calculator(new ReferenceStack<double>());
            for (int i = 1; i < 11; ++i)
            {
                calculator.Push(i);
            }

            while (calculator.Size >= 2)
            {
                calculator.Add();
                Console.WriteLine(calculator.Top);
            }
        }
    }
}
