using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MyNUnit
{
    public static class TestSystem
    {
        public static void RunTests(string path)
        {
            var assemblies = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories)
                .Select(Assembly.LoadFrom).ToList();

            var tasks = new List<Task>();
            
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.ExportedTypes)
                {
                    var task = new Task(() => RunTestMethods(type));
                    task.Start();
                    tasks.Add(task);
                }
            }

            Task.WhenAll(tasks).Wait();
        }

        private static void RunTestMethods(Type type)
        {
            RunHelperMethods(type, typeof(BeforeClassAttribute));
            
            var tasks = new List<Task>();
            
            foreach (var methodInfo in type.GetTypeInfo().DeclaredMethods)
            {
                if (Attribute.GetCustomAttributes(methodInfo)
                    .Any(attr => attr.GetType() == typeof(TestAttribute)))
                {
                    var task = new Task(() => RunTestMethod(methodInfo));
                    task.Start();
                    tasks.Add(task);
                }
            }

            Task.WhenAll(tasks).Wait();
            RunHelperMethods(type, typeof(AfterClassAttribute));
        }

        private static void RunTestMethod(MethodInfo methodInfo)
        {
            ValidateMethod(methodInfo);

            RunHelperMethods(methodInfo.DeclaringType, typeof(BeforeAttribute));
            
            var instance = CreateInstance(methodInfo);

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
            
            RunHelperMethods(methodInfo.DeclaringType, typeof(AfterAttribute));
        }

        private static void RunHelperMethods(Type type, Type helperAttributeType)
        {
            var tasks = new List<Task>();

            foreach (var methodInfo in type.GetTypeInfo().DeclaredMethods)
            {
                if (Attribute.GetCustomAttributes(methodInfo)
                    .Any(attr => attr.GetType() == helperAttributeType))
                {
                    var task = new Task(() => RunHelperMethod(methodInfo));
                    task.Start();
                    tasks.Add(task);
                }
            }

            Task.WaitAll(tasks.ToArray());
        }

        private static void RunHelperMethod(MethodInfo methodInfo)
        {
            ValidateMethod(methodInfo);
            var instance = CreateInstance(methodInfo);
            methodInfo.Invoke(instance, null);
        }

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

        private static object CreateInstance(MethodInfo methodInfo)
        {
            var constructor = methodInfo.DeclaringType.GetConstructor(Type.EmptyTypes);
            if (constructor == null)
            {
                throw new Exception("Test class should have parameterless constructor.");
            }

            return constructor.Invoke(null);
        }
    }
}