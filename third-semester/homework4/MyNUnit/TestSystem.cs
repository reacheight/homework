using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MyNUnit
{
    /// <summary>
    /// Class that implements test system
    /// </summary>
    public static class TestSystem
    {
        /// <summary>
        /// Executes tests in all classes of all assemblies stored by given path
        /// </summary>
        /// <param name="path">given path</param>
        public static void RunTests(string path)
        {
            var assemblies = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories)
                .Select(Assembly.LoadFrom).ToList();

            var tasks = assemblies.SelectMany(a => a.ExportedTypes)
                .Select(type => new Task(() => RunTestMethods(type)))
                .ToList();
            
            tasks.ForEach(task => task.Start());
            Task.WaitAll(tasks.ToArray());
        }

        /// <summary>
        /// Executes tests in given class
        /// </summary>
        /// <param name="type">type of given class</param>
        private static void RunTestMethods(Type type)
        {
            RunAttributeMethods<BeforeClassAttribute>(type);
            RunAttributeMethods<TestAttribute>(type);
            RunAttributeMethods<AfterClassAttribute>(type);
        }
        
        /// <summary>
        /// Executes all methods of given class with given attribute in parallel
        /// </summary>
        /// <param name="type">type of class whose methods are going to be executed</param>
        /// <typeparam name="T">given attribute</typeparam>
        private static void RunAttributeMethods<T>(Type type)
            where T : Attribute
        {
            var runMethod = typeof(T) == typeof(TestAttribute)
                ? (Action<MethodInfo>) RunTestMethod
                : RunHelpMethod;

            var tasks = type.GetTypeInfo().DeclaredMethods
                .Where(IsAttributeMethod)
                .Select(mi => new Task(() => runMethod(mi)))
                .ToList();
            
            tasks.ForEach(task => task.Start());
            Task.WaitAll(tasks.ToArray());

            bool IsAttributeMethod(MethodInfo methodInfo)
                => Attribute.GetCustomAttributes(methodInfo)
                    .Any(attr => attr.GetType() == typeof(T));
        }

        /// <summary>
        /// Executes test method 
        /// </summary>
        /// <param name="methodInfo">method info of test method to be executed</param>
        private static void RunTestMethod(MethodInfo methodInfo)
        {
            ValidateMethod(methodInfo);
            RunAttributeMethods<BeforeAttribute>(methodInfo.DeclaringType);
            
            var instance = CreateInstance(methodInfo.DeclaringType);
            var watch = Stopwatch.StartNew();
            try
            {
                methodInfo.Invoke(instance, null);
                watch.Stop();
                TestLogger.LogSuccess(methodInfo, watch.ElapsedMilliseconds);
            }
            catch (Exception exception)
            {
                watch.Stop();
                TestLogger.LogFail(methodInfo, watch.ElapsedMilliseconds, exception);
            }
            
            RunAttributeMethods<AfterAttribute>(methodInfo.DeclaringType);
        }

        /// <summary>
        /// Executes simple method
        /// </summary>
        /// <param name="methodInfo">method info of method to be executed</param>
        private static void RunHelpMethod(MethodInfo methodInfo)
        {
            ValidateMethod(methodInfo);
            var instance = CreateInstance(methodInfo.DeclaringType);
            methodInfo.Invoke(instance, null);
        }

        /// <summary>
        /// Validates that given method is executable
        /// </summary>
        /// <param name="methodInfo">method info of method to be validated</param>
        /// <exception cref="Exception">throws exception if method has any parameters
        /// or is not void</exception>
        private static void ValidateMethod(MethodInfo methodInfo)
        {
            if (methodInfo.GetParameters().Length != 0)
            {
                throw new Exception("Test method should not have any parameters.");
            }

            if (methodInfo.ReturnType != typeof(void))
            {
                throw new Exception("Test method should be void.");
            }
        }

        /// <summary>
        /// Creates instance of given type
        /// </summary>
        /// <param name="type">given type</param>
        /// <returns>instance of given type</returns>
        /// <exception cref="Exception">throws exception if given type
        /// has no parameterless constructor</exception>
        private static object CreateInstance(Type type)
        {
            var constructor = type.GetConstructor(Type.EmptyTypes);
            if (constructor == null)
            {
                throw new Exception("Test class should have parameterless constructor.");
            }

            return constructor.Invoke(null);
        }
    }
}