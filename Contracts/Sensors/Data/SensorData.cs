using System;

namespace Contracts.Sensors.Data
{
    /// <summary>
    /// Base class for sensor data types
    /// </summary>
    [Serializable]
    public class SensorData
    {
        /// <summary>
        /// The raw data as it came off the depth sensor
        /// </summary>
        protected byte[] rawData;

        /// <summary>
        /// Constructor to populate
        /// </summary>
        /// <param name="raw">Raw byte data</param>
        public SensorData(byte[] raw)
        {
            rawData = raw;
        }

        /// <summary>
        /// Return the raw data
        /// </summary>
        /// <returns>Byte array raw data</returns>
        public byte[] asRaw()
        {
            return rawData;
        }
    }
}
