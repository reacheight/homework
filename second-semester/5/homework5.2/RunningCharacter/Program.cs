namespace RunningCharacter
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Main class of the program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Reads map from the text file
        /// </summary>
        /// <param name="filename">Name of the text file</param>
        /// <returns>Map as list of strings</returns>
        private static List<string> ReadMap(string filename)
        {
            var result = new List<string>();
            foreach (string line in File.ReadLines(filename))
            {
                result.Add(line);
            }

            return result;
        }

        /// <summary>
        /// Main method of the program
        /// </summary>
        private static void Main(string[] args)
        {
            var map = ReadMap("map.txt");

            var eventLoop = new EventLoop();
            var game = new Game(map);

            eventLoop.StartHandler += game.OnStart;
            eventLoop.LeftHandler += game.OnLeft;
            eventLoop.RightHandler += game.OnRight;
            eventLoop.DownHandler += game.OnDown;
            eventLoop.UpHandler += game.OnUp;

            eventLoop.Run();
        }
    }
}
