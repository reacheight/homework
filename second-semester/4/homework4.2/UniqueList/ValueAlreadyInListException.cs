using System;

namespace UniqueList
{
    [Serializable]
    public class ValueAlreadyInListException : Exception
    {
        public ValueAlreadyInListException() { }

        public ValueAlreadyInListException(string message)
            : base(message) { }
    }
}
