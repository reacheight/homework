using System;
using MyNUnit.Attributes;

namespace TestProject7
{
    public class TestClass7
    {
        public static bool Ignored { get; set; } = true;
        
        [Test(Excpected = typeof(InvalidOperationException))]
        public void ExceptionTest()
        {
            throw new InvalidOperationException();
        }
        
        [Test(Excpected = typeof(InvalidOperationException))]
        public void FailedExceptionTest()
        {
            throw new NullReferenceException();
        }

        [Test(Ignore = "ignored")]
        public void IgnoredTest()
        {
            Ignored = false;
        }
    }
}