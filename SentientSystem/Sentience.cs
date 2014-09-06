using System;
using Configuration.Configs;

namespace SentientSystem
{
    /// <summary>
    /// The main startup point for Sentience
    /// </summary>
    class Sentience
    {
        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args">Startup args</param>
        static void Main(string[] args)
        {
            // TODO: Figure out some kind of restarting behaviour
            SentienceManager sm = new SentienceManager();

            // Delegate to clean up
            CleanUp cleanUp = delegate
            {
                // Cleans up the manager and everything in it
                sm.Deactivate();
            };

            // Be prepared to handle Ctrl+C
            Console.CancelKeyPress += delegate { cleanUp(); };

            // Configure the manager
            StartupConfiguration sc = new StartupConfiguration();
            sc.args = args;
            sm.Configure(sc);

            // Initialise the manager and system
            sm.Initialise();

            string text = "";
            while (text != "exit")
            {
                text = Console.ReadLine();
            }

            cleanUp();
        }

        /// <summary>
        /// A delegate to handle cleaning up when exited
        /// </summary>
        delegate void CleanUp();
    }
}
