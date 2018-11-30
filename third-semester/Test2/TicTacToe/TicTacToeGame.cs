using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class TicTacToeGame
    {
        private int _turn = 1;
        public string CurrentTurn => _turn == 0 ? "O" : "X";
        private GameBoard _board = new GameBoard();

        public bool MakeTurn(int x, int y)
        {
            if (x < 0 || x > 2 || y < 0 || y > 2 || _board[x, y] != null)
            {
                throw new ArgumentOutOfRangeException();
            }

            _board[x, y] = CurrentTurn;
            _turn = (_turn + 1) % 2;

            if (_board.IsWin(x, y))
            {
                Restart();
            }

            return _board.IsWin(x, y);
        }

        public void Restart()
        {
            _board = new GameBoard();
            _turn = 1;
        }
    }
}
