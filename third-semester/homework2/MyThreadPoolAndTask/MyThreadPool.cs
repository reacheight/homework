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
        private static readonly CancellationTokenSource CancellationSource = new CancellationTokenSource();
        private static readonly ManualResetEvent BlockHandle = new ManualResetEvent(false);
        private ConcurrentQueue<Action> _taskQueue = new ConcurrentQueue<Action>();

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
            if (CancellationSource.Token.IsCancellationRequested)
            {
                throw new InvalidOperationException("Pool has been shutted down.");
            }
            
            var task = new MyTask<TResult>(resultFunction, this);
            _taskQueue.Enqueue(task.Evaluate);
            BlockHandle.Set();
            return task;
        }

        /// <summary>
        /// Shuts down all pool threads
        /// </summary>
        public void Shutdown()
        {
            CancellationSource.Cancel();
            _taskQueue = null;
            BlockHandle.Set();
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
                        BlockHandle.WaitOne();
                        
                        if (CancellationSource.Token.IsCancellationRequested)
                        {
                            break;
                        }

                        Action task = null;
                        _taskQueue?.TryDequeue(out task);

                        if (_taskQueue != null && _taskQueue.IsEmpty)
                        {
                            BlockHandle.Reset();
                        }
                        
                        task?.Invoke();
                    }
                }) { Name = $"My Pool Thread Number {i}"};

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
                    while (!IsCompleted) { }

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