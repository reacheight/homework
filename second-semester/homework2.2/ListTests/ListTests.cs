using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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

        [TestMethod]
        public void IsEmptyReturnsTrueIfListIsEmpty()
        {
            Assert.AreEqual(true, list.IsEmpty());
        }

        [TestMethod]
        public void InsertRandomNumberInHeadWillNotCrash()
        {
            int randomInteger = new Random().Next();
            list.Insert(randomInteger, 0);
        }

        [TestMethod]
        public void InsertSomeRandomNumbersInHeadWillNotCrash()
        {
            var random = new Random();
            var numberOfElements = 10;
            
            for (int i = 0; i < numberOfElements; ++i)
            {
                list.Insert(random.Next(), 0);
            }
        }

        [TestMethod]
        public void InsertNotOnlyInHeadWillNotCrash()
        {
            var random = new Random();
            var numberOfElements = 10;

            for (int i = 0; i < numberOfElements; ++i)
            {
                list.Insert(random.Next(), i);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InsertFirstNotInHeadWillThrowMyException()
        {
            var random = new Random();
            int randomInteger = random.Next();
            int position = random.Next(1, 11);
            list.Insert(randomInteger, position);
        }
    }
}