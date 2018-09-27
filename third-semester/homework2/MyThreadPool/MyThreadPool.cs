using System;
using System.Collections.Concurrent;
using System.Threading;

namespace MyThreadPool
{
    public class MyThreadPool
    {
        private static readonly CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();
        private ConcurrentQueue<Action> _taskQueue = new ConcurrentQueue<Action>();
        
        public int NumberOfThreads { get; }

        public MyThreadPool(int numberOfThreads)
        {
            NumberOfThreads = numberOfThreads;
            CreateThreads();
        }

        public IMyTask<TResult> QueueTask<TResult>(Func<TResult> resultFunction)
        {
            var task = new MyTask<TResult>(resultFunction, this);
            _taskQueue.Enqueue(task.Evaluate);
            return task;
        }

        public void Shutdown()
        {
            CancellationTokenSource.Cancel();
            _taskQueue = null;
        }

        private void CreateThreads()
        {
            for (var i = 0; i < NumberOfThreads; ++i)
            {
                var thread = new Thread(() =>
                {
                    while (true)
                    {
                        if (CancellationTokenSource.Token.IsCancellationRequested)
                        {
                            break;
                        }

                        Action task = null;
                        _taskQueue?.TryDequeue(out task);
                        task?.Invoke();
                    }
                });

                thread.Start();
            }
        }
        
        private class MyTask<TResult> : IMyTask<TResult>
        {
            private readonly MyThreadPool _threadPool;
            private Func<TResult> _resultFunc;
            private TResult _result;
            private AggregateException _exception;
            
            public bool IsCompleted { get; private set; }

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
            

            public MyTask(Func<TResult> resultFunc, MyThreadPool threadPool)
            {
                _resultFunc = resultFunc;
                _threadPool = threadPool;
            }

            public void Evaluate()
            {
                try
                {
                    Result = _resultFunc();
                }
                catch (Exception exception)
                {
                    _exception = new AggregateException(exception);
                }
                
                IsCompleted = true;
                _resultFunc = null;
            }
            
            public IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> newTask)
                => _threadPool.QueueTask(() => newTask(Result));
        }
    }
}