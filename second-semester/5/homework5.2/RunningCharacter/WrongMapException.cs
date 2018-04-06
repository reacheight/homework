namespace RunningCharacter
{
    using System;

    /// <summary>
    /// Class that implements exception that throwing if
    /// second character of second line of line of map isn't space character
    /// </summary>
    [Serializable]
    public class WrongMapException : Exception
    {
        public WrongMapException() { }
        public WrongMapException(string message) : base(message) { }
        public WrongMapException(string message, Exception inner) : base(message, inner) { }
        protected WrongMapException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
