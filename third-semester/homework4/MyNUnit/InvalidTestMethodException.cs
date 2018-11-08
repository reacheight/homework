using System;
using System.Runtime.Serialization;

namespace MyNUnit
{
    [Serializable]
    public class InvalidTestMethodException : Exception
    {
        public InvalidTestMethodException()
        {
        }

        public InvalidTestMethodException(string message) : base(message)
        {
        }

        public InvalidTestMethodException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidTestMethodException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}