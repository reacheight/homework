namespace ExpressionTree
{
    using System;

    /// <summary>
    /// Class that implements node of a tree
    /// </summary>
    public abstract class Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        public Node()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        /// <param name="leftChild">Left node's child</param>
        /// <param name="rightChild">Right node's child</param>
        public Node(Node leftChild, Node rightChild)
        {
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
        }

        /// <summary>
        /// Gets infix notation of a parse tree with the root in that node
        /// </summary>
        public abstract string InfixNotation { get; }

        /// <summary>
        /// Gets or sets left node's child
        /// </summary>
        public Node LeftChild { get; set; }

        /// <summary>
        /// Gets or sets right node's child
        /// </summary>
        public Node RightChild { get; set; }

        /// <summary>
        /// Evaluate value of arithmetic expression of a parse tree with the root in that node
        /// </summary>
        /// <returns>value of arithmetic expression of a parse tree with the root in that node</returns>
        public abstract double Value();
    }
}
