using System;

namespace Calculator
{
    public class Calculator
    {
        public double Eval(string expression)
        {
            var tokens = expression.Split(' ');

            if (tokens.Length == 1)
            {
                return double.Parse(tokens[0]);
            }

            var first = double.Parse(tokens[0]);
            var second = double.Parse(tokens[2]);
            var operatorChar = tokens[1];

            switch (operatorChar)
            {
                case "+":
                    return first + second;

                case "-":
                    return first - second;

                case "*":
                    return first * second;

                case "/":
                    return first / second;

                default:
                    throw new Exception();
            }
        }
    }
}
