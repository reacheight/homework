﻿namespace RunningCharacter
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class that implements map
    /// </summary>
    public class Map
    {
        /// <summary>
        /// Map represented as list of strings
        /// </summary>
        private List<string> map;

        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="map">Map as list of strings</param>
        public Map(List<string> map)
        {
            this.map = map;

            if (!this.IsValid())
            {
                throw new WrongMapException();
            }
        }

        /// <summary>
        /// Gets height of a map
        /// </summary>
        public int Height => this.map.Count;

        /// <summary>
        /// Gets width of a map
        /// </summary>
        public int Width => this.map[0].Length;

        /// <summary>
        /// Gets point on a map by indexes
        /// </summary>
        /// <param name="i">Y-coordinate of point</param>
        /// <param name="j">X-coordinate of point</param>
        /// <returns>Character from the map</returns>
        public char this[int i, int j] => this.map[i][j];

        /// <summary>
        /// Prints map to console
        /// </summary>
        public void Print()
        {
            foreach (var line in this.map)
            {
                Console.WriteLine(line);
            }
        }

        /// <summary>
        /// Checks whether map is valid
        /// </summary>
        /// <returns>True if map is valid, false otherwise</returns>
        private bool IsValid()
        {
            if (this.map.Count == 0)
            {
                return false;
            }

            var width = this.map[0].Length;
            if (width == 0)
            {
                return false;
            }

            foreach (var line in this.map)
            {
                if (line.Length != width)
                {
                    return false;
                }
            }

            return this.map[1][1] == ' ';
        }
    }
}