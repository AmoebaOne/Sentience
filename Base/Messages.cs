
namespace Base
{
    /// <summary>
    /// Holds a series of string messages for various targets
    /// </summary>
    public class Messages
    {
        /// <summary>
        /// Messages for the end user (very user friendly)
        /// </summary>
        public string User { get; private set; }

        /// <summary>
        /// Messages for the developer (may be outside organisation, e.g. plugin developer)
        /// </summary>
        public string Developer { get; private set; }

        /// <summary>
        /// Headline for the log file
        /// </summary>
        public string Summary { get; private set; }

        /// <summary>
        /// All the (potentially gory) details
        /// </summary>
        public string Full { get; private set; }

        /// <summary>
        /// Construct with all the details
        /// </summary>
        /// <param name="Full">The full error details (hold nothing back)</param>
        /// <param name="Summary">The headline for the log file entry</param>
        /// <param name="Developer">Message for the developer</param>
        /// <param name="User">Message for the end user</param>
        public Messages(string Full, string Summary, string Developer, string User)
        {
            this.Full = Full;
            this.Summary = Summary;
            this.Developer = Developer;
            this.User = User;
        }
    }
}
