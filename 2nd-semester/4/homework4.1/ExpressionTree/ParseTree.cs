﻿namespace ExpressionTree
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Class that implements parse tree
    /// </summary>
    public class ParseTree
    {
        /// <summary>
        /// Root of the tree
        /// </summary>
        private Node root;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParseTree"/> class.
        /// </summary>
        /// <param name="expression">Parsed arithmetic expression</param>
        public ParseTree(string expression)
        {
            expression = expression.Replace(".", ",");
            this.root = this.BuildTree(expression);
        }

        /// <summary>
        /// Gets value of the parsed arithmetic expression
        /// </summary>
        public double Value => this.root.Value();

        /// <summary>
        /// Gets infix notation of the parsed arithmetic expression
        /// </summary>
        public string InfixNotation => this.root.InfixNotation;

        /// <summary>
        /// Gets operator by its character
        /// </summary>
        /// <param name="operatorChar">Character of operation</param>
        /// <returns>Wanted operator</returns>
        private static Operator GetOperator(char operatorChar)
        {
            switch (operatorChar)
            {
                case '+':
                    return new AdditionOperator();

                case '-':
                    return new SubtractionOperator();

                case '*':
                    return new MultiplicationOperator();

                case '/':
                    return new DivisionOperator();
            }

            throw new InvalidExpressionException("В выражении присутствует недопустимый символ.");
        }

        /// <summary>
        /// Builds parse tree
        /// </summary>
        /// <param name="expression">Parsed arithmetic expression</param>
        /// <returns>Root of the parse tree</returns>
        private Node BuildTree(string expression)
        {
            if (double.TryParse(expression, out double value))
            {
                return new Operand(value);
            }

            var (operationChar, leftOperand, rightOperand) = ExpressionParser.ParseExpression(expression);
            var node = GetOperator(operationChar);
            node.LeftChild = this.BuildTree(leftOperand);
            node.RightChild = this.BuildTree(rightOperand);

            return node;
        }

        /// <summary>
        /// Class that implements static methods for expression parsing
        /// </summary>
        private class ExpressionParser
        {
            /// <summary>
            /// Check whether expression is correct
            /// </summary>
            /// <param name="expression">Given expression</param>
            /// <returns>True if expression is correct, false otherwise</returns>
            public static bool IsValidExpression(string expression)
            {
                var match = Regex.Match(expression, @"^\([\*\/\+\-] ((\(.*\))|(\d+(,\d+)?)) ((\(.*\))|(\d+(,\d+)?))\)$");

                return match.Success;
            }

            /// <summary>
            /// Parse expression into operator and two operands
            /// </summary>
            /// <param name="expression">Given expression</param>
            /// <returns>Operator and two operands</returns>
            public static (char operatorChar, string leftOperand, string rightOperand) ParseExpression(string expression)
            {
                if (!IsValidExpression(expression))
                {
                    throw new InvalidExpressionException();
                }

                void Increment(bool flag, ref string onTrue, ref string onFalse, char value)
                {
                    if (flag)
                    {
                        onTrue += value;
                    }
                    else
                    {
                        onFalse += value;
                    }
                }

                var operatorChar = expression[1];
                var leftOperand = string.Empty;
                var rightOperand = string.Empty;

                bool isLeft = true;
                for (var i = 3; i < expression.Length; ++i)
                {
                    if (expression[i] == '(')
                    {
                        var bracketCount = -1;
                        while (expression[i] != ')' || bracketCount != 0)
                        {
                            if (expression[i] == '(')
                            {
                                ++bracketCount;
                            }

                            if (expression[i] == ')')
                            {
                                --bracketCount;
                            }

                            Increment(isLeft, ref leftOperand, ref rightOperand, expression[i]);

                            ++i;
                        }

                        Increment(isLeft, ref leftOperand, ref rightOperand, expression[i]);
                    }
                    else
                    {
                        if (expression[i] == ' ')
                        {
                            continue;
                        }

                        while (expression[i] != ' ' && expression[i] != ')')
                        {
                            Increment(isLeft, ref leftOperand, ref rightOperand, expression[i]);

                            ++i;
                        }
                    }

                    isLeft = false;
                }

                return (operatorChar, leftOperand, rightOperand);
            }
        }
    }
}
