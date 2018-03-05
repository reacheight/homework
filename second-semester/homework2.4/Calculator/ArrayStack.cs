using System;

namespace Calculator
{
    /// <summary>
    /// Class implemennting array-based stack
    /// </summary>
    /// <typeparam name="T">Type of the elements in the stack</typeparam>
    public class ArrayStack<T> : IStack<T>
    {
        /// <summary>
        /// Capacity of stack (number of maximum elements in the stack
        /// </summary>
        private const int Capacity = 100;

        /// <summary>
        /// Base of the stack
        /// </summary>
        private T[] array = new T[Capacity];

        /// <summary>
        /// Gets number of elements in the stack
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Gets a value indicating whether stack is empty
        /// </summary>
        public bool Empty => this.Size == 0;

        /// <summary>
        /// Push value to the top of the stack
        /// </summary>
        /// <param name="value">Pushed value</param>
        public void Push(T value)
        {
            if (this.Size == Capacity)
            {
                throw new Exception();
            }

            this.array[this.Size] = value;
            ++this.Size;
        }

        /// <summary>
        /// Remove value from the top of the stack
        /// </summary>
        /// <returns>Poped value</returns>
        public T Pop()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Попытка удалить элемент из пустого стэка.");
            }

            --this.Size;
            return this.array[this.Size];
        }

        /// <summary>
        /// Top of the stack
        /// </summary>
        /// <returns>Value of the top of the stack</returns>
        public T Top()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Попытка получить значение головы пустого стэка.");
            }

            return this.array[this.Size - 1];
        }
    }
}
