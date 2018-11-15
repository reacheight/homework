using System.Threading;
using MyNUnit.Attributes;

namespace TestProject3
{
    public class TestClass3
    {
        public static int[] Threads { get; set; } = {0, 0};

        [Test]
        public void Test1()
        {
            Threads[0] = Thread.CurrentThread.ManagedThreadId;
            Thread.Sleep(1000);
        }

        [Test]
        public void Test2()
        {
            Threads[1] = Thread.CurrentThread.ManagedThreadId;
            Thread.Sleep(1000);
        }
    }
}