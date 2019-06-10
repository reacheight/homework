using System;
using System.Threading;

namespace MyThreadPoolAndTask
{
        /// <summary>
        /// IMyTask interface implementation
        /// </summary>
        /// <typeparam name="TResult">task result type</typeparam>
        public class MyTask<TResult> : IMyTask<TResult>
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