using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace RunningCharacter.Tests
{
    [TestClass]
    public class MapTests
    {
        [TestMethod]
        [ExpectedException(typeof(WrongMapException))]
        public void FirstWrongMapThrowExprectedException()
        {
            var map = new Map("WrongMaps/map1.txt");
        }

        [TestMethod]
        [ExpectedException(typeof(WrongMapException))]
        public void SecondWrongMapThrowExprectedException()
        {
            var map = new Map("WrongMaps/map3.txt");
        }

        [TestMethod]
        [ExpectedException(typeof(WrongMapException))]
        public void EmptyMapThrowExprectedException()
        {
            var map = new Map("WrongMaps/map2.txt");
        }

        [TestMethod]
        public void ValidMapsWillNotCrash()
        {
            var validMaps = new List<string>() {
                "validMaps/map1.txt",
                "validMaps/map2.txt",
                "validMaps/map3.txt",
                "validMaps/map4.txt",
                "validMaps/map5.txt",
            };

            foreach (var filename in validMaps)
            {
                var map = new Map(filename);
            }
        }
    }
}