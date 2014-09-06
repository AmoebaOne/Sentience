using System;
using Base;

namespace Environment.Exceptions
{
    /// <summary>
    /// An invalid dimension has been provided in a coordinate
    /// </summary>
    public class InvalidDimensionCoordinateException : CoordinateException
    {
        /// <summary>
        /// Invalid Dimension Coordinate Exception
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="messages">Messages</param>
        /// <param name="context">Object context</param>
        /// <param name="Level">The error level</param>
        /// <param name="exception">Optional inner exception</param>
        public InvalidDimensionCoordinateException(int code, Messages messages, object context = null, Log.Level Level = Log.Level.Error, Exception exception = null) : base(code, messages, context, Level, exception) { }
    }
}
