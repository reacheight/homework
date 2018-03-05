using System;

namespace Calculator
{
    /// <summary>
    /// Class that implements calculator on the stack
    /// </summary>
    public class Calculator
    {
        /// <summary>
        /// Stack of the calculator
        /// </summary>
        private IStack<double> stack;

        /// <summary>
        /// Initializes a new instance of the <see cref="Calculator"/> class.
        /// Constructor of the class
        /// </summary>
        /// <param name="stack">Stack of the calculator</param>
        public Calculator(IStack<double> stack)
        {
            this.stack = stack;
        }

        /// <summary>
        /// Gets number of elements in the stack of the calculator
        /// </summary>
        public int Size => this.stack.Size;

        /// <summary>
        /// Gets a value indicating whether stack of the calculator is empty
        /// </summary>
        public bool Empty => this.stack.Empty;

        /// <summary>
        /// Gets top of the stack of the calculator
        /// </summary>
        public double Result => this.stack.Top();

        /// <summary>
        /// Push value to the top of the stack of the calculator
        /// </summary>
        /// <param name="value">Pushed value</param>
        public void Push(double value)
        {
            this.stack.Push(value);
        }

        /// <summary>
        /// Pop value from the top of the stack of the calculator
        /// </summary>
        /// <returns>Poped value</returns>
        public double Pop()
        {
            return this.stack.Pop();
        }

        /// <summary>
        /// Adds two values on the top of the stack, push result on the top of the stack
        /// </summary>
        public void Add()
        {
            if (this.Size < 2)
            {
                throw new InvalidOperationException("При выполнении операции размер стека должен быть больше или равен двум.");
            }

            var first = this.stack.Pop();
            var second = this.stack.Pop();

            this.stack.Push(first + second);
        }

        /// <summary>
        /// Subtracts the top of the stack from the previous value, push result on the top of the stack
        /// </summary>
        public void Subtract()
        {
            if (this.Size < 2)
            {
                throw new InvalidOperationException("При выполнении операции размер стека должен быть больше или равен двум.");
            }

            var first = this.stack.Pop();
            var second = this.stack.Pop();

            this.stack.Push(second - first);
        }

        /// <summary>
        /// Myltiplies two values on top of the stack, push result on the top of the stack
        /// </summary>
        public void Multiply()
        {
            if (this.Size < 2)
            {
                throw new InvalidOperationException("При выполнении операции размер стека должен быть больше или равен двум.");
            }

            var first = this.stack.Pop();
            var second = this.stack.Pop();

            this.stack.Push(first * second);
        }

        /// <summary>
        /// Divides value previous to the top of the stack by the top of the stack, push result on the top of the stack
        /// </summary>
        public void Divide()
        {
            if (this.Size < 2)
            {
                throw new InvalidOperationException("При выполнении операции размер стека должен быть больше или равен двум.");
            }

            var first = this.stack.Pop();
            var second = this.stack.Pop();

            if (Math.Abs(first) < 0.0000001)
            {
                throw new DivideByZeroException("Происходит деление на ноль.");
            }

            this.stack.Push(second / first);
        }
    }
}
