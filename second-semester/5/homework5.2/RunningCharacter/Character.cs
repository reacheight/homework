namespace RunningCharacter
{
    using System;

    /// <summary>
    /// Class that implements character of a game
    /// </summary>
    public class Character
    {
        /// <summary>
        /// Character's avatar that will bew printed in the console
        /// </summary>
        private readonly char symbol = '@';

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        public Character() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        /// <param name="x">X-coordinate of the character's starting point</param>
        /// <param name="y">Y-coordinate of the character's starting point</param>
        public Character(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        /// <param name="x">X-coordinate of the character's starting point</param>
        /// <param name="y">Y-coordinate of the character's starting point</param>
        /// <param name="symbol">Avatar of a character</param>
        public Character(int x, int y, char symbol)
        {
            this.X = x;
            this.Y = y;
            this.symbol = symbol;
        }

        /// <summary>
        /// Gets X-coordinate of a character in the console, ((0, 0) is top left corner)
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Gets Y-coordinate of a character in the console
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// Move character
        /// </summary>
        /// <param name="x">X-coordinate of new position</param>
        /// <param name="y">Y-coordinate of new position</param>
        /// <param name="map">Map, where character is placed</param>
        public void Move(int x, int y, Map map)
        {
            if (this.CanReachPoint(x, y, map))
            {
                this.Clear();
                this.Print(x, y);
                this.UpdateСoordinates();
            }
        }

        /// <summary>
        /// Clear character from the console
        /// </summary>
        private void Clear()
        {
            Console.Write(' ');
        }

        /// <summary>
        /// Print character to the console
        /// </summary>
        /// <param name="x">X-coordinate of position, where character will by printed</param>
        /// <param name="y">Y-coordinate of position, where character will by printed</param>
        private void Print(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(this.symbol);
            Console.SetCursorPosition(x, y);
        }

        /// <summary>
        /// Indicates whether point on the map can be reached by character
        /// </summary>
        /// <param name="x">X-coordinate of a point</param>
        /// <param name="y">Y-coordinate of a point</param>
        /// <param name="map">Map, where character is placed</param>
        /// <returns>True, if point can be reached, false otherwise</returns>
        private bool CanReachPoint(int x, int y, Map map) => Math.Abs(x - this.X) <= 1 && Math.Abs(y - this.Y) <= 1 &&
                                                             x >= 0 && y >= 0 && x < map.Width && y < map.Height &&
                                                             map[y, x] == ' ';

        /// <summary>
        /// Update character's coordinates
        /// </summary>
        private void UpdateСoordinates()
        {
            this.X = Console.CursorLeft;
            this.Y = Console.CursorTop;
        }
    }
}
