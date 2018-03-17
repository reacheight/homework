namespace RunningCharacter
{
    using System;

    public class EventLoop
    {
        /// <summary>
        /// Left arrow event
        /// </summary>
        public event EventHandler<EventArgs> LeftHandler = (sender, args) => { };

        /// <summary>
        /// Right arrow event
        /// </summary>
        public event EventHandler<EventArgs> RightHandler = (sender, args) => { };

        /// <summary>
        /// Up arrow event
        /// </summary>
        public event EventHandler<EventArgs> UpHandler = (sender, args) => { };

        /// <summary>
        /// Down arrow event
        /// </summary>
        public event EventHandler<EventArgs> DownHandler = (sender, args) => { };

        /// <summary>
        /// Start event
        /// </summary>
        public event EventHandler<EventArgs> StartHandler = (sender, args) => { };

        /// <summary>
        /// Main event loop
        /// </summary>
        public void Run()
        {
            this.StartHandler(this, EventArgs.Empty);

            while (true)
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        this.LeftHandler(this, EventArgs.Empty);
                        break;

                    case ConsoleKey.RightArrow:
                        this.RightHandler(this, EventArgs.Empty);
                        break;

                    case ConsoleKey.DownArrow:
                        this.DownHandler(this, EventArgs.Empty);
                        break;

                    case ConsoleKey.UpArrow:
                        this.UpHandler(this, EventArgs.Empty);
                        break;
                }
            }
        }
    }
}
