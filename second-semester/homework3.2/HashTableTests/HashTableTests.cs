using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HashTable.Tests
{
    [TestClass()]
    public class HashTableTests
    {
        private HashTable<int> table;

        [TestInitialize]
        public void Init()
        {
            table = new HashTable<int>((int value) => value + 3);
        }

        [TestMethod]
        public void AddOneElementWillNotCrash()
        {
            table.Add(1);
        }

        [TestMethod]
        public void AddDifferentElementsWillNotCrash()
        {
            var numberOfElements = 10;
            for (var i = 0; i < numberOfElements; ++i)
            {
                table.Add(i);
            }
        }

        [TestMethod]
        public void AddSameElementsWillNotCrash()
        {
            var numberOfElements = 10;
            for (var i = 0; i < numberOfElements; ++i)
            {
                table.Add(1);
            }
        }

        [TestMethod]
        public void HashTableDoNotStoreSameElements()
        {
            var numberOfElements = 10;
            for (var i = 0; i < numberOfElements; ++i)
            {
                table.Add(1);
            }

            table.Erase(1);

            Assert.IsFalse(table.Contains(1));
        }

        [TestMethod]
        public void EraseOneElementWillNotCrash()
        {
            table.Add(1);
            table.Erase(1);
        }

        [TestMethod]
        public void EraseElementsWillNotCrash()
        {
            var numberOfElements = 10;
            for (var i = 0; i < numberOfElements; ++i)
            {
                table.Add(i);
            }

            for (var i = 0; i < numberOfElements; ++i)
            {
                table.Erase(i);
            }
        }

        [TestMethod]
        public void ContainsWillNotCrash()
        {
            var numberOfOperations = 10;
            for (var i = 0; i < numberOfOperations; ++i)
            {
                table.Contains(i);
            }
        }

        [TestMethod]
        public void ContainsReturnsFalseForDeletedElement()
        {
            table.Add(1);
            table.Erase(1);

            Assert.IsFalse(table.Contains(1));
        }

        [TestMethod]
        public void ContainsReturnsTrueForAddedElements()
        {
            var numberOfElements = 10;
            for (var i = 0; i < numberOfElements; ++i)
            {
                table.Add(i);
            }

            for (var i = 0; i < numberOfElements; ++i)
            {
                Assert.IsTrue(table.Contains(i));
            }
        }

        [TestMethod]
        public void ContainsReturnsFalseForElementsNotFromTable()
        {
            var numberOfElements = 10;
            for (var i = 0; i < numberOfElements; ++i)
            {
                table.Add(i);
            }

            for (var i = numberOfElements; i < 2 * numberOfElements; ++i)
            {
                Assert.IsFalse(table.Contains(i));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ValueNotInHashTableException))]
        public void EraseFromEmptyTableWillThrowExpectedException()
        {
            table.Erase(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ValueNotInHashTableException))]
        public void EraseWrongElementsWillThrowExpectedException()
        {
            var numberOfElements = 10;
            for (var i = 0; i < numberOfElements; ++i)
            {
                table.Add(i);
            }

            table.Erase(42);
        }
    }
}