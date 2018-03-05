using System;

namespace Calculator
{
    public class Calculator
    {
        private IStack<double> stack;

        public Calculator(IStack<double> stack)
        {
            this.stack = stack;
        }

        public int Size => this.stack.Size;

        public bool Empty => this.stack.Empty;

        public double Result => this.stack.Top();

        public void Push(double value)
        {
            this.stack.Push(value);
        }

        public double Pop()
        {
            return this.stack.Pop();
        }

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

        public void Devide()
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
