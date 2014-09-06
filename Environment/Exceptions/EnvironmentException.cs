using System;
using Base;

namespace Environment.Exceptions
{
    /// <summary>
    /// Base exception for environment errors
    /// </summary>
    public class EnvironmentException : SentienceException
    {
        /// <summary>
        /// Environment exception
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="messages">Messages</param>
        /// <param name="context">Object context</param>
        /// <param name="Level">The error level</param>
        /// <param name="exception">Optional inner exception</param>
        public EnvironmentException(int code, Messages messages, object context = null, Log.Level Level = Log.Level.Error, Exception exception = null) : base(code, messages, context, Level, exception) { }
    }
}
