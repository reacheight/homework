using System;
using System.Threading;

namespace MyThreadPool
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var threadPool = new MyThreadPool(4);

            var count = 0;

            var task = threadPool.QueueTask(() =>
            {
                Thread.Sleep(2000);
                count++;
                return 5;
            });
            
            Console.WriteLine(task.Result);
            Console.WriteLine(count);
            
            threadPool.Shutdown();
        }
    }
}
