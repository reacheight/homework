using System;

namespace UniqueList
{
    /// <summary>
    /// Class that implements the exception thrown when trying to insert into the list a value that is already in it
    /// </summary>
    [Serializable]
    public class ValueAlreadyInListException : Exception
    {
        public ValueAlreadyInListException() { }

        public ValueAlreadyInListException(string message)
            : base(message) { }
    }
}
