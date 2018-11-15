using MyNUnit.Attributes;

namespace TestProject2
{
    public class TestClass2
    {
        public static int[] Executed = {0, 0, 0};
        
        [Test]
        public void Test1()
        {
            Executed[0]++;
        }

        [Test]
        public void Test2()
        {
            Executed[1]++;
        }

        [Test]
        public void Test3()
        {
            Executed[2]++;
        }
    }
}