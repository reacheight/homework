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
        /// <param name="ellapsedMilliseconds">test method execution time in milliseconds</param>
        /// <param name="successed">gets whether test successed or failed</param>
        public static void Log(MethodInfo methodInfo, long ellapsedMilliseconds, bool successed)
        {
            Console.WriteLine($"Test method {MethodName(methodInfo)} {(successed ? "successed" : "failed")}.");
            Console.WriteLine($"Execution time: {ellapsedMilliseconds} ms.");
            Console.WriteLine();
        }
        
        public static void LogIgnore(MethodInfo methodInfo, string message)
        {
            Console.WriteLine($"Test method {MethodName(methodInfo)} ignored with message: {message}");
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