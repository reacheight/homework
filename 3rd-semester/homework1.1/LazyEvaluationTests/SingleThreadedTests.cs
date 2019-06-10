using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LazyEvaluation.Tests
{
    [TestClass]
    public class SingleThreadedTests
    {
        private readonly Func<int> getInteger = () => 457;
        private readonly Func<string> getString = () => "string";

        [TestMethod]
        public void SingleThreadedLazyWorksRightForFirstGetCall()
        {
            var lazyObject = LazyFactory.CreateSingleThreadedLazy(this.getInteger);
            Assert.AreEqual(this.getInteger(), lazyObject.Get());
        }

        [TestMethod]
        public void MultiThreadedLazyWorksRightForFirstGetCall()
        {
            var lazyObject = LazyFactory.CreateMultiThreadedLazy(this.getString);
            Assert.AreEqual(this.getString(), lazyObject.Get());
        }

        [TestMethod]
        public void SingleThreadedLazyWorksRightForMultipleGetCalls()
        {
            for (int i = 0; i < 50; ++i)
            {
                var lazyObject = LazyFactory.CreateSingleThreadedLazy(this.getInteger);
                Assert.AreEqual(this.getInteger(), lazyObject.Get());
            }
        }

        [TestMethod]
        public void MulitThreadedLazyWorksRightForMultipleGetCalls()
        {
            for (int i = 0; i < 50; ++i)
            {
                var lazyObject = LazyFactory.CreateMultiThreadedLazy(this.getInteger);
                Assert.AreEqual(this.getInteger(), lazyObject.Get());
            }
        }

        [TestMethod]
        public void SingleThreadedLazyEvaluatesOnlyOnce()
        {
            
            var evaluationCounter = 0;
            var lazyObject = LazyFactory.CreateSingleThreadedLazy(() =>
            {
                evaluationCounter++;
                return this.getInteger();
            });

            for (int i = 0; i < 100; ++i)
            {
                lazyObject.Get();
            }

            Assert.AreEqual(1, evaluationCounter);
        }

        [TestMethod]
        public void MultiThreadedLazyEvaluatesOnlyOnce()
        {

            var evaluationCounter = 0;
            var lazyObject = LazyFactory.CreateMultiThreadedLazy(() =>
            {
                evaluationCounter++;
                return this.getString();
            });

            for (int i = 0; i < 100; ++i)
            {
                lazyObject.Get();
            }

            Assert.AreEqual(1, evaluationCounter);
        }

        [TestMethod]
        public void SingleThreadedLazyWorksWhenEvaluationResultIsNull()
        {
            var lazyObject = LazyFactory.CreateSingleThreadedLazy<object>(() => null);
            Assert.IsNull(lazyObject.Get());
        }

        [TestMethod]
        public void MultiThreadedLazyWorksWhenEvaluationResultIsNull()
        {
            var lazyObject = LazyFactory.CreateMultiThreadedLazy<object>(() => null);
            Assert.IsNull(lazyObject.Get());
        }
    }
}