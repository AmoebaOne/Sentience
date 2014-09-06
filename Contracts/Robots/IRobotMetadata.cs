using System;

namespace Contracts.Robots
{
    /// <summary>
    /// Metadata associated with IRobot injection
    /// </summary>
    public interface IRobotMetadata
    {
        /// <summary>
        /// The family that this IRobot belongs to
        /// </summary>
        RobotFamily Family { get; }

        /// <summary>
        /// The final type the IRobot is implemented by
        /// </summary>
        Type Type { get; }
    }
}
