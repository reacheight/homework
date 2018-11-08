using System;
using System.Runtime.Serialization;

namespace MyNUnit
{
    [Serializable]
    public class InvalidConstructorException : Exception
    {
        public InvalidConstructorException()
        {
        }

        public InvalidConstructorException(string message) : base(message)
        {
        }

        public InvalidConstructorException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidConstructorException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}