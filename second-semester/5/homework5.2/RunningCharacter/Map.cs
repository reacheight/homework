namespace RunningCharacter
{
    using System;
    using System.Collections.Generic;
    using System.IO;

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
        public Map(char[,] map)
        {
            this.map = map;
        }

        /// <summary>
        /// Gets height of map
        /// </summary>
        public int Height => this.map.GetLength(0);

        /// <summary>
        /// Gets width of map
        /// </summary>
        public int Width => this.map.GetLength(1);

        ///
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
