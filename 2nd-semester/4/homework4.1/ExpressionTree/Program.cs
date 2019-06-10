namespace ExpressionTree
{
    using System;
    using System.IO;

    public class Program
    {
        private static void Main(string[] args)
        {
            string expression = File.ReadAllText("input.txt");

            var tree = new ParseTree(expression);
            Console.WriteLine("Инфиксная форма выражения из дерева разбора: ");
            Console.WriteLine(tree.InfixNotation);
            Console.WriteLine($"Значение выражения: {tree.Value}");
        }
    }
}
