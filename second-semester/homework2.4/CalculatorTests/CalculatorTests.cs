using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Calculator.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        private Calculator calculator;

        [TestInitialize]
        public void Init()
        {
            this.calculator = new Calculator(new ReferenceStack<double>());
        }

        [TestMethod]
        public void EmptyReturnsTrueIfCalculatorIsEmpty()
        {
            Assert.IsTrue(this.calculator.Empty);
        }

        [TestMethod]
        public void EmptyReturnsFalseAfterPushingOneValue()
        {
            this.calculator.Push(3.14);

            Assert.IsFalse(this.calculator.Empty);
        }

        [TestMethod]
        public void PushAndPopOneElementWorksRight()
        {
            this.calculator.Push(3.14);
            this.calculator.Pop();

            Assert.IsTrue(this.calculator.Empty);
        }

        [TestMethod]
        public void PushAndPopSeveralElementsWorksRight()
        {
            var numberOfElements = 10;
            for (var i = 0; i < numberOfElements; ++i)
            {
                this.calculator.Push(i);
            }

            for (var i = 0; i < numberOfElements; ++i)
            {
                this.calculator.Pop();
            }

            Assert.IsTrue(this.calculator.Empty);
        }

        [TestMethod]
        public void TopWorksRight()
        {
            var numberOfElements = 10;
            for (var i = 0; i < numberOfElements; ++i)
            {
                this.calculator.Push(i);
            }

            for (var i = numberOfElements - 1; i >= 0; --i)
            {
                Assert.AreEqual(i, this.calculator.Top);
                this.calculator.Pop();
            }
        }

        [TestMethod]
        public void AddWorksRightForTwoElements()
        {
            this.calculator.Push(-3);
            this.calculator.Push(4);

            this.calculator.Add();

            Assert.AreEqual(1, this.calculator.Top);
            Assert.AreEqual(1, this.calculator.Size);
        }

        [TestMethod]
        public void SubtractWorksRightForTwoValidValues()
        {
            this.calculator.Push(10);
            this.calculator.Push(5);

            this.calculator.Subtract();

            Assert.AreEqual(5, this.calculator.Top);
            Assert.AreEqual(1, this.calculator.Size);
        }

        [TestMethod]
        public void MultiplyWorksRightForTwoElements()
        {
            this.calculator.Push(1);
            this.calculator.Push(4);

            this.calculator.Multiply();

            Assert.AreEqual(4, this.calculator.Top);
            Assert.AreEqual(1, this.calculator.Size);
        }

        [TestMethod]
        public void DivideWorksRightForTwoValidValues()
        {
            this.calculator.Push(6.15);
            this.calculator.Push(5);

            this.calculator.Divide();

            Assert.AreEqual(6.15 / 5, this.calculator.Top);
            Assert.AreEqual(1, this.calculator.Size);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddThrowExpectedExceptionIfSizeOfStackLessThenTwo()
        {
            this.calculator.Push(3.16);

            this.calculator.Add();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SubtractThrowExpectedExceptionIfSizeOfStackLessThenTwo()
        {
            this.calculator.Push(3.16);

            this.calculator.Subtract();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MultiplyThrowExpectedExceptionIfSizeOfStackLessThenTwo()
        {
            this.calculator.Push(3.16);

            this.calculator.Multiply();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DivideThrowExpectedExceptionIfSizeOfStackLessThenTwo()
        {
            this.calculator.Push(3.16);

            this.calculator.Divide();
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void DivideThrowExpectedExceptionIfHappenedDivisionByZero()
        {
            this.calculator.Push(3.16);
            this.calculator.Push(0);

            this.calculator.Divide();
        }

        [TestMethod]
        public void MultipleOperationsWillWorkWithValidValues()
        {
            this.calculator.Push(3.16);
            this.calculator.Push(103);
            this.calculator.Push(4);
            this.calculator.Push(76.13);

            this.calculator.Add();
            this.calculator.Subtract();
            this.calculator.Divide();
        }

        [TestMethod]
        public void PushAfterOperationWillWork()
        {
            this.calculator.Push(5.3);
            this.calculator.Push(79);

            this.calculator.Add();

            this.calculator.Push(411);
            Assert.AreEqual(411, this.calculator.Top);
        }
    }
}