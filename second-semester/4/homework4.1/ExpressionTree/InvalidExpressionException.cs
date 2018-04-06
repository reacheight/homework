namespace ExpressionTree
{
    using System;

    /// <summary>
    /// Class that implements exception that throwing if parsing expression is invalid
    /// </summary>
    [Serializable]
    public class InvalidExpressionException : Exception
    {
        public InvalidExpressionException() { }
        public InvalidExpressionException(string message) : base(message) { }
        public InvalidExpressionException(string message, Exception inner) : base(message, inner) { }
        protected InvalidExpressionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
