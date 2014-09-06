using System;

namespace Contracts.Effectors
{
    /// <summary>
    /// Metadata associated with an IEffector injection
    /// </summary>
    public interface IEffectorMetadata
    {
        /// <summary>
        /// The family that this IEffector belongs to
        /// </summary>
        EffectorFamily Family { get; }

        /// <summary>
        /// The final type the IEffector is implemented by
        /// </summary>
        Type Type { get; }
    }
}
