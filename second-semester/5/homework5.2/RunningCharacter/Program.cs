namespace RunningCharacter
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class Program
    {
        private static List<string> ReadMap(string filename)
        {
            var result = new List<string>();
            foreach (string line in File.ReadLines(filename))
            {
                result.Add(line);
            }

            return result;
        }

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
