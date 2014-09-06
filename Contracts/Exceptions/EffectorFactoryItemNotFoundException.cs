using System;
using Base;

namespace Contracts.Exceptions
{
    /// <summary>
    /// Exception for when an effector factory cannot find a requested item
    /// </summary>
    public class EffectorFactoryItemNotFoundException : FactoryItemNotFoundException
    {
        /// <summary>
        /// Effector factory item not found exception
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="messages">Messages</param>
        /// <param name="context">Object context</param>
        /// <param name="Level">The error level</param>
        /// <param name="exception">Optional inner exception</param>
        public EffectorFactoryItemNotFoundException(int code, Messages messages, object context = null, Log.Level Level = Log.Level.Error, Exception exception = null) : base(code, messages, context, Level, exception) { }
    }
}
