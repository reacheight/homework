using System;

namespace List
{
    /// <summary>
    /// Class that implements the list
    /// </summary>
    /// <typeparam name="T">Type of the elements of the list</typeparam>
    public class List<T>
    {
        /// <summary>
        /// Head of the list
        /// </summary>
        private Node head;

        /// <summary>
        /// Gets number of elements in the list
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Check if list is empty
        /// </summary>
        /// <returns>True if list is empty, false otherwise</returns>
        public bool IsEmpty() => this.Size == 0;

        /// <summary>
        /// Insert new element to the list
        /// </summary>
        /// <param name="value">Value of the element</param>
        /// <param name="position">Position in which the element is inserted</param>
        public void Insert(T value, int position)
        {
            if (position > this.Size || position < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(position), "Аргумент должен быть не меньше нуля и не больше текущего размера списка.");
            }

            if (position == 0)
            {
                this.InsertHead(value);

                return;
            }

            var previous = this.GetNode(position - 1);
            var newNode = new Node(value, previous.Next);
            previous.Next = newNode;

            ++this.Size;
        }

        /// <summary>
        /// Remove element from the list
        /// </summary>
        /// <param name="position">Position of the removed element</param>
        public void Erase(int position)
        {
            if (position >= this.Size || position < 0)
            {
                throw new ArgumentOutOfRangeException("position", "Аргумент должен быть не меньше нуля и меньше текущего размера списка.");
            }

            if (position == 0)
            {
                this.EraseHead();

                return;
            }

            var current = this.GetNode(position);
            var previous = this.GetNode(position - 1);
            previous.Next = current.Next;

            --this.Size;
        }

        /// <summary>
        /// Get value of the element in that position
        /// </summary>
        /// <param name="position">Position of the element whose value is got</param>
        /// <returns>Value of the element in that position</returns>
        public T Value(int position) => this.GetNode(position).Value;

        /// <summary>
        /// Get element from the list
        /// </summary>
        /// <param name="position">Position of the received element</param>
        /// <returns>Element of the list with this position</returns>
        private Node GetNode(int position)
        {
            var start = this.head;
            for (var i = 0; i < position; ++i)
            {
                start = start.Next;
            }

            return start;
        }

        /// <summary>
        /// Remove head of the list
        /// </summary>
        private void EraseHead()
        {
            this.head = this.head.Next;
            --this.Size;
        }

        /// <summary>
        /// Insert head of the list
        /// </summary>
        /// <param name="value">Value of the head of the list</param>
        private void InsertHead(T value)
        {
            var newHead = new Node(value, this.head);
            this.head = newHead;
            ++this.Size;
        }

        /// <summary>
        /// Class that implements list node
        /// </summary>
        private class Node
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Node"/> class
            /// </summary>
            /// <param name="value">Value of the new node</param>
            /// <param name="next">Next node in the list</param>
            public Node(T value, Node next)
            {
                this.Value = value;
                this.Next = next;
            }

            /// <summary>
            /// Gets value of the node
            /// </summary>
            public T Value { get; }

            /// <summary>
            /// Gets or sets next node in the list
            /// </summary>
            public Node Next { get; set; }
        }
    }
}
