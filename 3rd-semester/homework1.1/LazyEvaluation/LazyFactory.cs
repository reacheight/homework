namespace LazyEvaluation
{
    using System;

    /// <summary>
    /// Class for creating lazy evaluation objects
    /// </summary>
    public static class LazyFactory
    {
        /// <summary>
        /// Creates new instance of simple lazy evaluation implementation
        /// </summary>
        /// <param name="supplier">function that represents evaluation</param>
        /// <returns>simple lazy evaluation object</returns>
        /// <typeparam name="T">type of evaluation result</typeparam>
        public static ILazy<T> CreateSingleThreadedLazy<T>(Func<T> supplier)
            => new SingleThreadedLazy<T>(supplier);

        /// <summary>
        /// Creates new instance of thread-safe lazy evaluation implementation
        /// </summary>
        /// <param name="supplier">function that represents evaluation</param>
        /// <returns>thread-safe lazy evaluation object</returns>
        /// <typeparam name="T">type of evaluation result</typeparam>
        public static ILazy<T> CreateMultiThreadedLazy<T>(Func<T> supplier)
            => new MultiThreadedLazy<T>(supplier);
    }
}
