using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MyNUnit.Attributes;

namespace MyNUnit
{
    /// <summary>
    /// Class that implements test system
    /// </summary>
    public static class TestSystem
    {
        /// <summary>
        /// Executes tests in all classes of all assemblies stored by the given path
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
        /// Executes tests in the given class
        /// </summary>
        /// <param name="type">type of the given class</param>
        private static void RunTestMethods(Type type)
        {
            RunAttributeMethods<BeforeClassAttribute>(type);
            RunAttributeMethods<TestAttribute>(type);
            RunAttributeMethods<AfterClassAttribute>(type);
        }
        
        /// <summary>
        /// Executes all methods of the given class with the given attribute in parallel
        /// </summary>
        /// <param name="type">type of the class whose methods are going to be executed</param>
        /// <typeparam name="T">given attribute</typeparam>
        private static void RunAttributeMethods<T>(Type type)
            where T : Attribute
        {
            var runMethod = typeof(T) == typeof(TestAttribute)
                ? (Action<MethodInfo>) RunTestMethod
                : RunHelpMethod;

            var tasks = type.GetTypeInfo().DeclaredMethods
                .Where(mi => Attribute.IsDefined(mi, typeof(T)))
                .Select(mi => new Task(() => runMethod(mi)))
                .ToList();
            
            tasks.ForEach(task => task.Start());
            Task.WaitAll(tasks.ToArray());
        }

        /// <summary>
        /// Executes test method 
        /// </summary>
        /// <param name="methodInfo">method info of the test method to be executed</param>
        private static void RunTestMethod(MethodInfo methodInfo)
        {
            var attribute = Attribute.GetCustomAttribute(methodInfo, typeof(TestAttribute)) as TestAttribute;
            if (attribute.Ignore != null)
            {
                TestLogger.LogIgnore(methodInfo, attribute.Ignore);
                return;
            }
            
            ValidateMethod(methodInfo);
            RunAttributeMethods<BeforeAttribute>(methodInfo.DeclaringType);
            
            var instance = CreateInstance(methodInfo.DeclaringType);
            var watch = Stopwatch.StartNew();
            try
            {
                methodInfo.Invoke(instance, null);
                watch.Stop();
                TestLogger.Log(methodInfo, watch.ElapsedMilliseconds, attribute.Excpected == null);
            }
            catch (Exception exception)
            {
                watch.Stop();
                TestLogger.Log(methodInfo, watch.ElapsedMilliseconds,
                    attribute.Excpected != null && exception.InnerException.GetType() == attribute.Excpected);
            }

            RunAttributeMethods<AfterAttribute>(methodInfo.DeclaringType);
        }

        /// <summary>
        /// Executes simple method
        /// </summary>
        /// <param name="methodInfo">method info of the method to be executed</param>
        private static void RunHelpMethod(MethodInfo methodInfo)
        {
            ValidateMethod(methodInfo);
            var instance = CreateInstance(methodInfo.DeclaringType);
            methodInfo.Invoke(instance, null);
        }

        /// <summary>
        /// Validates that the given method is executable
        /// </summary>
        /// <param name="methodInfo">method info of the method to be validated</param>
        /// <exception cref="Exception">throws exception if the method has any parameters
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
        /// Creates instance of the given type
        /// </summary>
        /// <param name="type">given type</param>
        /// <returns>instance of the given type</returns>
        /// <exception cref="Exception">throws exception if the given type
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