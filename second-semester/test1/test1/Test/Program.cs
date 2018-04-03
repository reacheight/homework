using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var que = new Queue<string>();

            que.Enqueue("a", 13);
            que.Enqueue("b", 9);
            que.Enqueue("c", 10);
            que.Enqueue("d", 13);
            que.Enqueue("e", 12);

            Console.WriteLine(que.Dequeue());
            Console.WriteLine(que.Dequeue());
            Console.WriteLine(que.Dequeue());
        }
    }
}
