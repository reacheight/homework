namespace List
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Class that implements list
    /// </summary>
    /// <typeparam name="T">List item type</typeparam>
    public class List<T> : IList<T>
    {
        /// <summary>
        /// Head of a list
        /// </summary>
        private Node head;

        /// <summary>
        /// Gets a number of elements in a list
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets a value indicating whether list is readonly
        /// </summary>
        public bool IsReadOnly => true;

        /// <summary>
        /// Gets or sets a list element by the index
        /// </summary>
        /// <param name="index">Index of an element</param>
        /// <returns>Value in a list by the index</returns>
        public T this[int index]
        {
            get
            {
                if (index >= this.Count || index < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), "Аргумент должен быть не меньше нуля и меньше текущего размера списка.");
                }

                return this.GetNode(index).Value;
            }

            set
            {
                this.Insert(index, value);
            }
        }

        /// <summary>
        /// Add value in the end of a list
        /// </summary>
        /// <param name="item">Item to be added</param>
        public void Add(T item)
        {
            this.Insert(this.Count, item);
        }

        /// <summary>
        /// Clear a list
        /// </summary>
        public void Clear()
        {
            this.head = null;
        }

        /// <summary>
        /// Check whether a list contains the item
        /// </summary>
        /// <param name="item">Item to be checked</param>
        /// <returns>True if a list contains the item, false otherwise</returns>
        public bool Contains(T item) => this.IndexOf(item) != -1;

        /// <summary>
        /// Cope list to the array
        /// </summary>
        /// <param name="array">Array to wich a list is copied</param>
        /// <param name="arrayIndex">Starting index of copying</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException($"{nameof(array)} is null");
            }

            if (arrayIndex < 0 || arrayIndex > array.Length - this.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Аргумент должен быть не меньше нуля и оставшееся количество ячеек массива должно быть не меньше размера списка");
            }

            var itemIndex = 1;
            foreach (var item in this)
            {
                array[arrayIndex + itemIndex] = item;
            }
        }

        /// <summary>
        /// Returns enumerator
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new ListEnumerator(this.head);
        }

        /// <summary>
        /// Gets an index of the item
        /// </summary>
        /// <param name="item">Item whose index we get</param>
        /// <returns>Index of the item</returns>
        public int IndexOf(T item)
        {
            for (var i = 0; i < this.Count; ++i)
            {
                if (item.Equals(this[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Insert item in a list by the index
        /// </summary>
        /// <param name="index">Index by wich we insert an itme</param>
        /// <param name="item">Item to be inserted</param>
        public void Insert(int index, T item)
        {
            if (index > this.Count || index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Аргумент должен быть не меньше нуля и не больше текущего размера списка.");
            }

            if (index == 0)
            {
                this.InsertHead(item);

                return;
            }

            var previous = this.GetNode(index - 1);
            var newNode = new Node(item, previous.Next);
            previous.Next = newNode;

            ++this.Count;
        }

        /// <summary>
        /// Remove the item
        /// </summary>
        /// <param name="item">Item to be removed</param>
        /// <returns>True if the item was deleted, false otherwise</returns>
        public bool Remove(T item)
        {
            var index = this.IndexOf(item);

            if (index == -1)
            {
                return false;
            }

            this.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Remove an item by the index
        /// </summary>
        /// <param name="index">Index by which we remove an item</param>
        public void RemoveAt(int index)
        {
            if (index >= this.Count || index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Аргумент должен быть не меньше нуля и меньше текущего размера списка.");
            }

            if (index == 0)
            {
                this.EraseHead();

                return;
            }

            var previous = this.GetNode(index - 1);
            var current = previous.Next;
            previous.Next = current.Next;

            --this.Count;
        }

        /// <summary>
        /// Returns enumerator
        /// </summary>
        /// <returns>Enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ListEnumerator(this.head);
        }

        /// <summary>
        /// Get node of an item by the index
        /// </summary>
        /// <param name="position">Index of an item</param>
        /// <returns>Node object</returns>
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
        /// Insert to the head of a list
        /// </summary>
        /// <param name="value">Value to be inserted</param>
        private void InsertHead(T value)
        {
            var newHead = new Node(value, this.head);
            this.head = newHead;
            ++this.Count;
        }

        /// <summary>
        /// Remove value form the head of a list
        /// </summary>
        private void EraseHead()
        {
            this.head = this.head.Next;
            --this.Count;
        }

        /// <summary>
        /// Class that implementsa a node of a list
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
            /// Gets or sets next node
            /// </summary>
            public Node Next { get; set; }
        }

        /// <summary>
        /// Class that implements enumerator of a list
        /// </summary>
        private class ListEnumerator : IEnumerator<T>
        {
            /// <summary>
            /// Starting node
            /// </summary>
            private Node start;

            /// <summary>
            /// Current item node
            /// </summary>
            private Node current;

            /// <summary>
            /// Initializes a new instance of the <see cref="ListEnumerator"/> class.
            /// </summary>
            /// <param name="start">Starting node</param>
            public ListEnumerator(Node start)
            {
                this.start = start;
            }

            /// <summary>
            /// Gets value of the current item
            /// </summary>
            public T Current => this.current.Value;

            /// <summary>
            /// Gets value of the current item
            /// </summary>
            object IEnumerator.Current => this.current.Value;

            public void Dispose()
            {
            }

            /// <summary>
            /// Change the current node
            /// </summary>
            /// <returns>True if there is next node, false otherwise</returns>
            public bool MoveNext()
            {
                if (this.start == null || (this.current != null && this.current.Next == null))
                {
                    return false;
                }

                this.current = this.current?.Next ?? this.start;

                return true;
            }

            /// <summary>
            /// Reset the current node
            /// </summary>
            public void Reset()
            {
                this.current = null;
            }
        }
    }
}
