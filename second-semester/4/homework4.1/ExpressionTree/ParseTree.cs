namespace ExpressionTree
{
    using System;

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
        /// Parse expression to operator and two operands
        /// </summary>
        /// <param name="expression">Parsed arithmetic expression</param>
        /// <returns>Tuple of operator and two operands</returns>
        private static (char operatorChar, string leftOperand, string rightOperand) ParseExpression(string expression)
        {
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

            throw new WrongCharacterException("В выражении присутствует недопустимый символ.");
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
                var operand = new Operand(value);

                return operand;
            }

            var (operationChar, leftOperand, rightOperand) = ParseExpression(expression);
            var node = GetOperator(operationChar);
            node.LeftChild = this.BuildTree(leftOperand);
            node.RightChild = this.BuildTree(rightOperand);

            return node;
        }
    }
}
