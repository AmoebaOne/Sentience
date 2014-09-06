using System;

namespace Contracts.Processors
{
    /// <summary>
    /// Metadata associated with an IProcessor injection
    /// </summary>
    public interface IProcessorMetadata
    {
        /// <summary>
        /// The family that this IProcessor belongs to
        /// </summary>
        ProcessorFamily Family { get; }

        /// <summary>
        /// The final type the IProcessor is implemented by
        /// </summary>
        Type Type { get; }
    }
}
