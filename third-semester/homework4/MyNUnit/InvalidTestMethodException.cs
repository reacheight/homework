using System;
using System.Runtime.Serialization;

namespace MyNUnit
{
    /// <summary>
    /// Class that implements invalid test method exception.
    /// Thrown if method has any parameters and is not void.
    /// </summary>
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