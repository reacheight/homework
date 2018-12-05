namespace MyNUnit
{
    public class TestMethodResult
    {
        public TestMethodResult(string name, long executionTime, TestMethodStatus status, string ignoreMessage = null)
        {
            Name = name;
            ExecutionTime = executionTime;
            Status = status;
            IgnoreMessage = ignoreMessage;
        }
        
        public string Name { get; }
        public TestMethodStatus Status { get; }
        public long ExecutionTime { get; }
        public string IgnoreMessage { get; }
    }
}