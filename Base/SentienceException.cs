using System;

namespace Base
{
    /// <summary>
    /// Base exception for Sentience
    /// </summary>
    public class SentienceException : Exception
    {
        /// <summary>
        /// Exception messages
        /// </summary>
        public Messages Messages { get; private set; }

        /// <summary>
        /// An optional inner system type exception (should not be a SentienceException)
        /// </summary>
        public Exception SubException { get; private set; }

        /// <summary>
        /// Constructs (new-style)
        /// </summary>
        /// <param name="code">The exception code</param>
        /// <param name="messages">The exception messages</param>
        /// <param name="context">The context object</param>
        /// <param name="Level">The error level (defaults to error)</param>
        public SentienceException(int code, Messages messages, object context = null, Log.Level Level = Log.Level.Error, Exception exception = null)
            : base(messages.Full)
        {
            Messages = messages;
            SubException = exception;
            Log.Send(Level, code, messages.Summary, messages.Full, context, this);
        }
    }
}
