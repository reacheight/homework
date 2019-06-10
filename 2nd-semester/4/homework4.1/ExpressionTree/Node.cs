namespace ExpressionTree
{
    using System;

    /// <summary>
    /// Class that implements node of a tree
    /// </summary>
    public abstract class Node
    {
        /// <summary>
        /// Gets infix notation of a parse tree with the root in that node
        /// </summary>
        public abstract string InfixNotation { get; }

        /// <summary>
        /// Evaluate value of arithmetic expression of a parse tree with the root in that node
        /// </summary>
        /// <returns>value of arithmetic expression of a parse tree with the root in that node</returns>
        public abstract double Value();
    }
}
