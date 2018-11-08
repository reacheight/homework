using System.Linq;
using System.Threading;
using NUnit.Framework;
using Shouldly;
using TestProject1;
using TestProject2;
using TestProject3;

namespace MyNUnit.Tests
{
    public class TestSystemTests
    {
        [Test]
        public void TestMethodsRun()
        {
            TestClass1.Executed.ShouldAllBe(flag => !flag);
            TestSystem.RunTests("../../../../TestProjects/TestProject1/bin");
            TestClass1.Executed.ShouldAllBe(flag => flag);
        }

        [Test]
        public void TestMethodRunOnce()
        {
            TestClass2.Executed.ShouldAllBe(count => count == 0);
            TestSystem.RunTests("../../../../TestProjects/TestProject2/bin");
            TestClass2.Executed.ShouldAllBe(count => count == 1);
        }

        [Test]
        public void TestMethodsRunInParallel()
        {
            TestClass3.Executed.ShouldAllBe(flag => !flag);
            
            TestSystem.RunTests("../../../../TestProjects/TestProject3/bin");
            Thread.Sleep(1300);
            
            TestClass3.Executed.ShouldAllBe(flag => flag);
        }
    }
}