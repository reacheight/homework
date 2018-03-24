namespace ExpressionTree
{
    using System;

    [Serializable]
    public class WrongCharacterException : Exception
    {
        public WrongCharacterException() { }
        public WrongCharacterException(string message) : base(message) { }
        public WrongCharacterException(string message, Exception inner) : base(message, inner) { }
        protected WrongCharacterException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
