using System;

namespace Calculator
{
    public class ArrayStack<T> : IStack<T>
    {
        private const int Capacity = 100;
        private T[] array = new T[Capacity];

        public int Size { get; private set; }

        public bool Empty => this.Size == 0;

        public void Push(T value)
        {
            if (this.Size == Capacity)
            {
                throw new Exception();
            }

            this.array[this.Size] = value;
            ++this.Size;
        }

        public T Pop()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Попытка удалить элемент из пустого стэка.");
            }

            --this.Size;
            return this.array[this.Size];
        }

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
