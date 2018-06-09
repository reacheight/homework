namespace RunningCharacter
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class that implements a game with a running character
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Character of a game
        /// </summary>
        private Character character = new Character(1, 1);

        /// <summary>
        /// Map, where character is placed
        /// </summary>
        private Map map;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="filename">Name of the text file with map</param>
        public Game(string filename)
        {
            this.map = new Map(filename);
        }

        /// <summary>
        /// Start event handler
        /// </summary>
        /// <param name="sender">Event's sender</param>
        /// <param name="args">Event's arguments</param>
        public void OnStart(object sender, EventArgs args)
        {
            this.PrintStartState();
        }

        /// <summary>
        /// Left arrow event handler
        /// </summary>
        /// <param name="sender">Event's sender</param>
        /// <param name="args">Event's arguments</param>
        public void OnLeft(object sender, EventArgs args)
        {
            this.character.MoveLeft(this.map);
        }

        /// <summary>
        /// Right arrow event handler
        /// </summary>
        /// <param name="sender">Event's sender</param>
        /// <param name="args">Event's arguments</param>
        public void OnRight(object sender, EventArgs args)
        {
            this.character.MoveRight(this.map);
        }

        /// <summary>
        /// Down arrow event handler
        /// </summary>
        /// <param name="sender">Event's sender</param>
        /// <param name="args">Event's arguments</param>
        public void OnDown(object sender, EventArgs args)
        {
            this.character.MoveDown(this.map);
        }

        /// <summary>
        /// Up arrow event handler
        /// </summary>
        /// <param name="sender">Event's sender</param>
        /// <param name="args">Event's arguments</param>
        public void OnUp(object sender, EventArgs args)
        {
            this.character.MoveUp(this.map);
        }

        /// <summary>
        /// Print current game state to the console 
        /// </summary>
        private void PrintStartState()
        {
            this.map.Print();
            this.character.PrintOnMap(this.map);
        }
    }
}
