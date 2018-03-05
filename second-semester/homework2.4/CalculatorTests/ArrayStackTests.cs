using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Calculator.Tests
{
    [TestClass]
    public class ArrayStackTests
    {
        private ArrayStack<double> stack;

        [TestInitialize]
        public void Init()
        {
            this.stack = new ArrayStack<double>();
        }

        [TestMethod]
        public void EmptyReturnsTrueIfStackIsEmpty()
        {
            Assert.IsTrue(this.stack.Empty);
        }

        [TestMethod]
        public void EmptyReturnsFalseAfterPushingOneValue()
        {
            this.stack.Push(3.14);

            Assert.IsFalse(this.stack.Empty);
        }

        [TestMethod]
        public void PushAndPopOneElementWorksRight()
        {
            this.stack.Push(3.14);
            this.stack.Pop();

            Assert.IsTrue(this.stack.Empty);
        }

        [TestMethod]
        public void PushAndPopSeveralElementsWorksRight()
        {
            var numberOfElements = 10;
            for (var i = 0; i < numberOfElements; ++i)
            {
                this.stack.Push(i);
            }

            for (var i = 0; i < numberOfElements; ++i)
            {
                this.stack.Pop();
            }

            Assert.IsTrue(this.stack.Empty);
        }

        [TestMethod]
        public void TopWorksRight()
        {
            var numberOfElements = 10;
            for (var i = 0; i < numberOfElements; ++i)
            {
                this.stack.Push(i);
            }

            for (var i = numberOfElements - 1; i >= 0; --i)
            {
                Assert.AreEqual(i, this.stack.Top());
                this.stack.Pop();
            }
        }

        [TestMethod]
        public void SizeWorksRight()
        {
            var numberOfPushedElements = 50;
            for (var i = 0; i < numberOfPushedElements; ++i)
            {
                this.stack.Push(i);
            }

            var numberOfPopedElements = 30;
            for (var i = 0; i < numberOfPopedElements; ++i)
            {
                this.stack.Pop();
            }

            Assert.AreEqual(numberOfPushedElements - numberOfPopedElements, this.stack.Size);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PopFromEmptyStackThrowExpectedException()
        {
            this.stack.Pop();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TopOfEmptyStackThrowExpectedException()
        {
            this.stack.Top();
        }
    }
}