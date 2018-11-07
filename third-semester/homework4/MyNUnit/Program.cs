namespace MyNUnit
{
    class Program
    {
        static void Main(string[] args)
        {
            // relative path
            const string path = "../Tests/bin/";
            TestSystem.RunTests(path);
        }
    }
}