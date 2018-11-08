using System;
using System.Threading;
using MyNUnit.Attributes;

namespace TestProject3
{
    public class TestClass3
    {
        public static bool[] Executed = {false, false, false};

        [Test]
        public void Test1()
        {
            Thread.Sleep(1000);
            Executed[0] = true;
        }

        [Test]
        public void Test2()
        {
            Thread.Sleep(1000);
            Executed[1] = true;
        }

        [Test]
        public void Test3()
        {
            Thread.Sleep(1000);
            Executed[2] = true;
        }
    }
}