using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparseVector
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var vector1 = new SparseVector(10);
            var vector2 = new SparseVector(10);

            var i = 0;
            while (i < 10)
            {
                vector1[i] = i;
                vector2[i] = i + 10;
                i++;
            }

            vector1[5] = 0;

            vector1.Add(vector2);
            vector1.Subtract(vector2);
            var res = vector1.Multiply(vector2);

            var vector3 = vector1.Clone();
            vector3.PushBack(123);

            var nullVector = new SparseVector(5);
            int j = 0;
            while (j < 5)
            {
                nullVector[j] = 0;
            }
        }
    }
}