using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Class1<T>
    {
        private T value;

        Class1(T value)
        {
            this.value = value;
        }

        public static void Foo()
        {
            Console.WriteLine("foo");
        }
    }
}
