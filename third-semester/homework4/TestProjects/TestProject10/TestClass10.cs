using System;
using MyNUnit.Attributes;

namespace TestProject10
{
    public class TestClass10
    {
        public static int BeforeHash { get; private set; }
        public static int TestHash { get; private set; }
        public static int AfterHash { get; private set; }

        [Before]
        public void BeforeMethod()
        {
            BeforeHash = GetHashCode();
        }

        [Test]
        public void TestMethod()
        {
            TestHash = GetHashCode();
        }

        [After]
        public void AfterMethod()
        {
            AfterHash = GetHashCode();
        }
    }
}