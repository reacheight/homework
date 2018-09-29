using System;
using System.Collections.Concurrent;
using System.Threading;

namespace MyThreadPool
{
    public class MyThreadPool
    {
        private static readonly CancellationTokenSource CancellationSource = new CancellationTokenSource();
        private static readonly ManualResetEvent BlockHandle = new ManualResetEvent(false);
        private ConcurrentQueue<Action> _taskQueue = new ConcurrentQueue<Action>();
        
        public int NumberOfThreads { get; }

        public MyThreadPool(int numberOfThreads)
        {
            NumberOfThreads = numberOfThreads;
            CreateThreads();
        }

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

        public void Shutdown()
        {
            CancellationSource.Cancel();
            _taskQueue = null;
            BlockHandle.Set();
        }

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