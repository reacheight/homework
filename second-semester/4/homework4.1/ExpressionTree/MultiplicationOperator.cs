namespace ExpressionTree
{
    using System;

    public class MultiplicationOperator : Operator
    {
        public MultiplicationOperator()
        {
            this.OperatorChar = '*';
        }

        public MultiplicationOperator(Node leftChild, Node rightChild)
            : base(leftChild, rightChild)
        {
            this.OperatorChar = '*';
        }

        public override double Value() => this.LeftChild.Value() * this.RightChild.Value();
    }
}
