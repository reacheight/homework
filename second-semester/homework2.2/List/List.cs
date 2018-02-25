using System;

namespace List
{
    public class List<T>
    {
        private int size;
        private Node<T> head;

        public int Size => this.size;

        public bool IsEmpty() => this.size == 0;

        public void Insert(T value, int position)
        {
            if (position > this.size || position < 0)
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
            previous.SetNext(newNode);

            ++this.size;
        }

        public void Erase(int position)
        {
            if (position >= this.size || position < 0)
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
            previous.SetNext(current.Next);

            --this.size;
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
            --this.size;
        }

        private void InsertHead(T value)
        {
            var newHead = new Node<T>(value, this.head);
            this.head = newHead;
            ++this.size;
        }

        private class Node<T>
        {
            private readonly T value;
            private Node<T> next;

            public Node(T value, Node<T> next)
            {
                this.value = value;
                this.next = next;
            }

            public T Value => this.value;

            public Node<T> Next => this.next;

            public void SetNext(Node<T> next)
            {
                this.next = next;
            }
        }
    }
}
