namespace LazyEvaluation
{
    public interface ILazy<T>
    {
        T Get();
    }
}
