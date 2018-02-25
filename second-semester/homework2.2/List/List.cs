using System;

namespace List
{
    public class List<T>
    {
        private Node<T> head;

        public int Size { get; private set; }

        public bool IsEmpty() => this.Size == 0;

        public void Insert(T value, int position)
        {
            if (position > this.Size || position < 0)
            {
                throw new ArgumentException("Неверное значение аргумента");
            }

            if (position == 0)
            {
                this.InsertHead(value);

                return;
            }

            var previous = this.Previous(position);
            var newNode = new Node<T>(value, previous.Next);
            previous.Next = newNode;

            ++this.Size;
        }

        public void Erase(int position)
        {
            if (position >= this.Size || position < 0)
            {
                throw new ArgumentException("Неверное значение аргумента.");
            }

            if (position == 0)
            {
                this.EraseHead();

                return;
            }

            var current = this.Previous(position + 1);
            var previous = this.Previous(position);
            previous.Next = current.Next;

            --this.Size;
        }

        private Node<T> Previous(int position)
        {
            var start = this.head;
            for (var i = 0; i < position - 1; ++i)
            {
                start = start.Next;
            }

            return start;
        }

        private void EraseHead()
        {
            this.head = this.head.Next;
            --this.Size;
        }

        private void InsertHead(T value)
        {
            var newHead = new Node<T>(value, this.head);
            this.head = newHead;
            ++this.Size;
        }

        private class Node<T>
        {
            public Node(T value, Node<T> next)
            {
                this.Value = value;
                this.Next = next;
            }

            public T Value { get; }

            public Node<T> Next { get; set; }
        }
    }
}
