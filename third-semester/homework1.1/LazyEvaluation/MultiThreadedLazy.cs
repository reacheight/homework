namespace LazyEvaluation
{
    using System;
    using System.Threading;

    /// <summary>
    /// Thread-safe lazy evaluation implementation
    /// </summary>
    /// <typeparam name="T">type of evaluation result</typeparam>
    public class MultiThreadedLazy<T> : ILazy<T>
    {
        private readonly object lockObject = new object();
        private readonly Func<T> supplier;
        private bool isEvaluated = false;
        private T result;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiThreadedLazy{T}"/> class.
        /// </summary>
        /// <param name="supplier">function that represents evaluation</param>
        public MultiThreadedLazy(Func<T> supplier)
            => this.supplier = supplier;

        /// <summary>
        /// Gets evaluation result
        /// </summary>
        /// <returns>evaluation result</returns>
        public T Get()
        {
            if (!this.isEvaluated)
            {
                lock (this.lockObject)
                {
                    if (!this.isEvaluated)
                    {
                        this.result = this.supplier();
                        this.isEvaluated = true;
                    }
                }
            }

            return this.result;
        }
    }
}
