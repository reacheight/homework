using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Calculator.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        private Calculator calculator;
        private ReferenceStack<double> stack;

        private const double Delta = 0.000001;

        [TestInitialize]
        public void Init()
        {
            this.stack = new ReferenceStack<double>();
            this.calculator = new Calculator(stack);
        }

        [TestMethod]
        public void AddWorksRightForTwoElements()
        {
            this.stack.Push(-3);
            this.stack.Push(4);

            this.calculator.Add();

            Assert.AreEqual(1, this.stack.Top(), Delta);
            Assert.AreEqual(1, this.stack.Size);
        }

        [TestMethod]
        public void SubtractWorksRightForTwoValidValues()
        {
            this.stack.Push(10);
            this.stack.Push(5);

            this.calculator.Subtract();

            Assert.AreEqual(5, this.stack.Top(), Delta);
            Assert.AreEqual(1, this.stack.Size);
        }

        [TestMethod]
        public void MultiplyWorksRightForTwoElements()
        {
            this.stack.Push(1);
            this.stack.Push(4);

            this.calculator.Multiply();

            Assert.AreEqual(4, this.stack.Top(), Delta);
            Assert.AreEqual(1, this.stack.Size);
        }

        [TestMethod]
        public void DivideWorksRightForTwoValidValues()
        {
            this.stack.Push(6.15);
            this.stack.Push(5);

            this.calculator.Divide();

            Assert.AreEqual(6.15 / 5, this.stack.Top(), Delta);
            Assert.AreEqual(1, this.stack.Size);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddThrowExpectedExceptionIfSizeOfStackLessThenTwo()
        {
            this.stack.Push(3.16);

            this.calculator.Add();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SubtractThrowExpectedExceptionIfSizeOfStackLessThenTwo()
        {
            this.stack.Push(3.16);

            this.calculator.Subtract();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MultiplyThrowExpectedExceptionIfSizeOfStackLessThenTwo()
        {
            this.stack.Push(3.16);

            this.calculator.Multiply();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DivideThrowExpectedExceptionIfSizeOfStackLessThenTwo()
        {
            this.stack.Push(3.16);

            this.calculator.Divide();
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void DivideThrowExpectedExceptionIfHappenedDivisionByZero()
        {
            this.stack.Push(3.16);
            this.stack.Push(0);

            this.calculator.Divide();
        }

        [TestMethod]
        public void MultipleOperationsWillWorkWithValidValues()
        {
            this.stack.Push(3.16);
            this.stack.Push(103);
            this.stack.Push(4);
            this.stack.Push(76.13);

            this.calculator.Add();
            this.calculator.Subtract();
            this.calculator.Divide();
        }

        [TestMethod]
        public void PushAfterOperationWillWork()
        {
            this.stack.Push(5.3);
            this.stack.Push(79);

            this.calculator.Add();

            this.stack.Push(411);
            Assert.AreEqual(411, this.stack.Top(), Delta);
        }
    }
}