namespace Set
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Class that implements a set as a binary search tree
    /// </summary>
    /// <typeparam name="T">type of the elements of a set</typeparam>
    public class Set<T> : ISet<T>
        where T : IComparable
    {
        /// <summary>
        /// Root of a tree
        /// </summary>
        private Node root;

        /// <summary>
        /// Initializes a new instance of the <see cref="Set{T}"/> class.
        /// </summary>
        public Set()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Set{T}"/> class.
        /// </summary>
        /// <param name="collection">collection of items that will be added to set</param>
        public Set(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                this.Add(item);
            }
        }

        /// <summary>
        /// Gets number of elements in a set
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets a value indicating whether set is readonly
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Adds item to set
        /// </summary>
        /// <param name="item">item to be added to set</param>
        /// <returns>true if item was added to set, false otherwise</returns>
        public bool Add(T item)
        {
            if (this.Contains(item))
            {
                return false;
            }

            this.Count++;

            if (this.root == null)
            {
                this.root = new Node(item);

                return true;
            }

            this.GetParent(item).SetChild(item, new Node(item));
            return true;
        }

        /// <summary>
        /// Clears a set
        /// </summary>
        public void Clear()
        {
            this.root = null;
            this.Count = 0;
        }

        /// <summary>
        /// Checks whether set contains given item
        /// </summary>
        /// <param name="item">item to be checked</param>
        /// <returns>true if set contains the item, false otherwise</returns>
        public bool Contains(T item)
        {
            var node = this.root;

            while (node != null)
            {
                if (node.Value.Equals(item))
                {
                    return true;
                }

                node = node.GetChild(item);
            }

            return false;
        }

        /// <summary>
        /// Copies a set into an array
        /// </summary>
        /// <param name="array">array in which set will be copied</param>
        /// <param name="arrayIndex">starting index of copying</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException($"Array {nameof(array)} is null");
            }

            if (arrayIndex < 0 || arrayIndex > array.Length - this.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Аргумент должен быть не меньше нуля и оставшееся количество ячеек массива должно быть не меньше размера множества");
            }

            var itemIndex = 0;
            foreach (var item in this)
            {
                array[arrayIndex + itemIndex] = item;
                ++itemIndex;
            }
        }

        /// <summary>
        /// Excepts a set with another collection
        /// </summary>
        /// <param name="other">collection to be excepted with a set</param>
        public void ExceptWith(IEnumerable<T> other)
        {
            foreach (var item in other)
            {
                this.Remove(item);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var queue = new Stack<Node>();
            queue.Push(this.root);

            while (queue.Count != 0)
            {
                var currentNode = queue.Pop();
                if (currentNode != null)
                {
                    yield return currentNode.Value;
                    queue.Push(currentNode.RightChild);
                    queue.Push(currentNode.LeftChild);
                }
            }
        }

        /// <summary>
        /// Intersects a set with another collection
        /// </summary>
        /// <param name="other">collection to be intersected with a set</param>
        public void IntersectWith(IEnumerable<T> other)
        {
            var intersection = new List<T>();
            foreach (var item in other)
            {
                if (this.Contains(item))
                {
                    intersection.Add(item);
                }
            }

            this.Clear();
            foreach (var item in intersection)
            {
                this.Add(item);
            }
        }

        /// <summary>
        /// Checks whether a set is a proper subset of given collection
        /// </summary>
        /// <param name="other">given collection</param>
        /// <returns>true if a set is a proper subset of given collection, false otherwise</returns>
        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            var set = new Set<T>(other);
            return this.IsSubsetOf(other) && this.Count < set.Count;
        }

        /// <summary>
        /// Checks whether a set is a proper superset of given collection
        /// </summary>
        /// <param name="other">given collection</param>
        /// <returns>true if a set is a proper superset of given collection, false otherwise</returns>
        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            var set = new Set<T>(other);
            return set.IsProperSubsetOf(this);
        }

        /// <summary>
        /// Checks whether a set is a subset of given collection
        /// </summary>
        /// <param name="other">given collection</param>
        /// <returns>true if a set is a subset of given collection, false otherwise</returns>
        public bool IsSubsetOf(IEnumerable<T> other)
        {
            var set = new Set<T>(other);

            foreach (var item in this)
            {
                if (!set.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check whether a set is a superset of given collection
        /// </summary>
        /// <param name="other">given collection</param>
        /// <returns>true if a set is a supersetof given collection, false otherwise</returns>
        public bool IsSupersetOf(IEnumerable<T> other)
        {
            var set = new Set<T>(other);
            return set.IsSubsetOf(this);
        }

        /// <summary>
        /// Checks whether a set overlaps given collection
        /// </summary>
        /// <param name="other">given collection</param>
        /// <returns>true if a set overlaps given collection, false otherwise</returns>
        public bool Overlaps(IEnumerable<T> other)
        {
            foreach (var item in other)
            {
                if (this.Contains(item))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes item from a set
        /// </summary>
        /// <param name="item">item to be removed from a set</param>
        /// <returns>true if item was removed from a set, false otherwise</returns>
        public bool Remove(T item)
        {
            if (!this.Contains(item))
            {
                return false;
            }

            this.Count--;

            if (this.root.Value.Equals(item))
            {
                return this.RemoveRoot();
            }

            var parent = this.GetParent(item);
            var node = parent.GetChild(item);

            if (node.RightChild != null)
            {
                node.RightChild.GetLeftmostDescendant().LeftChild = node.LeftChild;
                parent.SetChild(node.Value, node.RightChild);
            }
            else
            {
                parent.SetChild(node.Value, node.LeftChild);
            }

            return true;
        }

        /// <summary>
        /// Checks whether a set equals to given collection
        /// </summary>
        /// <param name="other">given collection</param>
        /// <returns>true if a set equals to given collection, false otherwise</returns>
        public bool SetEquals(IEnumerable<T> other)
        {
            var set = new Set<T>(other);
            return this.IsSubsetOf(set) && set.IsSubsetOf(this);
        }

        /// <summary>
        /// Excepts a set with given collection symmetrically
        /// </summary>
        /// <param name="other">given collections</param>
        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            foreach (var item in other)
            {
                if (this.Contains(item))
                {
                    this.Remove(item);
                }
                else
                {
                    this.Add(item);
                }
            }
        }

        /// <summary>
        /// Unions a set with given collection
        /// </summary>
        /// <param name="other">given collection</param>
        public void UnionWith(IEnumerable<T> other)
        {
            foreach (var item in other)
            {
                this.Add(item);
            }
        }

        void ICollection<T>.Add(T item)
        {
            this.Add(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Removes root of a tree
        /// </summary>
        /// <returns>true if root was deleted</returns>
        private bool RemoveRoot()
        {
            if (this.root.RightChild != null)
            {
                this.root.RightChild.GetLeftmostDescendant().LeftChild = this.root.LeftChild;
                this.root = this.root.RightChild;

                return true;
            }

            this.root = this.root.LeftChild;
            return true;
        }

        /// <summary>
        /// Gets parent for an item,
        /// if a set does not contain an item, gets node that will be item parent after addition item to a set
        /// </summary>
        /// <param name="item">item which parent will be returned</param>
        /// <returns>item parent</returns>
        private Node GetParent(T item)
        {
            Node parent = null;
            var node = this.root;

            if (!this.Contains(item))
            {
                while (node != null)
                {
                    parent = node;
                    node = node.GetChild(item);
                }

                return parent;
            }

            while (node != null)
            {
                if (node.Value.Equals(item))
                {
                    return parent;
                }

                parent = node;
                node = node.GetChild(item);
            }

            return parent;
        }

        /// <summary>
        /// Class that represents a node of a tree
        /// </summary>
        private class Node
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Node"/> class.
            /// </summary>
            /// <param name="value">node value</param>
            public Node(T value)
            {
                this.Value = value;
            }

            /// <summary>
            /// Gets or sets node value
            /// </summary>
            public T Value { get; set; }

            /// <summary>
            /// Gets or sets right child of a node
            /// </summary>
            public Node RightChild { get; set; }

            /// <summary>
            /// Gets or sets left child of a node
            /// </summary>
            public Node LeftChild { get; set; }

            /// <summary>
            /// Gets child of a node by a key
            /// </summary>
            /// <param name="key">given key</param>
            /// <returns>node left child if key is less than node value, node right child otherwise</returns>
            public Node GetChild(T key)
            {
                if (this.Value.CompareTo(key) > 0)
                {
                    return this.LeftChild;
                }
                else
                {
                    return this.RightChild;
                }
            }

            /// <summary>
            /// Set node child by a key
            /// </summary>
            /// <param name="key">given key</param>
            /// <param name="item">item by which a node chil will be replaced</param>
            public void SetChild(T key, Node item)
            {
                if (this.Value.CompareTo(key) > 0)
                {
                    this.LeftChild = item;
                }
                else
                {
                    this.RightChild = item;
                }
            }

            /// <summary>
            /// Gets node leftmost descendant
            /// </summary>
            /// <returns>node leftmost descendant</returns>
            public Node GetLeftmostDescendant()
            {
                var node = this;
                while (node.LeftChild != null)
                {
                    node = node.LeftChild;
                }

                return node;
            }
        }
    }
}
