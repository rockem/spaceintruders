namespace Intruders
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (MarsIntruders game = new MarsIntruders())
            {
                game.Run();
            }
        }
    }
}

