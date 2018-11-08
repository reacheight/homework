using System;
using System.Reflection;

namespace MyNUnit
{
    /// <summary>
    /// Class that implements logger for test system
    /// </summary>
    public static class TestLogger
    {
        /// <summary>
        /// Gets full name of given method
        /// </summary>
        /// <param name="methodInfo">method info of given method</param>
        /// <returns>full name of given method</returns>
        private static string MethodString(MethodInfo methodInfo)
            => $"{methodInfo.DeclaringType}.{methodInfo.Name}";
        
        /// <summary>
        /// Logs result of successful test execution
        /// </summary>
        /// <param name="methodInfo">method info of executed test mehtod</param>
        /// <param name="ellapsedMilliseconds">test method execution time in milliseconds</param>
        public static void LogSuccess(MethodInfo methodInfo, long ellapsedMilliseconds)
            => Log($"Test method {MethodString(methodInfo)} successed.", ellapsedMilliseconds);

        /// <summary>
        /// Logs result of failed test execution
        /// </summary>
        /// <param name="methodInfo">method info of executed test</param>
        /// <param name="ellapsedMilliseconds">test method execution time in milliseconds</param>
        /// <param name="exception">exception thrown by test method</param>
        public static void LogFail(MethodInfo methodInfo, long ellapsedMilliseconds, Exception exception)
            => Log($"Test method {MethodString(methodInfo)} failed with exception message: {exception.Message}",
                ellapsedMilliseconds);

        /// <summary>
        /// Prints given message and execution time
        /// </summary>
        /// <param name="message">given message</param>
        /// <param name="ellapsedMilliseconds">test method execution time in milliseconds</param>
        private static void Log(string message, long ellapsedMilliseconds)
        {
            Console.WriteLine(message);
            Console.WriteLine($"Execution time: {ellapsedMilliseconds} ms.");
            Console.WriteLine();
        }
    }
}