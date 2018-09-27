namespace LazyEvaluation
{
    using System;

    public class SingleThreadedLazy<T> : ILazy<T>
    {
        private readonly Func<T> supplier;
        private bool isEvaluated;
        private T result;

        public SingleThreadedLazy(Func<T> supplier)
        {
            this.isEvaluated = false;
            this.supplier = supplier;
        }

        public T Get()
        {
            if (!this.isEvaluated)
            {
                this.result = this.supplier();
                this.isEvaluated = true;
            }

            return this.result;
        }
    }
}
