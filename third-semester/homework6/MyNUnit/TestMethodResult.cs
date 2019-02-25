namespace MyNUnit
{
    /// <summary>
    /// Represents method test result
    /// </summary>
    public class TestMethodResult
    {
        public TestMethodResult(string name, long executionTime, TestMethodStatus status, string ignoreMessage = null)
        {
            Name = name;
            ExecutionTime = executionTime;
            Status = status;
            IgnoreMessage = ignoreMessage;
        }
        
        /// <summary>
        /// Test name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Test execution status
        /// </summary>
        public TestMethodStatus Status { get; }

        /// <summary>
        /// Test execution time
        /// </summary>
        public long ExecutionTime { get; }

        /// <summary>
        /// Test ignore message
        /// </summary>
        public string IgnoreMessage { get; }
    }
}