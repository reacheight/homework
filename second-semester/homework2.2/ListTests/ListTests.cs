using Microsoft.VisualStudio.TestTools.UnitTesting;
using List;

namespace List.Tests
{
    [TestClass()]
    public class ListTests
    {
        private List<int> list;

        [TestInitialize]
        public void Init()
        {
            list = new List<int>();
        }

        [TestMethod()]
        public void IsEmptyReturnsTrueIfListIsEmpty()
        {
            Assert.AreEqual(true, list.IsEmpty());
        }

        [TestMethod()]
        public void InsertTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EraseTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ValueTest()
        {
            Assert.Fail();
        }
    }
}