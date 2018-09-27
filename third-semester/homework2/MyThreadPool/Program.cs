using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace MyThreadPool
{
    class Program
    {
        static void Main(string[] args)
        {
            var threadPool = new MyThreadPool(4);

            Func<int> getInteger = () => 5;
            var task = threadPool.QueueTask(getInteger);
            
            Console.WriteLine(task.Result);
            threadPool.Shutdown();
        }
    }
}