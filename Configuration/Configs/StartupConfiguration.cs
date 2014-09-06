using Base;

namespace Configuration.Configs
{
    /// <summary>
    /// Handles configuration of items that require arguments from a command line startup
    /// </summary>
    public class StartupConfiguration : SentienceConfiguration
    {
        /// <summary>
        /// Args should be populated from command line args from the user
        /// </summary>
        public string[] args;
    }
}
