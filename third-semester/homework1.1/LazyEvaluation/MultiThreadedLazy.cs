namespace LazyEvaluation
{
    using System;
    using System.Threading;

    public class MultiThreadedLazy<T> : ILazy<T>
    {
        private readonly Mutex mutex = new Mutex();
        private readonly Func<T> supplier;
        private volatile bool isEvaluated = false;
        private T result;

        public MultiThreadedLazy(Func<T> supplier)
        {
            this.supplier = supplier;
        }

        public T Get()
        {
            if (!this.isEvaluated)
            {
                this.mutex.WaitOne();
                if (!this.isEvaluated)
                {
                    this.result = this.supplier();
                    this.isEvaluated = true;
                }

                this.mutex.ReleaseMutex();
            }

            return this.result;
        }
    }
}
