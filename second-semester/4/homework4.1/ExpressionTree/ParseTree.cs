namespace ExpressionTree
{
    using System;

    public class ParseTree
    {
        private Node root;

        public ParseTree(string expression)
        {
            this.root = this.BuildTree(expression);
        }

        public double Value => this.root.Value();

        public string InfixNotation => this.root.InfixNotation;

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
