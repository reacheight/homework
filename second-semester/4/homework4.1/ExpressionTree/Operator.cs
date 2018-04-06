namespace ExpressionTree
{
    using System;

    /// <summary>
    /// Class that implements operator node of a tree
    /// </summary>
    public abstract class Operator : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Operator"/> class.
        /// </summary>
        public Operator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Operator"/> class.
        /// </summary>
        /// <param name="leftChild">Left node's child</param>
        /// <param name="rightChild">Right node's child</param>
        public Operator(Node leftChild, Node rightChild)
        {
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
        }

        /// <summary>
        /// Gets or sets left node's child
        /// </summary>
        public Node LeftChild { get; set; }

        /// <summary>
        /// Gets or sets right node's child
        /// </summary>
        public Node RightChild { get; set; }

        /// <summary>
        /// Gets or sets operation character
        /// </summary>
        public char OperatorChar { get; set; }

        /// <summary>
        /// Gets infix notation of a parse tree with the root in that node
        /// </summary>
        public override string InfixNotation =>
            $"({this.LeftChild.InfixNotation} {this.OperatorChar} {this.RightChild.InfixNotation})";
    }
}
