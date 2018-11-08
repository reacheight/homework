using System;
using System.Reflection;

namespace MyNUnit
{
    public static class TestLogger
    {
        private static string MethodString(MethodInfo methodInfo)
            => $"{methodInfo.DeclaringType}.{methodInfo.Name}";
        
        public static void LogSuccess(MethodInfo methodInfo, long ellapsedMilliseconds)
            => Log($"Test method {MethodString(methodInfo)} successed.", ellapsedMilliseconds);

        public static void LogFail(MethodInfo methodInfo, long ellapsedMilliseconds, Exception exception)
            => Log($"Test method {MethodString(methodInfo)} failed with exception message: {exception.Message}",
                ellapsedMilliseconds);

        private static void Log(string message, long ellapsedMilliseconds)
        {
            Console.WriteLine(message);
            Console.WriteLine($"Execution time: {ellapsedMilliseconds} ms.");
            Console.WriteLine();
        }
    }
}