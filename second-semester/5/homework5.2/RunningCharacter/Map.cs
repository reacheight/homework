namespace RunningCharacter
{
    using System;

    /// <summary>
    /// Class that implements map
    /// </summary>
    public class Map
    {
        /// <summary>
        /// Map represented as 2D array of chars
        /// </summary>
        private char[,] map;

        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="map">2D array of chars, map[1, 1] should be equal to ' '"</param>
        public Map(char[,] map)
        {
            this.map = map;

            if (map[1, 1] != ' ')
            {
                throw new WrongMapException("Второй символ второй строчки карты должен равняться ' '");
            }
        }

        /// <summary>
        /// Gets height of map
        /// </summary>
        public int Height => this.map.GetLength(0);

        /// <summary>
        /// Gets width of map
        /// </summary>
        public int Width => this.map.GetLength(1);

        /// <summary>
        /// Gets point on a map by indexes
        /// </summary>
        /// <param name="i">Y-coordinate of point</param>
        /// <param name="j">X-coordinate of point</param>
        /// <returns>Character from the map</returns>
        public char this[int i, int j]
        {
            get
            {
                return this.map[i, j];
            }
        }

        /// <summary>
        /// Prints map to console
        /// </summary>
        public void Print()
        {
            for (var i = 0; i < this.map.GetLength(0); ++i)
            {
                for (var j = 0; j < this.map.GetLength(1); ++j)
                {
                    Console.Write(this.map[i, j]);
                }

                Console.WriteLine();
            }
        }
    }
}
