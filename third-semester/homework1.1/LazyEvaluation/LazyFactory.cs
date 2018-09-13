namespace LazyEvaluation
{
    using System;

    public static class LazyFactory<T>
    {
        public static ILazy<T> CreateSingleThreadedLazy(Func<T> supplier)
        {
            return new SingleThreadedLazy<T>(supplier);
        }

        public static ILazy<T> CreateMultiThreadedLazy(Func<T> supplier)
        {
            return new MultiThreadedLazy<T>(supplier);
        }
    }
}
