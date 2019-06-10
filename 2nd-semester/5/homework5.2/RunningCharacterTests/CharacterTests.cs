using Microsoft.VisualStudio.TestTools.UnitTesting;
using RunningCharacter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningCharacter.Tests
{
    [TestClass]
    public class CharacterTests
    {
        private Character character;
        private Map map;

        [TestInitialize]
        public void Init()
        {
            this.character = new Character(1, 1);
        }

        [TestMethod]
        public void CharacterCanNotMoveOutsideMap()
        {
            this.map = new Map("validMaps/map1.txt");
            var size = Math.Max(this.map.Height, this.map.Width) + 10;

            for (int i = 0; i < size; ++i)
            {
                this.character.MoveDown(this.map);
            }

            Assert.AreEqual(1, this.character.X);
            Assert.AreEqual(this.map.Height - 1, this.character.Y);

            for (int i = 0; i < size; ++i)
            {
                this.character.MoveRight(this.map);
            }

            Assert.AreEqual(this.map.Width - 1, this.character.X);
            Assert.AreEqual(this.map.Height - 1, this.character.Y);
        }

        [TestMethod]
        public void CharacterMovesRight()
        {
            this.map = new Map("validMaps/map3.txt");

            Assert.IsFalse(this.character.MoveUp(this.map));
            Assert.IsFalse(this.character.MoveDown(this.map));
            this.character.MoveLeft(this.map);
            Assert.IsFalse(this.character.MoveLeft(this.map));

            for (int i = 0; i < this.map.Width - 1; ++i)
            {
                Assert.IsTrue(this.character.MoveRight(this.map));
            }

            Assert.IsTrue(this.character.MoveDown(this.map));
            Assert.IsTrue(this.character.MoveDown(this.map));
            Assert.IsFalse(this.character.MoveDown(this.map));
        }
    }
}