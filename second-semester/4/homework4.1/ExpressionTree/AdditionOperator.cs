namespace ExpressionTree
{
    using System;

    /// <summary>
    /// Class that represent addition operator node of a tree
    /// </summary>
    public class AdditionOperator : Operator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionOperator"/> class.
        /// </summary>
        public AdditionOperator()
        {
            this.OperatorChar = '+';
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionOperator"/> class.
        /// </summary>
        /// <param name="leftChild">Left node's child</param>
        /// <param name="rightChild">Right node's child</param>
        public AdditionOperator(Node leftChild, Node rightChild)
            : base(leftChild, rightChild)
        {
            this.OperatorChar = '+';
        }

        /// <summary>
        /// Evaluate value of arithmetic expression of a parse tree with the root in that node
        /// </summary>
        /// <returns>Value of arithmetic expression of a parse tree with the root in that node</returns>
        public override double Value() => this.LeftChild.Value() + this.RightChild.Value();
    }
}
