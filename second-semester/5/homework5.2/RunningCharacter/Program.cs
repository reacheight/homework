namespace RunningCharacter
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class Program
    {
        private static char[,] ReadMap(string filename)
        {
            char[,] result;
            var temp = new List<char[]>();

            foreach (string line in File.ReadLines(filename))
            {
                var array = line.ToCharArray();

                temp.Add(array);
            }

            result = new char[temp.Count, temp[0].Length];
            for (var i = 0; i < temp.Count; ++i)
            {
                for (var j = 0; j < temp[0].Length; ++j)
                {
                    result[i, j] = temp[i][j];
                }
            }

            return result;
        }

        private static void Main(string[] args)
        {
            char[,] map = ReadMap("map.txt");

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
