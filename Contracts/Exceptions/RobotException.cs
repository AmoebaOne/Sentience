using System;
using Base;

namespace Contracts.Exceptions
{
    /// <summary>
    /// An exception for robot problems
    /// </summary>
    public class RobotException : SentienceException
    {
        /// <summary>
        /// RobotException
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="messages">Messages</param>
        /// <param name="context">Object context</param>
        /// <param name="Level">The error level</param>
        /// <param name="exception">Optional inner exception</param>
        public RobotException(int code, Messages messages, object context = null, Log.Level Level = Log.Level.Error, Exception exception = null) : base(code, messages, context, Level, exception) { }
    }
}
