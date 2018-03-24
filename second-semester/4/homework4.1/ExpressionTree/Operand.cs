namespace ExpressionTree
{
    using System;

    public class Operand : Node
    {
        private double value;

        public Operand(double value)
        {
            this.value = value;
        }

        public override string InfixNotation => this.value.ToString();

        public override double Value() => this.value;
    }
}
