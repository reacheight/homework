namespace ExpressionTree
{
    using System;

    public class SubtractionOperator : Operator
    {
        public SubtractionOperator()
        {
            this.OperatorChar = '-';
        }

        public SubtractionOperator(Node leftChild, Node rightChild)
            : base(leftChild, rightChild)
        {
            this.OperatorChar = '-';
        }

        public override double Value() => this.LeftChild.Value() - this.RightChild.Value();
    }
}
