using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyThreadPoolAndTask.Test
{
    [TestClass]
    public class MyThreadPoolTest
    {
        private MyThreadPool threadPool;
        
        [TestInitialize]
        public void TestInitialisation()
        {
            threadPool = new MyThreadPool(4);
        }
        
        [TestMethod]
        public void NumberOfMaximumThreadsIsRight()
        {
            var set = new HashSet<string>();

            for (var i = 0; i < threadPool.NumberOfThreads; ++i)
            {
                threadPool.QueueTask(() =>
                {
                    set.Add(Thread.CurrentThread.Name);
                    Thread.Sleep(2000);
                    return 5;
                });
            }

            Thread.Sleep(2000);
            threadPool.Shutdown();
            
            Assert.IsTrue(set.Count == threadPool.NumberOfThreads);
        }

        [TestMethod]
        public void TaskResultIsRight()
        {
            int GetInteger() => 57;
            var intTask = threadPool.QueueTask(GetInteger);

            string GetString() => "string";
            var stringTask = threadPool.QueueTask(GetString);

            Assert.AreEqual(GetInteger(), intTask.Result);
            Assert.AreEqual(GetString(), stringTask.Result);
            
            threadPool.Shutdown();
        }

        [TestMethod]
        public void TaskEvaluatesOnlyOnce()
        {
            var counts = new List<int> {0, 0, 0, 0};

            for (var i = 0; i < counts.Count; ++i)
            {
                var j = i;
                threadPool.QueueTask(() =>
                {
                    counts[j]++;
                    Thread.Sleep(1000);
                    return 5;
                });
            }

            Thread.Sleep(1000);
            foreach (var count in counts)
            {
                Assert.AreEqual(1, count);
            }
            
            threadPool.Shutdown();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ImpossibleToQueueTaskAfterShutdown()
        {
            threadPool.Shutdown();

            threadPool.QueueTask(() => 123);
        }

        [TestMethod]
        public void ShutdownWorksRight()
        {
            var countOfEvaluatedTasks = 0;

            for (var i = 0; i < 10; ++i)
            {
                threadPool.QueueTask(() =>
                {
                    countOfEvaluatedTasks++;
                    Thread.Sleep(5000);
                    return 5;
                });
            }

            Thread.Sleep(1000);
            threadPool.Shutdown();

            Thread.Sleep(4000);
            Assert.AreEqual(threadPool.NumberOfThreads, countOfEvaluatedTasks);
        }

        [TestMethod]
        public void IsCompletedWorksRight()
        {
            var task = threadPool.QueueTask(() =>
            {
                Thread.Sleep(1000);
                return 5;
            });

            Assert.IsFalse(task.IsCompleted);
            Thread.Sleep(2000);
            Assert.IsTrue(task.IsCompleted);
            
            threadPool.Shutdown();
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void ResultThrowRightExceptionOnError()
        {
            var result = threadPool.QueueTask<object>(() => throw new NullReferenceException()).Result;
        }
    }
}