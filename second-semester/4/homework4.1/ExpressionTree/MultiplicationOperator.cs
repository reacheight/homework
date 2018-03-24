﻿namespace ExpressionTree
{
    using System;

    /// <summary>
    /// Class that represent multiplication operator node of a tree
    /// </summary>
    public class MultiplicationOperator : Operator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplicationOperator"/> class.
        /// </summary>
        public MultiplicationOperator()
        {
            this.OperatorChar = '*';
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplicationOperator"/> class.
        /// </summary>
        /// <param name="leftChild">Left node's child</param>
        /// <param name="rightChild">Right node's child</param>
        public MultiplicationOperator(Node leftChild, Node rightChild)
            : base(leftChild, rightChild)
        {
            this.OperatorChar = '*';
        }

        /// <summary>
        /// Evaluate value of arithmetic expression of a parse tree with the root in that node
        /// </summary>
        /// <returns>Value of arithmetic expression of a parse tree with the root in that node</returns>
        public override double Value() => this.LeftChild.Value() * this.RightChild.Value();
    }
}
