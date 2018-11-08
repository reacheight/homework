using System;
using System.Runtime.Serialization;

namespace MyNUnit
{
    /// <summary>
    /// Class that implements invalid constructor exception.
    /// Thrown if test class has no parameterless constructor.
    /// </summary>
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