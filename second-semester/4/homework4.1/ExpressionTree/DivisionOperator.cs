namespace ExpressionTree
{
    using System;

    public class DivisionOperator : Operator
    {
        public DivisionOperator()
        {
            this.OperatorChar = '/';
        }

        public DivisionOperator(Node leftChild, Node rightChild)
            : base(leftChild, rightChild)
        {
            this.OperatorChar = '/';
        }

        public override double Value()
        {
            if (this.RightChild.Value() < Math.Abs(0.000001))
            {
                throw new DivideByZeroException("Попытка разделить на ноль.");
            }

            return this.LeftChild.Value() / this.RightChild.Value();
        }
    }
}
