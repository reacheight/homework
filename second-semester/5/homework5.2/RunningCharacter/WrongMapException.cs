namespace RunningCharacter
{
    using System;

    /// <summary>
    /// Class that implements exception that throwing if map of the game isn't valid
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
