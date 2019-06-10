namespace LazyEvaluation
{
    /// <summary>
    /// Lazy evaluation interface
    /// </summary>
    /// <typeparam name="T">type of evaluation result</typeparam>
    public interface ILazy<T>
    {
        /// <summary>
        /// Gets result of evaluation
        /// </summary>
        /// <returns>result of evaluation</returns>
        T Get();
    }
}
