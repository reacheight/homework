namespace ExpressionTree
{
    using System;

    public abstract class Node
    {
        public Node()
        {
        }

        public Node(Node leftChild, Node rightChild)
        {
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
        }

        public abstract string InfixNotation { get; }

        public Node LeftChild { get; set; }

        public Node RightChild { get; set; }

        public abstract double Value();

        public void Print()
        {
            Console.WriteLine(this.InfixNotation);
        }
    }
}
