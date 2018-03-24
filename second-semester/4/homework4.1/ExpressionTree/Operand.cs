namespace ExpressionTree
{
    using System;

    /// <summary>
    /// Class that implements real operand
    /// </summary>
    public class Operand : Node
    {
        /// <summary>
        /// Real value of the operand
        /// </summary>
        private double value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Operand"/> class.
        /// </summary>
        /// <param name="value">Real value of the operand</param>
        public Operand(double value)
        {
            this.value = value;
        }

        /// <summary>
        /// Gets infix notation of a parse tree with the root in that node
        /// In this case it is the value itself
        /// </summary>
        public override string InfixNotation => this.value.ToString();

        /// <summary>
        /// Evaluate value of arithmetic expression of a parse tree with the root in that node
        /// In this case it is the value itself
        /// </summary>
        /// <returns>Value of arithmetic expression of a parse tree with the root in that node</returns>
        public override double Value() => this.value;
    }
}
