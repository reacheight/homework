using MyNUnit.Attributes;

namespace TestProject5
{
    public class TestClass5
    {
        public static int[] BeforeClassExecuted = {0, 0};
        public static int[] AfterClassExectured = {0};

        [BeforeClass]
        public void Before1()
        {
            BeforeClassExecuted[0]++;
        }

        [BeforeClass]
        public void Before2()
        {
            BeforeClassExecuted[1]++;
        }

        [Test]
        public void Test1()
        {
        }
        
        [Test]
        public void Test2()
        {
        }

        [AfterClass]
        public void After()
        {
            AfterClassExectured[0]++;
        }
    }
}