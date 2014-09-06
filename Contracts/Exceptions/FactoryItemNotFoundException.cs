using System;
using Base;

namespace Contracts.Exceptions
{
    /// <summary>
    /// Base exception for when factory items that are requested cant be found
    /// </summary>
    public class FactoryItemNotFoundException : FactoryException
    {
        /// <summary>
        /// Factory Item Not Found Exception
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="messages">Messages</param>
        /// <param name="context">Object context</param>
        /// <param name="Level">The error level</param>
        /// <param name="exception">Optional inner exception</param>
        public FactoryItemNotFoundException(int code, Messages messages, object context = null, Log.Level Level = Log.Level.Error, Exception exception = null) : base(code, messages, context, Level, exception) { }
    }
}
