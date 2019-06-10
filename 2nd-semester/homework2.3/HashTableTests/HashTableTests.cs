using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HashTable.Tests
{
    [TestClass()]
    public class HashTableTests
    {
        private HashTable<string> table;

        [TestInitialize]
        public void Init()
        {
            table = new HashTable<string>();
        }

        [TestMethod]
        public void AddOneElementWillNotCrash()
        {
            table.Add("one");
        }

        [TestMethod]
        public void AddDifferentElementsWillNotCrash()
        {
            var numberOfElements = 10;
            for (var i = 0; i < numberOfElements; ++i)
            {
                table.Add(i.ToString());
            }
        }

        [TestMethod]
        public void AddSameElementsWillNotCrash()
        {
            var numberOfElements = 10;
            for (var i = 0; i < numberOfElements; ++i)
            {
                table.Add("one");
            }
        }

        [TestMethod]
        public void HashTableDoNotStoreSameElements()
        {
            var numberOfElements = 10;
            for (var i = 0; i < numberOfElements; ++i)
            {
                table.Add("one");
            }

            table.Erase("one");

            Assert.IsFalse(table.Contains("one"));
        }

        [TestMethod]
        public void EraseOneElementWillNotCrash()
        {
            table.Add("one");
            table.Erase("one");
        }

        [TestMethod]
        public void EraseElementsWillNotCrash()
        {
            var numberOfElements = 10;
            for (var i = 0; i < numberOfElements; ++i)
            {
                table.Add(i.ToString());
            }

            for (var i = 0; i < numberOfElements; ++i)
            {
                table.Erase(i.ToString());
            }
        }

        [TestMethod]
        public void ContainsWillNotCrash()
        {
            var numberOfOperations = 10;
            for (var i = 0; i < numberOfOperations; ++i)
            {
                table.Contains(i.ToString());
            }
        }

        [TestMethod]
        public void ContainsReturnsFalseForDeletedElement()
        {
            table.Add("one");
            table.Erase("one");

            Assert.IsFalse(table.Contains("one"));
        }

        [TestMethod]
        public void ContainsReturnsTrueForAddedElements()
        {
            var numberOfElements = 10;
            for (var i = 0; i < numberOfElements; ++i)
            {
                table.Add(i.ToString());
            }

            for (var i = 0; i < numberOfElements; ++i)
            {
                Assert.IsTrue(table.Contains(i.ToString()));
            }
        }

        [TestMethod]
        public void ContainsReturnsFalseForElementsNotFromTable()
        {
            var numberOfElements = 10;
            for (var i = 0; i < numberOfElements; ++i)
            {
                table.Add(i.ToString());
            }

            for (var i = numberOfElements; i < 2* numberOfElements; ++i)
            {
                Assert.IsFalse(table.Contains(i.ToString()));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EraseFromEmptyTableWillThrowExpectedException()
        {
            table.Erase("one");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EraseWrongElementsWillThrowExpectedException()
        {
            var numberOfElements = 10;
            for (var i = 0; i < numberOfElements; ++i)
            {
                table.Add(i.ToString());
            }

            table.Erase("one");
        }
    }
}