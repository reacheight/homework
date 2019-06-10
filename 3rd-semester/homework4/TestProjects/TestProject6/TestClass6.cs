using System;
using MyNUnit.Attributes;

namespace TestProject6
{
    public class TestClass6
    {
        [Test]
        public void PassedTest1()
        {
        }

        [Test]
        public void PassedTest2()
        {
        }

        [Test]
        public void FailedTest1()
        {
            throw new Exception();
        }

        [Test]
        public void FailedTest2()
        {
            throw new NullReferenceException();
        }
    }
}