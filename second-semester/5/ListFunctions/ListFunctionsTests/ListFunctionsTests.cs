using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ListFunctions.Tests
{
    [TestClass]
    public class ListFunctionsTests
    {
        private List<int> list;

        [TestInitialize]
        public void Init()
        {
            list = new List<int>();
            for (var i = 0; i < 10; ++i)
            {
                list.Add(i);
            }
        }

        [TestMethod]
        public void MapFirstTest()
        {
            var newList = ListFunctions.Map(list, i => i.ToString());

            foreach (var i in list)
            {
                Assert.AreEqual(i.ToString(), newList[i]);
            }
        }

        [TestMethod]
        public void MapSecondTest()
        {
            var newList = ListFunctions.Map(list, i => i + 1);

            foreach (var i in list)
            {
                Assert.AreEqual(i + 1, newList[i]);
            }
        }

        [TestMethod]
        public void FilterFirstTest()
        {
            var newList = ListFunctions.Filter(list, i => i % 2 == 1);

            Assert.AreEqual(5, newList.Count);

            foreach (var i in list)
            {
                if (i % 2 == 1)
                {
                    Assert.AreEqual(i, newList[(i - 1) / 2]);
                }
            }
        }

        [TestMethod]
        public void FilterSecondTest()
        {
            var newList = ListFunctions.Filter(list, i => false);

            Assert.AreEqual(0, newList.Count);
        }

        [TestMethod]
        public void FoldFirstTest()
        {
            var myResult = ListFunctions.Fold(list, 0, (acc, elem) => acc + elem);

            int result = 0;
            foreach (var i in list)
            {
                result += i;
            }

            Assert.AreEqual(result, myResult);
        }

        [TestMethod]
        public void FoldSecondTest()
        {
            var myResult = ListFunctions.Fold(list, 42, (acc, elem) => acc);

            Assert.AreEqual(42, myResult);
        }
    }
}