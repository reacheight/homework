using System;

namespace MyThreadPool
{
    /// <summary>
    /// MyTask interface
    /// </summary>
    /// <typeparam name="TResult">task result type</typeparam>
    public interface IMyTask<TResult>
    {
        /// <summary>
        /// Gets true if task is finished, false otherwise
        /// </summary>
        bool IsCompleted { get; }
        
        /// <summary>
        /// Gets task result
        /// </summary>
        TResult Result { get; }

        /// <summary>
        /// Queues new task based on result of this task
        /// </summary>
        /// <param name="newTask">function that represent new task</param>
        /// <typeparam name="TNewResult">new task result type</typeparam>
        /// <returns>IMyTask object based on new task</returns>
        IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> newTask);
    }
}