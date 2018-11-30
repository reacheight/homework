using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Tests
{
    [TestClass]
    public class TicTacToeGameTests
    {
        private TicTacToeGame game;

        [TestInitialize]
        public void Init()
        {
            game = new TicTacToeGame();
        }

        [TestMethod]
        public void MakeTurnWorksOnValidInput()
        {
            game.MakeTurn(1, 1);
            game.MakeTurn(0, 0);
            game.MakeTurn(0, 2);
            var gameOver = game.MakeTurn(1, 0);
            Assert.IsFalse(gameOver);

            gameOver = game.MakeTurn(2, 0);
            Assert.IsTrue(gameOver);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MakeTurnThrowsOnInvalidInput()
        {
            game.MakeTurn(-4, 2);
        }
    }
}