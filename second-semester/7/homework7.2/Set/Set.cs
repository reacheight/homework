namespace Set
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Set<T> : ISet<T>
        where T : IComparable
    {
        private Node root;

        public Set()
        {
        }

        public Set(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                this.Add(item);
            }
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

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

        public void Clear()
        {
            this.root = null;
            this.Count = 0;
        }

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

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

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

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            var set = new Set<T>(other);
            return this.IsSubsetOf(other) && this.Count < set.Count;
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            var set = new Set<T>(other);
            return set.IsProperSubsetOf(this);
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            var set = new Set<T>(other);

            bool isSubset = true;
            foreach (var item in this)
            {
                if (!set.Contains(item))
                {
                    isSubset = false;
                    break;
                }
            }

            return isSubset;
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            var set = new Set<T>(other);
            return set.IsSubsetOf(this);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

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

        public bool SetEquals(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

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

        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
            }

            public T Value { get; set; }

            public Node RightChild { get; set; }

            public Node LeftChild { get; set; }

            public bool HasNoChild => this.RightChild == null && this.LeftChild == null;

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
