namespace LazyEvaluation
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            Func<int> integer = () => 3;

            var lazyObject = LazyFactory<int>.CreateSingleThreadedLazy(integer);
            Console.WriteLine(lazyObject.Get());

            Console.ReadKey();
        }
    }
}
