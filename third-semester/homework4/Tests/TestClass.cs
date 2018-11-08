using System;
using System.Threading;
using MyNUnit;
using Shouldly;

namespace Tests
{
    public class TestClass
    {
        [BeforeClass]
        public void BeforeClass()
        {
            Console.WriteLine("before class");
        }

        [AfterClass]
        public void AfterClass()
        {
            Console.WriteLine("after class");
        }
        [Before]
        public void Before()
        {
            Console.WriteLine("before");
        }
        
        [After]
        public void After()
        {
            Console.WriteLine("after");
        }
        
        [Test]
        public void TestMethod()
        {
            true.ShouldBe(true);
        }
        
        [Test]
        public void LongTestMethod()
        {
            Thread.Sleep(2000);
            true.ShouldBe(false);
        }
        
        [Test]
        public void TestMethod2()
        {
            Thread.Sleep(3000);
        }
    }
}