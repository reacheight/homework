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
        /// Logs result of test method execution
        /// </summary>
        /// <param name="methodInfo">executed test method</param>
        /// <param name="elapsedMilliseconds">test method execution time in milliseconds</param>
        /// <param name="succeeded">gets whether test succeeded or failed</param>
        public static void Log(MethodInfo methodInfo, long elapsedMilliseconds, bool succeeded)
        {
            Console.WriteLine($"Test method {MethodName(methodInfo)} {(succeeded ? "succeeded" : "failed")}.\n" +
                              $"Execution time: {elapsedMilliseconds} ms.\n");
        }
        
        /// <summary>
        /// Logs about ignoration of test method
        /// </summary>
        /// <param name="methodInfo">given test method</param>
        /// <param name="message">ignore message</param>
        public static void LogIgnore(MethodInfo methodInfo, string message)
        {
            Console.WriteLine($"Test method {MethodName(methodInfo)} ignored with message: {message}.\n");
        }
        
        /// <summary>
        /// Gets full name of given method
        /// </summary>
        /// <param name="methodInfo">method info of given method</param>
        /// <returns>full name of given method</returns>
        private static string MethodName(MethodInfo methodInfo)
            => $"{methodInfo.DeclaringType}.{methodInfo.Name}";
    }
}