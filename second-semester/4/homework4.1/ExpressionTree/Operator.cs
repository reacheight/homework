namespace ExpressionTree
{
    using System;

    public abstract class Operator : Node
    {
        public Operator()
        {
        }

        public Operator(Node leftChild, Node rightChild)
            : base(leftChild, rightChild)
        {
        }

        public char OperatorChar { get; set; }

        public override string InfixNotation =>
            $"({this.LeftChild.InfixNotation} {this.OperatorChar} {this.RightChild.InfixNotation})";
    }
}
