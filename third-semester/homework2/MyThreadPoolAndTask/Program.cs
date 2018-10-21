using System;
using System.Collections.Generic;
using System.Threading;

namespace MyThreadPoolAndTask
{
    public class Program
    {
        public static int Factorial(int n)
        {
            if (n < 0) throw new ArgumentOutOfRangeException();

            if (n <= 1) return 1;

            return Factorial(n - 1) * n;
        }
        
        public static void Main(string[] args)
        {
            var threadPool = new MyThreadPool(5);
            var tasks = new List<IMyTask<int>>();
            for (var i = 0; i < 10; ++i)
            {
                var j = i;
                tasks.Add(threadPool.QueueTask(() => Factorial(j)));
            }

            Thread.Sleep(1000);

            foreach (var task in tasks)
            {
                Console.WriteLine(task.Result);
            }

            tasks[0].ContinueWith((x) =>
            {
                Console.WriteLine("New task is evaluating!");
                return x * x;
            });
            
            threadPool.Shutdown();
        }
    }
}
