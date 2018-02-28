using System;

namespace HashTable
{
    public class HashTable
    {
        private readonly int size = 150;
        private List.List<string>[] array;

        public HashTable()
        {
            this.array = new List.List<string>[this.size];
            for (int i = 0; i < this.size; ++i)
            {
                this.array[i] = new List.List<string>();
            }
        }

        public void Add(string value)
        {
            if (!this.Contains(value))
            {
                this.GetList(value).Insert(value, 0);
            }
        }

        public void Erase(string value)
        {
            var position = this.Position(value);

            if (position == -1)
            {
                throw new ArgumentException("Попытка удалить элемент, которого нет в хэш-таблице");
            }

            this.GetList(value).Erase(position);
        }

        public bool Contains(string value) => this.Position(value) != -1;

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

        private List.List<string> GetList(string value)
        {
            int hash = HashFunc(value);
            return this.array[hash % this.size];
        }
    }
}
