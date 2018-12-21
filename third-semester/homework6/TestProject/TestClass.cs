using MyNUnit.Attributes;
using Shouldly;
using System;

namespace TestProject
{
    public class TestClass
    {
        [Test]
        public void PassingTestMethod()
        {
            true.ShouldBeTrue();
        }

        [Test]
        public void FailingTestMethod()
        {
            5.ShouldBe(6);
        }

        [Test(Ignore = "just for fun")]
        public void IgnoredMethod()
        {

        }
    }
}
