using System.Threading;
using MyNUnit;
using Shouldly;

namespace Tests
{
    public class TestClass
    {
        [Test]
        public void TestMethod()
        {
            true.ShouldBe(true);
        }
        
        [Test]
        public void LongTestMethod()
        {
            Thread.Sleep(3000);
        }
        
        [Test]
        public void TestMethod2()
        {
            Thread.Sleep(3000);
        }
    }
}