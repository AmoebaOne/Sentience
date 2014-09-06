
namespace Base
{
    /// <summary>
    /// Configuration for the logging
    /// </summary>
    public class LogConfiguration : SentienceConfiguration
    {
        /// <summary>
        /// The target types
        /// </summary>
        public Log.Target[] Targets;

        /// <summary>
        /// The logfile
        /// </summary>
        public string Logfile;

        /// <summary>
        /// The log levels to output
        /// </summary>
        public Log.Level[] Levels;
    }
}
