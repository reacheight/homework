namespace Calculator
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Class that implements simple binary calculator (operations with two real operand)
    /// </summary>
    public class Calculator
    {
        /// <summary>
        /// Easy way to choose right function
        /// </summary>
        private Dictionary<string, Func<double, double, double>> dict = new Dictionary<string, Func<double, double, double>>
        {
            ["+"] = (double x, double y) => x + y,
            ["-"] = (double x, double y) => x - y,
            ["*"] = (double x, double y) => x * y,
            ["/"] = (double x, double y) => x / y,
        };

        /// <summary>
        /// Evaluate given arithmetic expression
        /// </summary>
        /// <param name="expression">given arithmetic expression</param>
        /// <returns>result of evaluating</returns>
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

            return this.dict[operatorChar](first, second);
        }
    }
}
