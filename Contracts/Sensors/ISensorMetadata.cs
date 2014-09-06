using System;

namespace Contracts.Sensors
{
    /// <summary>
    /// Metadata associated with an ISensor injection
    /// </summary>
    public interface ISensorMetadata
    {
        /// <summary>
        /// The family that this ISensor belongs to
        /// </summary>
        SensorFamily Family { get; }

        /// <summary>
        /// The final type the ISensor is implemented by
        /// </summary>
        Type Type { get; }
    }
}
