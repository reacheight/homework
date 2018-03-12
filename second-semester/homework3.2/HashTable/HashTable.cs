using System;

namespace HashTable
{
    /// <summary>
    /// Class that implement the hash-table
    /// </summary>
    /// <typeparam name="T">Type of the elements in hashtable</typeparam>
    public class HashTable<T>
    {
        /// <summary>
        /// Number of lists in the hash table
        /// </summary>
        private const int Size = 150;

        /// <summary>
        /// Hash function
        /// </summary>
        private Func<T, int> hashFunction = (T value) => value.GetHashCode();

        /// <summary>
        /// Array of the lists
        /// </summary>
        private List<T>[] array;

        /// <summary>
        /// Initializes a new instance of the <see cref="HashTable{T}"/> class.
        /// </summary>
        /// <param name="hashFunction">Hash function</param>
        public HashTable(Func<T, int> hashFunction)
        {
            this.hashFunction = hashFunction;
            this.array = new List<T>[Size];
            for (int i = 0; i < Size; ++i)
            {
                this.array[i] = new List<T>();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HashTable{T}"/> class.
        /// </summary>
        public HashTable()
        {
            this.array = new List<T>[Size];
            for (int i = 0; i < Size; ++i)
            {
                this.array[i] = new List<T>();
            }
        }

        /// <summary>
        /// Add value to the hash table
        /// </summary>
        /// <param name="value">Value added to the hash table</param>
        public void Add(T value)
        {
            if (!this.Contains(value))
            {
                this.GetList(value).Insert(value, 0);
            }
        }

        /// <summary>
        /// Erase value from the hash table
        /// </summary>
        /// <param name="value">Value erased from the hash table</param>
        public void Erase(T value)
        {
            var list = this.GetList(value);
            var position = list.Position(value);

            if (position == -1)
            {
                throw new ValueNotInHashTableException("Попытка удалить элемент, которого нет в хэш-таблице");
            }

            list.Erase(position);
        }

        /// <summary>
        /// Check if value in the hash table
        /// </summary>
        /// <param name="value">Verified value</param>
        /// <returns>True if value in the hash table, false otherwise</returns>
        public bool Contains(T value) => this.GetList(value).Position(value) != -1;

        /// <summary>
        /// Get list that contains value
        /// </summary>
        /// <param name="value">Value whose list it returned</param>
        /// <returns>List of the value</returns>
        private List<T> GetList(T value)
        {
            var hash = Math.Abs(this.hashFunction(value));
            return this.array[hash % Size];
        }
    }
}
