using System;

namespace UniqueList
{
    public class UniqueList<T> : List<T>
    {
        /// <summary>
        /// Insert value to the list
        /// </summary>
        /// <param name="value">Inserted value</param>
        /// <param name="position">Position of inserted value</param>
        public new void Insert(T value, int position)
        {
            if (this.Position(value) != -1)
            {
                throw new ValueAlreadyInListException("Попытка добавить в список значение, которое уже находится в списке.");
            }

            base.Insert(value, position);
        }

        /// <summary>
        /// Remove value from the list
        /// </summary>
        /// <param name="value">Removed value</param>
        public void EraseValue(T value)
        {
            var position = this.Position(value);

            if (position == -1)
            {
                throw new ValueNotInListException("Попытка удалить из списка значение, которого нет в списке.");
            }

            this.Erase(position);
        }
    }
}
