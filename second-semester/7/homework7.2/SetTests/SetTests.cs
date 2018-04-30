using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Set.Tests
{
    [TestClass]
    public class SetTests
    {
        private Set<int> emptySet;
        private Set<string> stringSet;
        private Set<int> intSet;
        private List<int> list;

        private List<int> setList;
        private Set<int> set;
        private List<int> supersetList;
        private Set<int> superset;

        [TestInitialize]
        public void Init()
        {
            this.emptySet = new Set<int>();
            this.stringSet = new Set<string>();
            this.intSet = new Set<int>(new List<int> { 1, 3, 5, 4, 6, 23 });
            this.list = new List<int> { 1, 3, 5, 7, 8, 19 };

            this.setList = new List<int>() { 1, 3, 5, 7, 9 };
            this.set = new Set<int>(setList);
            this.supersetList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            this.superset = new Set<int>(supersetList);
        }

        [TestMethod]
        public void SizeOfEmptySetIsZero()
        {
            Assert.AreEqual(0, this.emptySet.Count);
        }

        [TestMethod]
        public void AdditionStoresItem()
        {
            this.emptySet.Add(3);

            Assert.AreEqual(1, this.emptySet.Count);
        }

        [TestMethod]
        public void ItemCanBeRemovedAfterAddition()
        {
            this.emptySet.Add(3);

            Assert.IsTrue(this.emptySet.Remove(3));
            Assert.AreEqual(0, this.emptySet.Count);
        }

        [TestMethod]
        public void ItemCantBeAddedTwice()
        {
            Assert.IsTrue(this.emptySet.Add(3));
            Assert.IsFalse(this.emptySet.Add(3));

            Assert.AreEqual(1, this.emptySet.Count);
        }

        [TestMethod]
        public void RemovingItemThatIsNotInSetReturnsFalse()
        {
            Assert.IsFalse(this.emptySet.Remove(3));
        }

        [TestMethod]
        public void RemovingItemTwiceWillNotCrash()
        {
            this.emptySet.Add(3);

            Assert.IsTrue(this.emptySet.Remove(3));
            Assert.IsFalse(this.emptySet.Remove(3));
        }

        [TestMethod]
        public void ContainsWorksRight()
        {
            this.emptySet.Add(3);

            Assert.IsTrue(this.emptySet.Contains(3));
            Assert.IsFalse(this.emptySet.Contains(23));
        }

        [TestMethod]
        public void SetDoesNotContainItemAfterRemovingIt()
        {
            this.emptySet.Add(3);
            this.emptySet.Remove(3);

            Assert.IsFalse(this.emptySet.Contains(3));
        }

        [TestMethod]
        public void MultipleAdditionWorksRight()
        {
            this.emptySet.Add(3);
            this.emptySet.Add(5);
            this.emptySet.Add(23);

            Assert.AreEqual(3, this.emptySet.Count);
            Assert.IsTrue(this.emptySet.Contains(3));
            Assert.IsTrue(this.emptySet.Contains(5));
            Assert.IsTrue(this.emptySet.Contains(23));
        }

        [TestMethod]
        public void MultipleRemovingWorksRight()
        {
            this.emptySet.Add(3);
            this.emptySet.Add(5);
            this.emptySet.Add(234);
            this.emptySet.Add(23);

            this.emptySet.Remove(3);
            this.emptySet.Remove(234);
            this.emptySet.Remove(23);

            Assert.AreEqual(1, this.emptySet.Count);
            Assert.IsFalse(this.emptySet.Contains(3));
            Assert.IsFalse(this.emptySet.Contains(234));
            Assert.IsFalse(this.emptySet.Contains(23));
        }

        [TestMethod]
        public void EnumeratorWorksRight()
        {
            int count = 0;
            foreach (var item in this.intSet)
            {
                count++;
                Assert.IsTrue(this.intSet.Contains(item));
            }

            Assert.AreEqual(this.intSet.Count, count);
        }

        [TestMethod]
        public void IsSubsetWorksRIght()
        {
            Assert.IsTrue(set.IsSubsetOf(superset));
            Assert.IsFalse(superset.IsSubsetOf(setList));
            Assert.IsTrue(set.IsSubsetOf(setList));
        }

        [TestMethod]
        public void IsSupersetWorksRIght()
        {
            Assert.IsTrue(superset.IsSupersetOf(set));
            Assert.IsFalse(set.IsSupersetOf(superset));
            Assert.IsTrue(superset.IsSubsetOf(supersetList));
        }

        [TestMethod]
        public void ExceptWithWorksRight()
        {
            this.intSet.ExceptWith(this.list);

            foreach (var item in this.list)
            {
                Assert.IsFalse(this.intSet.Contains(item));
            }
        }
    }
}