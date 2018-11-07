using System;
using System.Diagnostics;
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

            var tasks = assemblies.SelectMany(a => a.ExportedTypes)
                .Select(type => new Task(() => RunTestMethods(type)))
                .ToList();
            
            tasks.ForEach(task => task.Start());
            Task.WaitAll(tasks.ToArray());
        }

        private static void RunTestMethods(Type type)
        {
            RunHelperMethods(type, typeof(BeforeClassAttribute));
            
            var tasks = type.GetTypeInfo().DeclaredMethods
                .Where(IsTestMethod)
                .Select(mi => new Task(() => RunTestMethod(mi)))
                .ToList();
            
            tasks.ForEach(task => task.Start());
            Task.WaitAll(tasks.ToArray());
            
            RunHelperMethods(type, typeof(AfterClassAttribute));

            bool IsTestMethod(MethodInfo methodInfo)
                => Attribute.GetCustomAttributes(methodInfo).
                    Any(attr => attr.GetType() == typeof(TestAttribute));
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
            var tasks = type.GetTypeInfo().DeclaredMethods
                .Where(IsNeededMethod)
                .Select(mi => new Task(() => RunHelperMethod(mi)))
                .ToList();
            
            tasks.ForEach(task => task.Start());
            Task.WaitAll(tasks.ToArray());

            bool IsNeededMethod(MethodInfo methodInfo)
                => Attribute.GetCustomAttributes(methodInfo)
                    .Any(attr => attr.GetType() == helperAttributeType);
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