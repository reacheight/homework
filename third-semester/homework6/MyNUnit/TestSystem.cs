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
        private static ConcurrentBag<TestMethodResult> _succeeded;
        private static ConcurrentBag<TestMethodResult> _failed;
        private static ConcurrentBag<TestMethodResult> _ignored;

        /// <summary>
        /// Gets list of names of all succeeded tests from last run
        /// </summary>
        public static IReadOnlyCollection<TestMethodResult> Succeeded => _succeeded;
        
        /// <summary>
        /// Gets list of names of all failed tests from last run
        /// </summary>
        public static IReadOnlyCollection<TestMethodResult> Failed => _failed;
        
        /// <summary>
        /// Gets list of names of all ignored tests from last run
        /// </summary>
        public static IReadOnlyCollection<TestMethodResult> Ignored => _ignored;        

        /// <summary>
        /// Executes tests in all classes of all assemblies stored by the given path
        /// </summary>
        /// <param name="path">given path</param>
        public static void RunTests(string path)
        {
            InitStaticFields();
            
            var types = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories)
                .Select(Assembly.LoadFrom)
                .ToHashSet()
                .SelectMany(a => a.ExportedTypes);

            Parallel.ForEach(types, RunTestMethods);
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
        /// <param name="instance">instance on which methods are going to be executed</param>
        /// <typeparam name="T">given attribute</typeparam>
        private static void RunAttributeMethods<T>(Type type, object instance = null)
            where T : Attribute
        {
            var runMethod = typeof(T) == typeof(TestAttribute)
                ? (Action<MethodInfo>) RunTestMethod
                : mi => RunHelpMethod(mi, instance);

            var attributeMethods = type.GetTypeInfo().DeclaredMethods
                .Where(mi => Attribute.IsDefined(mi, typeof(T)));

            Parallel.ForEach(attributeMethods, runMethod);
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
                _ignored.Add(new TestMethodResult(MethodName(methodInfo), 0, TestMethodStatus.Ignored, attribute.Ignore));
                return;
            }
            
            ValidateMethod(methodInfo);
            
            var instance = CreateInstance(methodInfo.DeclaringType);
            RunAttributeMethods<BeforeAttribute>(methodInfo.DeclaringType, instance);

            var succeeded = false;
            var watch = Stopwatch.StartNew();
            try
            {
                methodInfo.Invoke(instance, null);
                succeeded = attribute.Expected == null;
            }
            catch (Exception exception)
            {
                succeeded = attribute.Expected != null && exception.InnerException.GetType() == attribute.Expected;
            }
            finally
            {
                watch.Stop();
            }
            
            if (succeeded)
            {
                _succeeded.Add(new TestMethodResult(MethodName(methodInfo), watch.ElapsedMilliseconds, TestMethodStatus.Succeeded));
            }
            else
            {
                _failed.Add(new TestMethodResult(MethodName(methodInfo), watch.ElapsedMilliseconds, TestMethodStatus.Failed));
            }
            
            RunAttributeMethods<AfterAttribute>(methodInfo.DeclaringType, instance);
            
            string MethodName(MethodInfo mi) => $"{mi.DeclaringType}.{mi.Name}";
        }

        /// <summary>
        /// Executes simple method
        /// </summary>
        /// <param name="methodInfo">method info of the method to be executed</param>
        /// <param name="instance">instance on which method is going to be executed</param>
        private static void RunHelpMethod(MethodInfo methodInfo, object instance = null)
        {
            ValidateMethod(methodInfo);
            instance = instance ?? CreateInstance(methodInfo.DeclaringType);
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
        
        /// <summary>
        /// Initialises static fields
        /// </summary>
        private static void InitStaticFields()
        {
            _succeeded = new ConcurrentBag<TestMethodResult>();
            _failed = new ConcurrentBag<TestMethodResult>();
            _ignored = new ConcurrentBag<TestMethodResult>();
        }
    }
}