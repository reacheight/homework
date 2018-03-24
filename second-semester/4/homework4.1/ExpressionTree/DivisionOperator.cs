namespace ExpressionTree
{
    using System;

    /// <summary>
    /// Class that represent division operator node of a tree
    /// </summary>
    public class DivisionOperator : Operator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DivisionOperator"/> class.
        /// </summary>
        public DivisionOperator()
        {
            this.OperatorChar = '/';
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DivisionOperator"/> class.
        /// </summary>
        /// <param name="leftChild">Left node's child</param>
        /// <param name="rightChild">Right node's child</param>
        public DivisionOperator(Node leftChild, Node rightChild)
            : base(leftChild, rightChild)
        {
            this.OperatorChar = '/';
        }

        /// <summary>
        /// Evaluate value of arithmetic expression of a parse tree with the root in that node
        /// </summary>
        /// <returns>Value of arithmetic expression of a parse tree with the root in that node</returns>
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
