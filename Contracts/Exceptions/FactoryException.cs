using System;
using Base;

namespace Contracts.Exceptions
{
    /// <summary>
    /// Base exception for errors in factories
    /// </summary>
    public class FactoryException : SentienceException
    {
        /// <summary>
        /// A factory exception
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="messages">Messages</param>
        /// <param name="context">Object context</param>
        /// <param name="Level">The error level</param>
        /// <param name="exception">Optional inner exception</param>
        public FactoryException(int code, Messages messages, object context = null, Log.Level Level = Log.Level.Error, Exception exception = null) : base(code, messages, context, Level, exception) { }
    }
}
