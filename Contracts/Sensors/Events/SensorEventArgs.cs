﻿using Contracts.Sensors.Data;

namespace Contracts.Sensors.Events
{
    /// <summary>
    /// Base class for sensor event args
    /// </summary>
    public class SensorEventArgs : SentienceEventArgs
    {
        /// <summary>
        /// The sensor the event was generated by
        /// </summary>
        public ISensor Sensor;
        
        /// <summary>
        /// The data from the sensor
        /// </summary>
        public SensorData Data;

        /// <summary>
        /// Constructs
        /// </summary>
        /// <param name="sensor">The sensor the event was from</param>
        /// <param name="data">The event data</param>
        public SensorEventArgs(ISensor sensor, SensorData data)
        {
            Sensor = sensor;
            Data = data;
        }

        /// <summary>
        /// Allows for the overriding of the sensor that generated this args
        /// </summary>
        /// <param name="sensor">The new sensor</param>
        public void overrideSensor(ISensor sensor)
        {
            this.Sensor = sensor;
        }
    }
}
