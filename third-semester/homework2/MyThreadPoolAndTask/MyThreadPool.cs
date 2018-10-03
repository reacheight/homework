using System;
using System.Collections.Concurrent;
using System.Threading;

namespace MyThreadPoolAndTask
{
    /// <summary>
    /// Class that implements simple thread pool
    /// </summary>
    public class MyThreadPool : IDisposable
    {
        private readonly CancellationTokenSource _cancellationSource = new CancellationTokenSource();
        private readonly BlockingCollection<Action> _taskQueue = new BlockingCollection<Action>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MyThreadPool"/> class.
        /// </summary>
        /// <param name="numberOfThreads">maximum number of working threads</param>
        public MyThreadPool(int numberOfThreads)
        {
            NumberOfThreads = numberOfThreads;
            CreateThreads();
        }
        
        /// <summary>
        /// Gets maximum number of threads in the pool
        /// </summary>
        public int NumberOfThreads { get; }

        /// <summary>
        /// Queues task into thread pool
        /// </summary>
        /// <param name="resultFunction">function that represent task</param>
        /// <typeparam name="TResult">task result type</typeparam>
        /// <returns>IMyTask object based on given function</returns>
        public IMyTask<TResult> QueueTask<TResult>(Func<TResult> resultFunction)
        {
            if (_cancellationSource.Token.IsCancellationRequested)
            {
                throw new InvalidOperationException("Thread pool has been shutted down");
            }
            
            var task = new MyTask<TResult>(resultFunction, this);

            try
            {
                _taskQueue.Add(task.Evaluate, _cancellationSource.Token);
            }
            catch (OperationCanceledException)
            {
                throw new InvalidOperationException("Thread pool has been shutted down");
            }
            catch (ObjectDisposedException)
            {
                throw new InvalidOperationException("Thread pool has been shutted down");
            }

            return task;
        }

        /// <summary>
        /// Shuts down all pool threads
        /// </summary>
        public void Shutdown()
        {
            _cancellationSource.Cancel();
            _taskQueue.CompleteAdding();
            _taskQueue.Dispose();
        }

        public void Dispose()
        {
            Shutdown();
        }

        /// <summary>
        /// Creates pool threads
        /// </summary>
        private void CreateThreads()
        {
            for (var i = 0; i < NumberOfThreads; ++i)
            {
                var thread = new Thread(() =>
                {
                    while (true)
                    {
                        if (_cancellationSource.Token.IsCancellationRequested)
                        {
                            break;
                        }

                        try
                        {
                            _taskQueue.Take(_cancellationSource.Token).Invoke();
                        }
                        catch (OperationCanceledException) { }
                        catch (ObjectDisposedException) { }
                    }
                }) { Name = $"My Pool Thread Number {i}" };

                thread.Start();
            }
        }
    }
}