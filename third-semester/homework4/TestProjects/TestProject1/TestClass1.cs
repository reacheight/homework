using MyNUnit.Attributes;

namespace TestProject1
{
    public class TestClass1
    {
        public static bool[] Executed = {false, false, false};

        [Test]
        public void Test1()
        {
            Executed[0] = true;
        }

        [Test]
        public void Test2()
        {
            Executed[1] = true;
        }

        [Test]
        public void Test3()
        {
            Executed[2] = true;
        }
    }
}