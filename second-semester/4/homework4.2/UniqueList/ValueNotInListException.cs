using System;

namespace UniqueList
{
    [Serializable]
    public class ValueNotInListException : Exception
    {
        public ValueNotInListException() { }

        public ValueNotInListException(string message)
            : base(message) { }
    }
}
