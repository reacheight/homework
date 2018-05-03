namespace Set.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;

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
        public void ClearWorksRight()
        {
            this.emptySet.Add(23);
            this.emptySet.Add(103);

            this.emptySet.Clear();

            Assert.AreEqual(0, this.emptySet.Count);
            Assert.IsFalse(this.emptySet.Contains(23));
            Assert.IsFalse(this.emptySet.Contains(103));
        }

        [TestMethod]
        public void IsSubsetWorksRIght()
        {
            Assert.IsTrue(this.set.IsSubsetOf(this.superset));
            Assert.IsFalse(this.superset.IsSubsetOf(this.setList));
            Assert.IsTrue(this.set.IsSubsetOf(this.setList));
        }

        [TestMethod]
        public void IsSupersetWorksRIght()
        {
            Assert.IsTrue(this.superset.IsSupersetOf(this.set));
            Assert.IsFalse(this.set.IsSupersetOf(this.superset));
            Assert.IsTrue(this.superset.IsSubsetOf(this.supersetList));
        }

        [TestMethod]
        public void IsProperSubsetWorksRight()
        {
            Assert.IsTrue(this.set.IsProperSubsetOf(this.supersetList));
            Assert.IsFalse(this.superset.IsProperSubsetOf(this.setList));
            Assert.IsFalse(this.set.IsProperSubsetOf(this.setList));
        }

        [TestMethod]
        public void IsProperSupersetWorksRight()
        {
            Assert.IsTrue(this.superset.IsProperSupersetOf(this.setList));
            Assert.IsFalse(this.set.IsProperSupersetOf(this.supersetList));
            Assert.IsFalse(this.set.IsProperSubsetOf(this.setList));
        }

        [TestMethod]
        public void SetEqualsWorksRight()
        {
            Assert.IsTrue(this.set.SetEquals(this.setList));
            Assert.IsFalse(this.set.SetEquals(this.supersetList));
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

        [TestMethod]
        public void IntersectWithWorksRight()
        {
            var intersection = new List<int>();

            foreach (var item in list)
            {
                if (this.intSet.Contains(item))
                {
                    intersection.Add(item);
                }
            }

            this.intSet.IntersectWith(this.list);
            Assert.IsTrue(this.intSet.SetEquals(intersection));
        }

        [TestMethod]
        public void OverlapsWorksRight()
        {
            this.emptySet.Add(54);
            this.emptySet.Add(234);
            this.emptySet.Add(99);

            Assert.IsTrue(this.set.Overlaps(this.supersetList));
            Assert.IsFalse(this.emptySet.Overlaps(this.setList));
        }

        [TestMethod]
        public void SymmetricExceptWithWorksRight()
        {
            var result = new List<int>();
            
            foreach (var item in this.intSet)
            {
                result.Add(item);
            }

            foreach (var item in this.list)
            {
                if (result.Contains(item))
                {
                    result.Remove(item);
                }
                else
                {
                    result.Add(item);
                }
            }

            this.intSet.SymmetricExceptWith(this.list);
            Assert.IsTrue(this.intSet.SetEquals(result));
        }

        [TestMethod]
        public void UnionWithWorksRight()
        {
            var union = new List<int>();

            foreach (var item in this.intSet)
            {
                union.Add(item);
            }

            foreach (var item in this.list)
            {
                union.Add(item);
            }

            this.intSet.UnionWith(this.list);
            Assert.IsTrue(this.intSet.SetEquals(union));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExceptWithNullThrowsExcpectedException()
        {
            this.set.IntersectWith(null);
        }

        [TestMethod]
        public void CopyToWorksWithZeroStartingIndex()
        {
            var array = new int[this.intSet.Count];
            this.intSet.CopyTo(array, 0);

            Assert.IsTrue(this.intSet.SetEquals(array));
        }

        [TestMethod]
        public void CopyToWorksWithValidNonzeroStartingIndex()
        {
            var startingIndex = 2;
            var array = new int[this.intSet.Count + startingIndex + 1];
            this.intSet.CopyTo(array, startingIndex);
            foreach (var item in this.intSet)
            {
                Assert.IsTrue(item == array[startingIndex]);
                startingIndex++;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyToNullThrowsExcpectedException()
        {
            this.intSet.CopyTo(null, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CopyToWithInvalidStartingIndexThrowsExcpectedException()
        {
            var startingIndex = 4;
            var array = new int[this.intSet.Count + 2];
            this.intSet.CopyTo(array, startingIndex);
        }
    }
}