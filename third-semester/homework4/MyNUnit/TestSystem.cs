using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        private static ConcurrentBag<string> _successed;
        private static ConcurrentBag<string> _failed;
        private static ConcurrentBag<string> _ignored;

        /// <summary>
        /// Gets list of names of all successed tests from last run
        /// </summary>
        public static IReadOnlyCollection<string> Successed => _successed;
        
        /// <summary>
        /// Gets list of names of all failed tests from last run
        /// </summary>
        public static IReadOnlyCollection<string> Failed => _failed;
        
        /// <summary>
        /// Gets list of names of all ignored tests from last run
        /// </summary>
        public static IReadOnlyCollection<string> Ignored => _ignored;        

        /// <summary>
        /// Executes tests in all classes of all assemblies stored by the given path
        /// </summary>
        /// <param name="path">given path</param>
        public static void RunTests(string path)
        {
            var assemblies = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories)
                .Select(Assembly.LoadFrom)
                .ToHashSet()
                .ToList();
            
            InitStaticFields();
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
                _ignored.Add($"{methodInfo.DeclaringType}.{methodInfo.Name}");
                return;
            }
            
            ValidateMethod(methodInfo);
            RunAttributeMethods<BeforeAttribute>(methodInfo.DeclaringType);

            bool successed;
            var instance = CreateInstance(methodInfo.DeclaringType);
            var watch = Stopwatch.StartNew();
            try
            {
                methodInfo.Invoke(instance, null);
                watch.Stop();
                successed = attribute.Excpected == null;
            }
            catch (Exception exception)
            {
                watch.Stop();
                successed = attribute.Excpected != null && exception.InnerException.GetType() == attribute.Excpected;
            }
            
            TestLogger.Log(methodInfo, watch.ElapsedMilliseconds, successed);
            
            if (successed)
            {
                _successed.Add($"{methodInfo.DeclaringType}.{methodInfo.Name}");
            }
            else
            {
                _failed.Add($"{methodInfo.DeclaringType}.{methodInfo.Name}");
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
        /// <exception cref="InvalidTestMethodException">throws exception if the method has any parameters
        /// or is not void</exception>
        private static void ValidateMethod(MethodInfo methodInfo)
        {
            if (methodInfo.GetParameters().Length != 0)
            {
                throw new InvalidTestMethodException("Test method should not have any parameters:" +
                                                     $"{methodInfo.DeclaringType}.{methodInfo.Name}");
            }

            if (methodInfo.ReturnType != typeof(void))
            {
                throw new InvalidTestMethodException("Test method should be void:" +
                                                     $"{methodInfo.DeclaringType}.{methodInfo.Name}");
            }
        }

        /// <summary>
        /// Creates instance of the given type
        /// </summary>
        /// <param name="type">given type</param>
        /// <returns>instance of the given type</returns>
        /// <exception cref="InvalidConstructorException">throws exception if the given type
        /// has no parameterless constructor</exception>
        private static object CreateInstance(Type type)
        {
            var constructor = type.GetConstructor(Type.EmptyTypes);
            if (constructor == null)
            {
                throw new InvalidConstructorException("Test class should have parameterless constructor:" +
                                                      type.Name);
            }

            return constructor.Invoke(null);
        }
        
        private static void InitStaticFields()
        {
            _successed = new ConcurrentBag<string>();
            _failed = new ConcurrentBag<string>();
            _ignored = new ConcurrentBag<string>();
        }
    }
}