namespace Test.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class QueueTests
    {
        private Queue<int> intQueue;
        private Queue<string> stringQueue;

        [TestInitialize]
        public void Init()
        {
            this.intQueue = new Queue<int>();
            this.stringQueue = new Queue<string>();
        }

        [TestMethod]
        public void EnqueueIncreaseCount()
        {
            this.intQueue.Enqueue(3, 0);
            this.stringQueue.Enqueue("qwerty", 5);

            Assert.AreEqual(1, this.intQueue.Count);
            Assert.AreEqual(1, this.stringQueue.Count);
        }

        [TestMethod]
        public void DequeueWorksAfterEnqueue()
        {
            this.intQueue.Enqueue(10, 10);
            this.stringQueue.Enqueue("value", -3);

            this.intQueue.Dequeue();
            this.stringQueue.Dequeue();
        }

        [TestMethod]
        public void DequeueDecreaseCount()
        {
            this.intQueue.Enqueue(10, 10);
            this.stringQueue.Enqueue("value", -3);

            this.intQueue.Dequeue();
            this.stringQueue.Dequeue();

            Assert.AreEqual(0, this.intQueue.Count);
            Assert.AreEqual(0, this.stringQueue.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void DequeueFromEmptyQueueThrowExpectedException()
        {
            this.intQueue.Dequeue();
        }

        [TestMethod]
        public void DequeWorksRightWithUniquePriorities()
        {
            this.intQueue.Enqueue(10, 10);
            this.intQueue.Enqueue(9, -9);
            this.intQueue.Enqueue(15, 230);
            this.stringQueue.Enqueue("low", 5);
            this.stringQueue.Enqueue("high", 42);
            this.stringQueue.Enqueue("mid", 20);

            Assert.AreEqual(15, this.intQueue.Dequeue());
            Assert.AreEqual("high", this.stringQueue.Dequeue());
        }

        [TestMethod]
        public void DequeWorksRightWithNotUniquePriorities()
        {
            this.intQueue.Enqueue(10, 10);
            this.intQueue.Enqueue(0, 230);
            this.intQueue.Enqueue(9, -9);
            this.intQueue.Enqueue(15, 230);
            this.stringQueue.Enqueue("low", 5);
            this.stringQueue.Enqueue("first high", 42);
            this.stringQueue.Enqueue("high", 42);
            this.stringQueue.Enqueue("mid", 20);

            Assert.AreEqual(0, this.intQueue.Dequeue());
            Assert.AreEqual("first high", this.stringQueue.Dequeue());
        }

        [TestMethod]
        public void DequeWorksRightWithNegativePriorities()
        {
            this.intQueue.Enqueue(10, -100);
            this.intQueue.Enqueue(0, -7);
            this.intQueue.Enqueue(15, -54);
            this.stringQueue.Enqueue("low", -1024);
            this.stringQueue.Enqueue("high", -128);
            this.stringQueue.Enqueue("mid", -526);

            Assert.AreEqual(0, this.intQueue.Dequeue());
            Assert.AreEqual("high", this.stringQueue.Dequeue());
        }

        [TestMethod]
        public void MultipleDequesWorksRight()
        {
            this.intQueue.Enqueue(10, -100);
            this.intQueue.Enqueue(0, -7);
            this.intQueue.Enqueue(15, -54);
            this.intQueue.Enqueue(42, 24);
            this.stringQueue.Enqueue("low", -1024);
            this.stringQueue.Enqueue("high", 128);
            this.stringQueue.Enqueue("mid", -526);
            this.stringQueue.Enqueue("value", -256);

            Assert.AreEqual(42, this.intQueue.Dequeue());
            Assert.AreEqual(0, this.intQueue.Dequeue());
            Assert.AreEqual("high", this.stringQueue.Dequeue());
            Assert.AreEqual("value", this.stringQueue.Dequeue());
        }
    }
}