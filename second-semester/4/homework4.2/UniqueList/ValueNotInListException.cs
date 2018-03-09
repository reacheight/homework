using System;

namespace UniqueList
{
    /// <summary>
    /// Class that implements the exception thrown when trying to remove invalid value from the list
    /// </summary>
    [Serializable]
    public class ValueNotInListException : Exception
    {
        public ValueNotInListException() { }

        public ValueNotInListException(string message)
            : base(message) { }
    }
}
