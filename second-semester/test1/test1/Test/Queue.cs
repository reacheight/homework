namespace Test
{
    using System;

    /// <summary>
    /// Class that implements priority queue
    /// </summary>
    /// <typeparam name="T">Type of the elements in a queue</typeparam>
    public class Queue<T>
    {
        /// <summary>
        /// Head of a queue
        /// </summary>
        private Node head;

        /// <summary>
        /// Gets a number of elements in a queue
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Insert to a queue value with priority
        /// </summary>
        /// <param name="value">Value of inserted item</param>
        /// <param name="priority">Priority of inserted item</param>
        public void Enqueue(T value, int priority)
        {
            this.Count++;

            var previous = this.GetPrevious(priority);
            if (previous == null || this.head == null)
            {
                this.head = new Node(value, priority, this.head);
                return;
            }

            previous.Next = new Node(value, priority, previous.Next);
        }

        /// <summary>
        /// Gets value of the item with the highest priority
        /// </summary>
        /// <returns>Value of the item with the highest priority</returns>
        public T Dequeue()
        {
            if (this.Count == 0)
            {
                throw new NotSupportedException("Удалять из пустой очереди нельзя.");
            }

            this.Count--;
            T result = this.head.Value;
            this.head = this.head.Next;
            return result;
        }

        /// <summary>
        /// Gets previous node by priority
        /// </summary>
        /// <param name="priority">Given priority of an item</param>
        /// <returns>Previous node by priority</returns>
        private Node GetPrevious(int priority)
        {
            Node previous = null;
            var start = this.head;
            while (start != null && start.Priority >= priority)
            {
                previous = start;
                start = start.Next;
            }

            return previous;
        }

        /// <summary>
        /// Class that implements item of a queue
        /// </summary>
        private class Node
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Node"/> class.
            /// </summary>
            /// <param name="value">Value of the item</param>
            /// <param name="priority">Priority of the item</param>
            /// <param name="next">Next item</param>
            public Node(T value, int priority, Node next)
            {
                this.Value = value;
                this.Priority = priority;
                this.Next = next;
            }

            /// <summary>
            /// Gets a priority of the item
            /// </summary>
            public int Priority { get; private set; }

            /// <summary>
            /// Gets a value of the item
            /// </summary>
            public T Value { get; private set; }

            /// <summary>
            /// Gets or sets a next item
            /// </summary>
            public Node Next { get; set; }
        }
    }
}
