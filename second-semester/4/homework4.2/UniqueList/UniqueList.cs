using System;

namespace UniqueList
{
    public class UniqueList<T> : List<T>
    {
        public new void Insert(T value, int position)
        {
            if (this.Position(value) != -1)
            {
                throw new ValueAlreadyInListException("Попытка добавить в список значение, которое уже находится в списке.");
            }

            base.Insert(value, position);
        }

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
