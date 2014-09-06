using System;
using Base;
using Configuration;

namespace Contracts.Robots
{
    /// <summary>
    /// Base interface for a robot, i.e. a collection of sensors and effectors
    /// </summary>
    public interface IRobot : ISentience
    {
        /// <summary>
        /// Pauses the robot's operation
        /// </summary>
        void Pause();

        /// <summary>
        /// Resumes the robot's operations from a paused state
        /// </summary>
        void Resume();

        /// <summary>
        /// Returns the type of the configuration required
        /// </summary>
        /// <returns>The type of the configuration expected</returns>
        Type getConfigurationType();

        /// <summary>
        /// Provides the robot with the global configuration file
        /// </summary>
        /// <param name="configurator">The global configuration system</param>
        void giveGlobalConfiguration(Configurator configurator);
    }
}
