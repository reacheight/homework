namespace LazyEvaluation
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            Func<int> integer = () => 3;

            var lazyObject = LazyFactory<int>.CreateSingleThreadedLazy(integer);
            Console.WriteLine(lazyObject.Get());

            Console.ReadKey();
        }
    }
}
