namespace LazyEvaluation
{
    /// <summary>
    /// Lazy evaluation interface
    /// </summary>
    /// <typeparam name="T">type of evaluation result</typeparam>
    public interface ILazy<T>
    {
        T Get();
    }
}
