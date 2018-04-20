namespace Calculator
{
    using System;

    /// <summary>
    /// Class that implements exception that is thrown if an arithmetic expression can not be evaluated
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
