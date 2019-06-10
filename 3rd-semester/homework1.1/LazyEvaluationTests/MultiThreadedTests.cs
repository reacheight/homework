using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace LazyEvaluation.Tests
{
    [TestClass]
    public class MultiThreadedTests
    {
        private readonly Func<int> getInteger = () => 643;
        private readonly Action timeConsumingJob = () => Thread.Sleep(1000);

        private static T[] DoWorkOnMultipleThreads<T>(Func<T> supplier)
        {
            var lazyObject = LazyFactory.CreateMultiThreadedLazy(supplier);
            var threads = new Thread[10];
            var results = new T[threads.Length];
            for (int i = 0; i < threads.Length; ++i)
            {
                var local = i;
                threads[i] = new Thread(() => results[local] = lazyObject.Get());
                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            return results;
        }

        [TestMethod]
        public void MultiThreadedLazyWorksCorrectWithMultipleThreads()
        {
            foreach (var value in DoWorkOnMultipleThreads(this.getInteger))
            {
                Assert.AreEqual(this.getInteger(), value);
            }
        }

        [TestMethod]
        public void MultiThreadedLazyEvaluatesOnlyOnceWithMultipleThreads()
        {
            var evaluationCount = 0;
            DoWorkOnMultipleThreads(() =>
            {
                evaluationCount++;
                this.timeConsumingJob();
                return this.getInteger();
            });

            Assert.AreEqual(1, evaluationCount);
        }
    }
}
