﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UniqueList.Tests
{
    [TestClass()]
    public class UniqeListTests
    {
        private UniqueList<int> list;

        [TestInitialize]
        public void Init()
        {
            list = new UniqueList<int>();
        }

        [TestMethod]
        public void IsEmptyReturnsTrueIfListIsEmpty()
        {
            Assert.IsTrue(list.IsEmpty());
        }

        [TestMethod]
        public void InsertInHeadWillNotCrash()
        {
            list.Insert(56, 0);
        }

        [TestMethod]
        public void InsertRandomIntegerInHeadWillNotCrash()
        {
            int randomInteger = new Random().Next();
            list.Insert(randomInteger, 0);
        }

        [TestMethod]
        public void InsertDifferentValuesWillNotCrash()
        {
            var numberOfElements = 100;

            for (int i = 0; i < numberOfElements; ++i)
            {
                list.Insert(i, i);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InsertInIncorrectPositionWillThrowExpectedException()
        {
            var numberOfElements = 100;
            for (var i = 0; i < numberOfElements; ++i)
            {
                list.Insert(i, i);
            }

            list.Insert(101, numberOfElements + 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ValueAlreadyInListException))]
        public void InsertValueThatAlreadyInListWillThrowExpectedException()
        {
            var numberOfElements = 100;
            for (var i = 0; i < numberOfElements; ++i)
            {
                list.Insert(i, i);
            }

            list.Insert(40, numberOfElements + 10);
        }

        [TestMethod]
        public void SizeOfEmptyListIsZero()
        {
            Assert.AreEqual(0, list.Size);
        }

        [TestMethod]
        public void EraseHeadFromNonemptyListWillNotCrash()
        {
            list.Insert(14, 0);

            list.Erase(0);
        }

        [TestMethod]
        public void EraseValidValueWorksRight()
        {
            list.Insert(14, 0);

            list.EraseValue(14);
        }

        [TestMethod]
        [ExpectedException(typeof(ValueNotInListException))]
        public void EraseValueThatNotInListWillThrowExpectedException()
        {
            var numberOfElements = 100;
            for (var i = 0; i < numberOfElements; ++i)
            {
                list.Insert(i, i);
            }

            list.EraseValue(numberOfElements + 10);
        }

        [TestMethod]
        public void EraseNotOnlyFromHeadWillNotCrash()
        {
            var numberOfElements = 100;
            for (var i = 0; i < numberOfElements; ++i)
            {
                list.Insert(i, i);
            }

            for (var i = numberOfElements - 1; i >= 0; --i)
            {
                list.Erase(i);
            }
        }

        [TestMethod]
        public void SizeReturnsRightValue()
        {
            var numberOfAddedElements = 100;
            for (var i = 0; i < numberOfAddedElements; ++i)
            {
                list.Insert(i, i);
            }

            var numberOfRemovedElements = 50;
            for (var i = numberOfRemovedElements - 1; i >= 0; --i)
            {
                list.Erase(i);
            }

            Assert.AreEqual(numberOfAddedElements - numberOfRemovedElements, list.Size);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ValueOfElementByIncorrectPositionWillThrowExpectedException()
        {
            var numberOfElements = 100;
            for (var i = 0; i < numberOfElements; ++i)
            {
                list.Insert(i, i);
            }

            var value = list.Value(numberOfElements);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EraseFromIncorrectPositionWillThrowExpectedException()
        {
            var numberOfElements = 100;
            for (var i = 0; i < numberOfElements; ++i)
            {
                list.Insert(i, i);
            }

            list.Erase(numberOfElements);
        }

        [TestMethod]
        public void ValueWorksRight()
        {
            var numberOfElements = 100;
            var array = new int[numberOfElements];
            for (var i = 0; i < numberOfElements; ++i)
            {
                array[i] = i;
                list.Insert(i, i);
            }

            for (var i = 0; i < numberOfElements; ++i)
            {
                Assert.AreEqual(array[i], list.Value(i));
            }
        }

        [TestMethod]
        public void PositionWorksRight()
        {
            var numberOfElements = 100;
            for (var i = 0; i < numberOfElements; ++i)
            {
                list.Insert(i, i);
            }

            for (var i = 0; i < numberOfElements; ++i)
            {
                Assert.AreEqual(i, list.Position(i));
            }

            for (var i = numberOfElements; i < 2 * numberOfElements; ++i)
            {
                Assert.AreEqual(-1, list.Position(i));
            }
        }
    }
}