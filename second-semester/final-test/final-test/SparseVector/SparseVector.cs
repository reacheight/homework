namespace SparseVector
{
    using System;
    using System.Collections.Generic;

    public class SparseVector
    {
        private Dictionary<int, int> hashTable = new Dictionary<int, int>();

        public SparseVector()
        {
        }

        public SparseVector(int size)
        {
            this.Count = size;
        }

        public int Count { get; private set; }

        public bool IsNull => this.hashTable.Count == 0;

        public int this[int index]
        {
            get
            {
                if (index >= this.Count || index < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                if (this.hashTable.ContainsKey(index))
                {
                    return this.hashTable[index];
                }
                else
                {
                    return 0;
                }
            }

            set
            {
                if (index >= this.Count || index < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                if (value == 0)
                {
                    this.hashTable.Remove(index);
                    return;
                }

                this.hashTable[index] = value;
            }
        }

        public IEnumerable<int> Keys()
        {
            foreach (var key in this.hashTable.Keys)
            {
                yield return key;
            }
        }

        public void PushBack(int value)
        {
            if (value == 0)
            {
                this.Count++;
                return;
            }

            this.hashTable[this.Count] = value;
            this.Count++;
        }

        public void Add(SparseVector other)
        {
            foreach (var key in other.Keys())
            {
                this[key] = this[key] + other[key];
            }
        }

        public void Subtract(SparseVector other)
        {
            foreach (var key in other.Keys())
            {
                this[key] = this[key] - other[key];
            }
        }

        public int Multiply(SparseVector other)
        {
            var result = 0;

            for (int i = 0; i < Math.Max(this.Count, other.Count); ++i)
            {
                result += this[i] * other[i];
            }

            return result;
        }

        public SparseVector Clone()
        {
            var copy = new SparseVector(this.Count);

            foreach (var key in this.Keys())
            {
                copy[key] = this[key];
            }

            return copy;
        }

        public void Extend(int size)
        {
            if (size > this.Count)
            {
                this.Count = size;
            }
        }
    }
}
