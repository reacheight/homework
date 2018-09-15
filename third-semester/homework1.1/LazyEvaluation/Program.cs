namespace LazyEvaluation
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            Func<int> createInteger = () => 3;
            Func<string> createString = () => "string";

            var simpleLazyObject = LazyFactory.CreateSingleThreadedLazy(createInteger);
            Console.WriteLine(simpleLazyObject.Get());

            var lazyObject = LazyFactory.CreateMultiThreadedLazy(createString);
            Console.WriteLine(lazyObject.Get());
        }
    }
}
