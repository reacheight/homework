using System;
using System.Threading;

namespace MyThreadPoolAndTask
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var threadPool = new MyThreadPool(4);

            var count = 0;

            var task = threadPool.QueueTask(() =>
            {
                count++;
                Thread.Sleep(5000);
                return 5;
            });

            Thread.Sleep(6000);

            Console.WriteLine(count);
            threadPool.Shutdown();
        }
    }
}
