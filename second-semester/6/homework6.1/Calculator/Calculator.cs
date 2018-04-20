using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class Calculator
    {
        Dictionary<string, Func<double, double, double>> dict = new Dictionary<string, Func<double, double, double>>
        {
            ["+"] = (double x, double y) => x + y,
            ["-"] = (double x, double y) => x - y,
            ["*"] = (double x, double y) => x * y,
            ["/"] = (double x, double y) => x / y,
        };

        public double Eval(string expression)
        {
            if (double.TryParse(expression, out double result))
            {
                return result;
            }

            var match = Regex.Match(expression, @"^\-?\d+(,\d+)? [\+\-\*\/] \-?\d+(,\d+)?$");
            if (!match.Success)
            {
                throw new InvalidExpressionException("Неверное выражение.");
            }

            var tokens = expression.Split(' ');
            var first = double.Parse(tokens[0]);
            var second = double.Parse(tokens[2]);
            var operatorChar = tokens[1];

            if (operatorChar == "/" && Math.Abs(second) < 0.000001)
            {
                throw new DivideByZeroException("Деление на ноль.");
            }

            return dict[operatorChar](first, second);
        }
    }
}
