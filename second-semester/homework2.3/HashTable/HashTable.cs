using System;

namespace HashTable
{
    /// <summary>
    /// Class that implement the hash-table
    /// </summary>
    public class HashTable
    {
        /// <summary>
        /// Number of lists in the hash table
        /// </summary>
        private readonly int size = 150;

        /// <summary>
        /// Array of the lists
        /// </summary>
        private List<string>[] array;

        public HashTable()
        {
            this.array = new List<string>[this.size];
            for (int i = 0; i < this.size; ++i)
            {
                this.array[i] = new List<string>();
            }
        }

        /// <summary>
        /// Add value to the hash table
        /// </summary>
        /// <param name="value">Value added to the hash table</param>
        public void Add(string value)
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
        public void Erase(string value)
        {
            var position = this.Position(value);

            if (position == -1)
            {
                throw new ArgumentException("Попытка удалить элемент, которого нет в хэш-таблице");
            }

            this.GetList(value).Erase(position);
        }

        /// <summary>
        /// Check if value in the hash table
        /// </summary>
        /// <param name="value">Verified value</param>
        /// <returns>True if value in the hash table, false otherwise</returns>
        public bool Contains(string value) => this.Position(value) != -1;

        /// <summary>
        /// Hash-function
        /// </summary>
        /// <param name="key">Key of the hash function</param>
        /// <returns>Hash value of the key</returns>
        private static int HashFunc(string key)
        {
            int result = 0;
            int p = 31;
            foreach (var ch in key)
            {
                result = (result * p) + (int)ch;
            }

            return result;
        }

        /// <summary>
        /// Get position of the value in the list in the hash table
        /// </summary>
        /// <param name="value">Value whose position is returned</param>
        /// <returns>Position of the value if value in the hash table, -1 otherwise</returns>
        private int Position(string value)
        {
            var list = this.GetList(value);
            for (var i = 0; i < list.Size; ++i)
            {
                if (list.Value(i) == value)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Get list that contains value
        /// </summary>
        /// <param name="value">Value whose list it returned</param>
        /// <returns>List of the value</returns>
        private List<string> GetList(string value)
        {
            int hash = HashFunc(value);
            return this.array[hash % this.size];
        }
    }
}
