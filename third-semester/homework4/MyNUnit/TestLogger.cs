using System;
using System.Reflection;

namespace MyNUnit
{
    public static class TestLogger
    {
        private static string MethodString(MethodInfo methodInfo)
        {
            return $"{methodInfo.DeclaringType}.{methodInfo.Name}";
        }
        public static void LogSuccess(MethodInfo methodInfo, long ellapsedMilliseconds)
        {
            Console.WriteLine($"Test method {MethodString(methodInfo)} successed.");
            Console.WriteLine($"Execution time: {ellapsedMilliseconds} ms.");
            Console.WriteLine();
        }

        public static void LogFail(MethodInfo methodInfo, long ellapsedMilliseconds, Exception exception)
        {
            Console.WriteLine($"Test method {MethodString(methodInfo)} failed with exception message: {exception.Message}");
            Console.WriteLine($"Execution time: {ellapsedMilliseconds} ms.");
        }
    }
}