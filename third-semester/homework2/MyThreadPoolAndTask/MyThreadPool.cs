using System;
using System.Collections.Concurrent;
using System.Threading;

namespace MyThreadPoolAndTask
{
    /// <summary>
    /// Class that implements simple thread pool
    /// </summary>
    public class MyThreadPool
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

            return task;
        }

        /// <summary>
        /// Shuts down all pool threads
        /// </summary>
        public void Shutdown()
        {
            _cancellationSource.Cancel();
            _taskQueue.CompleteAdding();
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
                    }
                }) { Name = $"My Pool Thread Number {i}" };

                thread.Start();
            }
        }
        
        /// <summary>
        /// IMyTask interface implementation
        /// </summary>
        /// <typeparam name="TResult">task result type</typeparam>
        private class MyTask<TResult> : IMyTask<TResult>
        {
            private readonly MyThreadPool _threadPool;
            private readonly ManualResetEvent _resultHandle = new ManualResetEvent(false);
            private Func<TResult> _resultFunction;
            private TResult _result;
            private AggregateException _exception;
            
            /// <summary>
            /// Initializes a new instance of the <see cref="MyTask{T}"/> class.
            /// </summary>
            /// <param name="resultFunction">function that represent task</param>
            /// <param name="threadPool">
            /// thread pool in which tasks from ContinueWith method
            /// are going to be evaluated
            /// </param>
            public MyTask(Func<TResult> resultFunction, MyThreadPool threadPool)
            {
                _resultFunction = resultFunction;
                _threadPool = threadPool;
            }
            
            /// <summary>
            /// Gets true if task is finished
            /// </summary>
            public bool IsCompleted { get; private set; }

            /// <summary>
            /// Gets task result
            /// </summary>
            public TResult Result
            {
                get
                {
                    _resultHandle.WaitOne();

                    if (_exception != null)
                    {
                        throw _exception;
                    }

                    return _result;
                }

                private set => _result = value;
            }

            /// <summary>
            /// Evaluates task and assings Result and IsCompleted properties
            /// </summary>
            public void Evaluate()
            {
                try
                {
                    Result = _resultFunction();
                }
                catch (Exception exception)
                {
                    _exception = new AggregateException(exception);
                }
                
                IsCompleted = true;
                _resultFunction = null;
                _resultHandle.Set();
            }
            
            /// <summary>
            /// Queues new task based on result of this task
            /// </summary>
            /// <param name="newTask">function that represent new task</param>
            /// <typeparam name="TNewResult">new task result type</typeparam>
            /// <returns>IMyTask object based on new task</returns>
            public IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> newTask)
                => _threadPool.QueueTask(() => newTask(Result));
        }
    }
}