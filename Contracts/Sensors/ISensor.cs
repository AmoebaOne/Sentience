using System;
using Base;
using Contracts.Sensors.Events;

namespace Contracts.Sensors
{
    /// <summary>
    /// Delegate for sensor event handling
    /// </summary>
    /// <param name="args">Arguments from the event</param>
    public delegate void SensorEvent(SensorEventArgs args);

    /// <summary>
    /// Base class of sensors
    /// </summary>
    public interface ISensor : ISentience
    {
        /// <summary>
        /// Generic event for when a sensor receives a batch of data
        /// </summary>
        event SensorEvent DataReceived;

        /// <summary>
        /// Returns the type of configuration this object expects
        /// </summary>
        /// <returns>Configuration type</returns>
        Type getConfigurationType();
    }
}
