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
            RunAttributeMethods<BeforeClassAttribute>(type);
            RunAttributeMethods<TestAttribute>(type);
            RunAttributeMethods<AfterClassAttribute>(type);
        }
        
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

        private static void RunHelpMethod(MethodInfo methodInfo)
        {
            ValidateMethod(methodInfo);
            var instance = CreateInstance(methodInfo.DeclaringType);
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