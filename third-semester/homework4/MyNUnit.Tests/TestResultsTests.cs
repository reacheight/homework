using System.Linq;
using NUnit.Framework;
using Shouldly;
using TestProject6;

namespace MyNUnit.Tests
{
    public class TestResultsTests
    {
        [Test]
        public void SimpleSuccessedAndFailedTestResultsAreRight()
        {
            TestSystem.RunTests("../../../../TestProjects/TestProject6/bin");
            
            TestSystem.Successed.Count.ShouldBe(2);
            TestSystem.Failed.Count.ShouldBe(2);
            TestSystem.Successed.ShouldContain("TestProject6.TestClass6.PassedTest1");
            TestSystem.Successed.ShouldContain("TestProject6.TestClass6.PassedTest2");
            TestSystem.Failed.ShouldContain("TestProject6.TestClass6.FailedTest1");
            TestSystem.Failed.ShouldContain("TestProject6.TestClass6.FailedTest2");
        }
    }
}