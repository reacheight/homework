using System;

namespace Stack
{
    /// <summary>
    /// Class implementing a stack
    /// </summary>
    public class Stack
    {
        private StackElement head = null;
        private int size = 0;

        /// <summary>
        /// Push an integer to the top of the stack
        /// </summary>
        /// <param name="value">The value of the integer added to the stack</param>
        public void Push(int value)
        {
            this.head = new StackElement(value, this.head);
            ++this.size;
        }

        /// <summary>
        /// Pop an integer from the top of the stack
        /// </summary>
        /// <returns>The value of the poped integer</returns>
        public int Pop()
        {
            if (this.size == 0)
            {
                throw new InvalidOperationException("Попытка удалить элемент из пустого стэка.");
            }

            int value = this.head.Value();
            this.head = this.head.Next();

            --this.size;

            return value;
        }

        /// <summary>
        /// Get the value of the top of the stack
        /// </summary>
        /// <returns>The value of the top of the stack</returns>
        public int Top()
        {
            if (this.size == 0)
            {
                throw new InvalidOperationException("Попытка получить значение головы пустого стэка.");
            }

            return this.head.Value();
        }

        /// <summary>
        /// Get stack size
        /// </summary>
        /// <returns>Stack size</returns>
        public int Size() => this.size;

        /// <summary>
        /// Check if the stack is empty
        /// </summary>
        /// <returns>True if the stack is empty, false otherwise</returns>
        public bool IsEmpty() => this.size == 0;

        /// <summary>
        /// Class implementing an element of the stack
        /// </summary>
        private class StackElement
        {
            private int value;
            private StackElement next;

            public StackElement(int value, StackElement next)
            {
                this.value = value;
                this.next = next;
            }

            /// <summary>
            /// Get value of the elemet of the stack
            /// </summary>
            /// <returns>Value of the element of the stack</returns>
            public int Value() => this.value;

            /// <summary>
            /// Get the next element of the stack
            /// </summary>
            /// <returns>The next element of the stack</returns>
            public StackElement Next() => this.next;
        }
    }
}
