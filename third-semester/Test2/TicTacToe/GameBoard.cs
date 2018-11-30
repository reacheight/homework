using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class GameBoard
    {
        private string[, ] _field = new string[3, 3];

        public string this[int i, int j]
        {
            get => _field[i, j];
            set => _field[i, j] = value;
        }

        public bool IsWin(int x, int y)
            => (_field[x, 0] == _field[x, 1] && _field[x, 1] == _field[x, 2]) ||
                (_field[0, y] == _field[1, y] && _field[1, y] == _field[2, y]) ||
                (OnMainDiagonal(x, y) && _field[0, 0] == _field[1, 1] && _field[1, 1] == _field[2, 2]) ||
                (OnSideDiagonal(x, y) && _field[0, 2] == _field[1, 1] && _field[1, 1] == _field[2, 0]);

        private bool OnMainDiagonal(int x, int y)
            => x == y;

        private bool OnSideDiagonal(int x, int y)
            => (x == 0 && y == 2) || (x == 1 && y == 1) || (x == 2 && y == 0);
    }
}
