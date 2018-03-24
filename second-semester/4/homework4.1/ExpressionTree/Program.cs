namespace ExpressionTree
{
    using System;

    public class Program
    {
        private static void Main(string[] args)
        {
            var tree = new ParseTree("(* (- (+ 4 5) 14) (/ 16 4)");
            Console.WriteLine(tree.Value);
        }

    }
}
