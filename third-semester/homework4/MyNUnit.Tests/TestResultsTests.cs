using NUnit.Framework;
using Shouldly;
using TestProject7;

namespace MyNUnit.Tests
{
    public class TestResultsTests
    {
        [Test]
        public void SimpleTestResultsAreRight()
        {
            TestSystem.RunTests("../../../../TestProjects/TestProject6/bin");
            
            TestSystem.Successed.Count.ShouldBe(2);
            TestSystem.Failed.Count.ShouldBe(2);
            
            TestSystem.Successed.ShouldContain("TestProject6.TestClass6.PassedTest1");
            TestSystem.Successed.ShouldContain("TestProject6.TestClass6.PassedTest2");
            
            TestSystem.Failed.ShouldContain("TestProject6.TestClass6.FailedTest1");
            TestSystem.Failed.ShouldContain("TestProject6.TestClass6.FailedTest2");
        }

        [Test]
        public void ExcpectedWorksRight()
        {
            TestSystem.RunTests("../../../../TestProjects/TestProject7/bin");
            
            TestSystem.Successed.ShouldContain("TestProject7.TestClass7.ExceptionTest");
            TestSystem.Failed.ShouldContain("TestProject7.TestClass7.FailedExceptionTest");
        }

        [Test]
        public void IgnoreWorksRight()
        {
            TestSystem.RunTests("../../../../TestProjects/TestProject7/bin");

            TestClass7.Ignored = true;
            TestSystem.Ignored.Count.ShouldBe(1);
            TestSystem.Ignored.ShouldContain("TestProject7.TestClass7.IgnoredTest");
        }
    }
}