using Base;

namespace SentientSystem
{
    /// <summary>
    /// Configuration for output of messages
    /// </summary>
    public class OutputConfiguration : SentienceConfiguration
    {
        /// <summary>
        /// Defines the types of output available
        /// </summary>
        public enum Types
        {
            /// <summary>
            /// No output type
            /// </summary>
            None,
            /// <summary>
            /// Output to the console
            /// </summary>
            Console,
            /// <summary>
            /// Output to a UI
            /// </summary>
            UI,
            /// <summary>
            /// Output to a line display on the robot
            /// </summary>
            LineDisplay
        }

        /// <summary>
        /// The type of output to use
        /// </summary>
        public Types method;
    }
}
