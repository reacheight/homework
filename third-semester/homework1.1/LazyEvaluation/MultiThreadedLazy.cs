namespace LazyEvaluation
{
    using System;
    using System.Threading;

    public class MultiThreadedLazy<T> : ILazy<T>
    {
        private readonly Func<T> supplier;
        private bool isEvaluated;
        private T result;

        public MultiThreadedLazy(Func<T> supplier)
        {
            this.isEvaluated = false;
            this.supplier = supplier;
        }

        public T Get()
        {
            if (!this.isEvaluated)
            {
                var evaluation = new Thread(() => this.result = this.supplier());
                evaluation.Start();
                evaluation.Join();
                this.isEvaluated = true;
            }

            return this.result;
        }
    }
}
