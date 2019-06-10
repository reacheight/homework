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

            for (var i = 0; i < 10; ++i)
            {
                threadPool.QueueTask(() =>
                {
                    set.Add(Thread.CurrentThread.Name);
                    Thread.Sleep(1000);
                    return 5;
                });
            }

            Thread.Sleep(2000);
            threadPool.Shutdown();

            Assert.IsTrue(set.Count >= threadPool.NumberOfThreads);
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
                    Thread.Sleep(100);
                    return 5;
                });
            }

            Thread.Sleep(100);
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
        public void ShutdownDoesNotStopEvaluatingTasks()
        {
            var numberOfEvaluatedTasks = 0;

            for (var i = 0; i < 4; ++i)
            {
                threadPool.QueueTask(() =>
                {
                    numberOfEvaluatedTasks++;
                    Thread.Sleep(500);
                    return 5;
                });
            }

            Thread.Sleep(100);
            threadPool.Shutdown();

            Thread.Sleep(400);
            Assert.AreEqual(threadPool.NumberOfThreads, numberOfEvaluatedTasks);
        }
        
        [TestMethod]
        public void AfterShutdownTasksFromQueueDoNotEvaluate()
        {
            var numberOfEvaluatedTasks = 0;

            for (var i = 0; i < 10; ++i)
            {
                threadPool.QueueTask(() =>
                {
                    numberOfEvaluatedTasks++;
                    Thread.Sleep(500);
                    return 5;
                });
            }

            Thread.Sleep(100);
            threadPool.Shutdown();

            Thread.Sleep(400);
            Assert.AreEqual(threadPool.NumberOfThreads, numberOfEvaluatedTasks);
        }

        [TestMethod]
        public void IsCompletedWorksRight()
        {
            var task = threadPool.QueueTask(() =>
            {
                Thread.Sleep(100);
                return 5;
            });

            Assert.IsFalse(task.IsCompleted);
            Thread.Sleep(200);
            Assert.IsTrue(task.IsCompleted);
            
            threadPool.Shutdown();
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void ResultThrowRightExceptionOnError()
        {
            var result = threadPool.QueueTask<object>(() => throw new NullReferenceException()).Result;
        }

        [TestMethod]
        public void ContinueWithQueuesNewTasks()
        {
            var task = threadPool.QueueTask(() => 5);

            var isEvaluated = new List<bool> {false, false, false, false};
            for (var i = 0; i < isEvaluated.Count; ++i)
            {
                var j = i;
                task.ContinueWith((x) =>
                {
                    isEvaluated[j] = true;
                    return x;
                });
            }
            

            Thread.Sleep(200);
            foreach (var flag in isEvaluated)
            {
                Assert.IsTrue(flag);
            }
            
            threadPool.Shutdown();
        }

        [TestMethod]
        public void ContinueWithDoesNotBlockThread()
        {
            var isThreadBlocked = true;
            var task = threadPool.QueueTask(() =>
            {
                Thread.Sleep(500);
                return 5;
            });

            task.ContinueWith((x) => 3);

            if (!task.IsCompleted)
            {
                isThreadBlocked = false;
            }
            
            Assert.IsFalse(isThreadBlocked);
            
            threadPool.Shutdown();
        }

        [TestMethod]
        public void ContinueWithTaskEvaluatesAfterMainTask()
        {
            var lastThreadFlag = false;
            var task = threadPool.QueueTask(() =>
            {
                lastThreadFlag = false;
                return 5;
            });

            task.ContinueWith((x) =>
            {
                Thread.Sleep(200);
                lastThreadFlag = true;
                return x;
            });

            Thread.Sleep(400);
            Assert.IsTrue(lastThreadFlag);
            
            threadPool.Shutdown();
        }

        [TestMethod]
        public void ContinueWithTasksDoesNotUseTheSameThread()
        {
            var task = threadPool.QueueTask(() => 4);
            
            var set = new HashSet<string>();
            for (var i = 0; i < threadPool.NumberOfThreads; ++i)
            {
                task.ContinueWith((x) =>
                {
                    set.Add(Thread.CurrentThread.Name);
                    Thread.Sleep(500);
                    return x;
                });
            }

            Assert.AreNotEqual(1, set.Count);
            
            threadPool.Shutdown();
        }
    }
}