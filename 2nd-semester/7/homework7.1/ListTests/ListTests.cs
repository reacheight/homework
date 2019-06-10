using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace List.Tests
{
    [TestClass]
    public class ListTests
    {
        List<int> list;
        List<string> stringList;

        [TestInitialize]
        public void Init()
        {
            this.list = new List<int>();
            this.stringList = new List<string>();
        }

        [TestMethod]
        public void CountOfEmptyListIsZero()
        {
            Assert.AreEqual(0, this.list.Count);
        }

        [TestMethod]
        public void AddIncreaseCount()
        {
            this.list.Add(3);

            Assert.AreEqual(1, this.list.Count);
        }

        [TestMethod]
        public void InsertIncreaseCount()
        {
            this.list.Insert(0, 3);

            Assert.AreEqual(1, this.list.Count);
        }
        
        [TestMethod]
        public void AddStoreItem()
        {
            this.list.Add(3);

            Assert.IsTrue(this.list.Contains(3));
        }

        [TestMethod]
        public void InsertStoreItem()
        {
            this.list.Insert(0, 3);

            Assert.IsTrue(this.list.Contains(3));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InsertionInWrongIndexThrowExpectedException()
        {
            this.list.Insert(0, 3);
            this.list.Insert(7, 3);
        }

        [TestMethod]
        public void InsertionInDifferentIndexesWorksRight()
        {
            this.list.Insert(0, 3);
            this.list.Insert(1, 4);
            this.list.Insert(2, 7);
            this.list.Insert(0, 10);
        }

        [TestMethod]
        public void RemoveDecreaseCount()
        {
            this.list.Insert(0, 3);
            this.list.Remove(3);

            Assert.AreEqual(0, this.list.Count);
        }

        [TestMethod]
        public void RemoveAtDecreaseCount()
        {
            this.list.Insert(0, 3);
            this.list.RemoveAt(0);

            Assert.AreEqual(0, this.list.Count);
        }

        [TestMethod]
        public void RemoveWorksRight()
        {
            this.list.Insert(0, 3);
            this.list.Insert(0, 5);
            this.list.Insert(0, 10);
            this.list.Remove(3);

            Assert.IsFalse(this.list.Contains(3));
        }

        [TestMethod]
        public void RemoveAtWorksRight()
        {
            this.list.Insert(0, 3);
            this.list.Insert(1, 5);
            this.list.Insert(2, 10);
            this.list.RemoveAt(0);
            this.list.RemoveAt(0);

            Assert.IsFalse(this.list.Contains(3));
            Assert.IsFalse(this.list.Contains(5));
        }

        [TestMethod]
        public void RemoveWrongItemReturnsFalse()
        {
            this.list.Insert(0, 3);
            this.list.Insert(0, 5);
            this.list.Insert(0, 10);

            Assert.IsFalse(this.list.Remove(15));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RemoveAtWrongIndexThrowExpectedException()
        {
            this.list.Insert(0, 3);
            this.list.Insert(7, 3);
            this.list.RemoveAt(-3);
        }

        [TestMethod]
        public void IndexOfWorksRight()
        {
            this.list.Insert(0, 3);
            this.list.Insert(1, 5);
            this.list.Insert(2, 10);

            Assert.AreEqual(0, this.list.IndexOf(3));
            Assert.AreEqual(2, this.list.IndexOf(10));
            Assert.AreEqual(1, this.list.IndexOf(5));
            Assert.AreEqual(-1, this.list.IndexOf(23));
        }

        [TestMethod]
        public void GettingByIndexWorksRight()
        {
            this.list.Insert(0, 3);
            this.list.Insert(1, 5);
            this.list.Insert(2, 10);

            Assert.AreEqual(5, this.list[1]);
            Assert.AreEqual(10, this.list[2]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GettingByWrongIndexThrowExcpectedException()
        {
            this.list.Insert(0, 3);
            this.list.Insert(7, 3);
            this.list.RemoveAt(-3);

            var tmp = this.list[5];
        }

        [TestMethod]
        public void ClearWorksRight()
        {
            this.list.Insert(0, 3);
            this.list.Insert(1, 5);
            this.list.Insert(1, 7);

            this.list.Clear();

            Assert.AreEqual(0, this.list.Count);
            Assert.IsFalse(this.list.Contains(3));
            Assert.IsFalse(this.list.Contains(5));
            Assert.IsFalse(this.list.Contains(7));
        }

        [TestMethod]
        public void EnumeratorWorksRight()
        {
            this.list.Insert(0, 3);
            this.list.Insert(1, 5);
            this.list.Insert(1, 7);

            var i = 0;
            foreach (var item in list)
            {
                Assert.AreEqual(list[i], item);
                ++i;
            }
        }

        [TestMethod]
        public void CopyToWorksRight()
        {
            this.list.Insert(0, 3);
            this.list.Insert(1, 5);
            this.list.Insert(1, 7);

            var array = new int[3];
            this.list.CopyTo(array, 0);

            for (int i = 0; i < this.list.Count; ++i)
            {
                Assert.AreEqual(list[i], array[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyToNullArrayThrowExpectedException()
        {
            this.list.Insert(0, 3);
            this.list.Insert(1, 5);
            this.list.Insert(1, 7);

            int[] array = null;
            this.list.CopyTo(array, 0);

            for (int i = 0; i < this.list.Count; ++i)
            {
                Assert.AreEqual(list[i], array[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CopyToWrongArrayIndexThrowExpectedException()
        {
            this.list.Insert(0, 3);
            this.list.Insert(1, 5);
            this.list.Insert(1, 7);

            var array = new int[6];
            this.list.CopyTo(array, 5);

            for (int i = 0; i < this.list.Count; ++i)
            {
                Assert.AreEqual(list[i], array[i]);
            }
        }

        [TestMethod]
        public void StringListInsertionWorksRight()
        {
            Assert.AreEqual(0, this.stringList.Count);
            this.stringList.Add("one");
            Assert.AreEqual(1, this.stringList.Count);
            this.stringList.Insert(1, "two");
            Assert.AreEqual(2, this.stringList.Count);
        }

        [TestMethod]
        public void StringListRemoveWorksRight()
        {
            this.stringList.Add("one");
            this.stringList.Insert(1, "two");

            this.stringList.Remove("one");
            this.stringList.RemoveAt(0);
        }

        [TestMethod]
        public void StringListContainsWorksRight()
        {
            this.stringList.Add("one");
            this.stringList.Insert(1, "two");

            Assert.IsTrue(this.stringList.Contains("one"));
            Assert.IsFalse(this.stringList.Contains("three"));
        }
    }
}