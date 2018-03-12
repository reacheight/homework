using System;

namespace HashTable
{
    [Serializable]
    public class ValueNotInHashTableException : Exception
    {
        public ValueNotInHashTableException() { }
        public ValueNotInHashTableException(string message) : base(message) { }
        public ValueNotInHashTableException(string message, Exception inner) : base(message, inner) { }
        protected ValueNotInHashTableException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
