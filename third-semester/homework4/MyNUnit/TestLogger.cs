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
        public static void LogSuccess(MethodInfo methodInfo)
        {
            Console.WriteLine($"{MethodString(methodInfo)} successed.");
        }

        public static void LogFail(MethodInfo methodInfo, Exception exception)
        {
            Console.WriteLine($"{MethodString(methodInfo)} failed with exception message: {exception.Message}");
        }
    }
}