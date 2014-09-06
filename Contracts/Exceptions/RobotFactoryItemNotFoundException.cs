using System;
using Base;

namespace Contracts.Exceptions
{
    /// <summary>
    /// Exception for when robot factory cannot find requested item
    /// </summary>
    public class RobotFactoryItemNotFoundException : FactoryItemNotFoundException
    {
        /// <summary>
        /// Robot Factory Item Not Found Exception
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="messages">Messages</param>
        /// <param name="context">Object context</param>
        /// <param name="Level">The error level</param>
        /// <param name="exception">Optional inner exception</param>
        public RobotFactoryItemNotFoundException(int code, Messages messages, object context = null, Log.Level Level = Log.Level.Error, Exception exception = null) : base(code, messages, context, Level, exception) { }
    }
}
