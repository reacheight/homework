namespace LazyEvaluation
{
    using System;

    /// <summary>
    /// Simple lazy evaluation implementation
    /// </summary>
    /// <typeparam name="T">type of evaluation result</typeparam>
    public class SingleThreadedLazy<T> : ILazy<T>
    {
        private bool isEvaluated = false;
        private Func<T> supplier;
        private T result;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleThreadedLazy{T}"/> class.
        /// </summary>
        /// <param name="supplier">function that represents evaluation</param>
        public SingleThreadedLazy(Func<T> supplier)
            => this.supplier = supplier;

        /// <summary>
        /// Gets evaluation result
        /// </summary>
        /// <returns>evaluation result</returns>
        public T Get()
        {
            if (!this.isEvaluated)
            {
                this.result = this.supplier();
                this.isEvaluated = true;
                this.supplier = null;

            }

            return this.result;
        }
    }
}
