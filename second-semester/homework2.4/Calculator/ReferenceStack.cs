using System;

namespace Calculator
{
    public class ReferenceStack<T> : IStack<T>
    {
        private StackElement head;

        /// <summary>
        /// Gets size of the stack
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Gets a value indicating whether stack is empty
        /// </summary>
        public bool Empty => this.Size == 0;

        /// <summary>
        /// Push an integer to the top of the stack
        /// </summary>
        /// <param name="value">The value of the integer added to the stack</param>
        public void Push(T value)
        {
            this.head = new StackElement
            {
                Value = value,
                Next = this.head
            };

            ++this.Size;
        }

        /// <summary>
        /// Pop an integer from the top of the stack
        /// </summary>
        /// <returns>The value of the poped integer</returns>
        public T Pop()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Попытка удалить элемент из пустого стэка.");
            }

            var value = this.head.Value;
            this.head = this.head.Next;

            --this.Size;

            return value;
        }

        /// <summary>
        /// Get the value of the top of the stack
        /// </summary>
        /// <returns>The value of the top of the stack</returns>
        public T Top()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Попытка получить значение головы пустого стэка.");
            }

            return this.head.Value;
        }

        /// <summary>
        /// Class implementing an element of the stack
        /// </summary>
        private class StackElement
        {
            public T Value { get; set; }

            public StackElement Next { get; set; }
        }
    }
}
