using System;
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
                }
            }

            Task.WhenAll(tasks).Wait();
        }

        private static void RunTestMethod(MethodInfo methodInfo)
        {
            ValidateTestMethod(methodInfo);

            var constructor = methodInfo.DeclaringType.GetConstructor(Type.EmptyTypes);
            if (constructor == null)
            {
                throw new Exception("Test class should have parameterless constructor.");
            }

            try
            {
                methodInfo.Invoke(constructor.Invoke(null), null);
                TestLogger.LogSuccess(methodInfo);
            }
            catch (Exception exception)
            {
                TestLogger.LogFail(methodInfo, exception);
            }
        }

        private static void ValidateTestMethod(MethodInfo methodInfo)
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
    }
}