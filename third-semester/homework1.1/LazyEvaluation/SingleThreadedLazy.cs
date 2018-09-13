namespace LazyEvaluation
{
    using System;

    public class SingleThreadedLazy<T> : ILazy<T>
    {
        private bool isEvaluated;
        private T result;
        private readonly Func<T> supplier;

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
