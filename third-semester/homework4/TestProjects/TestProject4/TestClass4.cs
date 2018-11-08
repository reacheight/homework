using System;
using MyNUnit.Attributes;

namespace TestProject4
{
    public class TestClass4
    {
        public static int[] BeforeExecuted = {0, 0};
        public static int[] AfterExectured = {0};

        [Before]
        public void Before1()
        {
            BeforeExecuted[0]++;
        }

        [Before]
        public void Before2()
        {
            BeforeExecuted[1]++;
        }

        [Test]
        public void Test1()
        {
        }
        
        [Test]
        public void Test2()
        {
        }

        [After]
        public void After()
        {
            AfterExectured[0]++;
        }
    }
}