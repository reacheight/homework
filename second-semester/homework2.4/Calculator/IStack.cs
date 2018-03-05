namespace Calculator
{
    public interface IStack<T>
    {
        int Size { get; }

        bool Empty { get; }

        void Push(T value);

        T Pop();

        T Top();
    }
}
