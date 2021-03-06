﻿namespace RunningCharacter
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
        private const char Symbol = '@';

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
        /// Gets X-coordinate of a character in the console, ((0, 0) is top left corner)
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Gets Y-coordinate of a character in the console
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// Move right
        /// </summary>
        /// <param name="map">Map that character is placed at</param>
        /// <returns>True if move has been done, false otherwise</returns>
        public bool MoveRight(Map map)
        {
            return this.Move(this.X + 1, this.Y, map);
        }

        /// <summary>
        /// Move left
        /// </summary>
        /// <param name="map">Map that character is placed at</param>
        /// <returns>True if move has been done, false otherwise</returns>
        public bool MoveLeft(Map map)
        {
            return this.Move(this.X - 1, this.Y, map);
        }

        /// <summary>
        /// Move up
        /// </summary>
        /// <param name="map">Map that character is placed at</param>
        /// <returns>True if move has been done, false otherwise</returns>
        public bool MoveUp(Map map)
        {
            return this.Move(this.X, this.Y - 1, map);
        }

        /// <summary>
        /// Move down
        /// </summary>
        /// <param name="map">Map that character is placed at</param>
        /// <returns>True if move has been done, false otherwise</returns>
        public bool MoveDown(Map map)
        {
            return this.Move(this.X, this.Y + 1, map);
        }

        /// <summary>
        /// Print character at the map
        /// </summary>
        /// <param name="map">Map that character is placed at</param>
        /// <returns>True if character was printed, false otherwise</returns>
        public bool PrintOnMap(Map map)
        {
            return this.Move(this.X, this.Y, map);
        }

        /// <summary>
        /// Move character
        /// </summary>
        /// <param name="x">X-coordinate of new position</param>
        /// <param name="y">Y-coordinate of new position</param>
        /// <param name="map">Map, where character is placed</param>
        /// <returns>True if move has been done, false otherwise</returns>
        private bool Move(int x, int y, Map map)
        {
            if (this.CanReachPoint(x, y, map))
            {
                this.Clear();
                this.Print(x, y);
                this.UpdateСoordinates();

                return true;
            }

            return false;
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
            Console.Write(Symbol);
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
