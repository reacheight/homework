namespace ExpressionTree
{
    using System;

    public class AdditionOperator : Operator
    {
        public AdditionOperator()
        {
            this.OperatorChar = '+';
        }

        public AdditionOperator(Node leftChild, Node rightChild)
            : base(leftChild, rightChild)
        {
            this.OperatorChar = '+';
        }

        public override double Value() => this.LeftChild.Value() + this.RightChild.Value();
    }
}
