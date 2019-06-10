namespace RunningCharacter
{
    /// <summary>
    /// Main class of the program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method of the program
        /// </summary>
        private static void Main(string[] args)
        {
            var eventLoop = new EventLoop();
            var game = new Game("map.txt");

            eventLoop.StartHandler += game.OnStart;
            eventLoop.LeftHandler += game.OnLeft;
            eventLoop.RightHandler += game.OnRight;
            eventLoop.DownHandler += game.OnDown;
            eventLoop.UpHandler += game.OnUp;

            eventLoop.Run();
        }
    }
}
