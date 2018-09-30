using System;
using System.Threading;

namespace MyThreadPool
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var threadPool = new MyThreadPool(4);

            var count1 = 0;

            var task1 = threadPool.QueueTask(() =>
            {
                Thread.Sleep(2000);
                count1++;
                return 5;
            });

            var count2 = 0;
            var task2 = threadPool.QueueTask(() =>
            {
                Thread.Sleep(3000);
                count2++;
                return "sdf";
            });

            var task3 = threadPool.QueueTask(() => 123);
            var task4 = threadPool.QueueTask(() => "sdfsdfsdf");
            var task5 = threadPool.QueueTask(() =>
            {
                Thread.Sleep(5000);
                return 1231;
            });
            
            Console.WriteLine(task1.Result);
            Console.WriteLine(count1);
            
            Console.WriteLine(task2.Result);
            Console.WriteLine(count2);
            
            Console.WriteLine(task3.Result);
            Console.WriteLine(task4.Result);
            Console.WriteLine(task5.Result);
            
            threadPool.Shutdown();
        }
    }
}
