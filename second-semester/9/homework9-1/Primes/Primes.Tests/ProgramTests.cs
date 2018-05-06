using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Primes.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        List<int> list = new List<int>() { 0, 1, 2, 3, 4, 5, 7, 10, 11, 13, 17, 19, 23, 100 };
        List<int> primes = new List<int>() { 2, 3, 5, 7, 11, 13, 17, 19, 23 };
        List<int> notPrimes = new List<int>() { 0, 1, 4, 10, 100 };

        [TestMethod]
        public void IsPrimeTest()
        {
            foreach (var integer in primes)
            {
                Assert.IsTrue(Program.IsPrime(integer));
            }

            foreach (var integer in notPrimes)
            {
                Assert.IsFalse(Program.IsPrime(integer));
            }
        }

        [TestMethod]
        public void IntegersTest()
        {
            var current = 1;
            foreach (var item in Program.Integers())
            {
                if (item > 100)
                {
                    break;
                }

                Assert.AreEqual(current, item);
                current++;
            }
        }

        [TestMethod]
        public void FilterFirstTest()
        {
            var result = Program.Filter(list, Program.IsPrime);
            var resultList = new List<int>();

            foreach (var item in result)
            {
                resultList.Add(item);
            }

            var count = 0;
            foreach (var item in list)
            {
                if (Program.IsPrime(item))
                {
                    count++;
                    Assert.IsTrue(resultList.Contains(item));
                }
            }

            Assert.AreEqual(count, resultList.Count);
        }

        [TestMethod]
        public void FilterSecondTest()
        {
            var count = 0;

            foreach (var item in Program.Filter(list, x => false))
            {
                count++;
            }

            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void PrimesTest()
        {
            foreach (var item in Program.Primes())
            {
                if (item > 100)
                {
                    break;
                }

                Assert.IsTrue(Program.IsPrime(item));
            }
        }
    }
}