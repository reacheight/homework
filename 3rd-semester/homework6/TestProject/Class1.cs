using MyNUnit.Attributes;
using System;

namespace TestProject
{
    public class Class1
    {
        [Test(Ignore = "because of!")]
        public void PassingMethod()
        {
        }

        [Test]
        public void FailingMethod()
        {
            throw new Exception();
        }

        [Test]
        public void ShouldNotCrash()
        { 
        }
    }
}
