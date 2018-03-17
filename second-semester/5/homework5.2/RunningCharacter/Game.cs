namespace RunningCharacter
{
    using System;

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
        /// <param name="map">Map, where character is placed</param>
        public Game(char[,] map)
        {
            this.map = new Map(map);
        }

        public void OnStart(object sender, EventArgs args)
        {
            this.PrintStartState();
        }

        public void OnLeft(object sender, EventArgs args)
        {
            this.character.Move(this.character.X - 1, this.character.Y, this.map);
        }

        public void OnRight(object sender, EventArgs args)
        {
            this.character.Move(this.character.X + 1, this.character.Y, this.map);
        }

        public void OnDown(object sender, EventArgs args)
        {
            this.character.Move(this.character.X, this.character.Y + 1, this.map);
        }

        public void OnUp(object sender, EventArgs args)
        {
            this.character.Move(this.character.X, this.character.Y - 1, this.map);
        }

        private void PrintStartState()
        {
            this.map.Print();
            this.character.Move(this.character.X, this.character.Y, this.map);
        }
    }
}
