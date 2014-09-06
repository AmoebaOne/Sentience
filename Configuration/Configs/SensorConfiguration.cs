using System.Runtime.Serialization;
using Base;

namespace Configuration.Configs
{
    /// <summary>
    /// A configuration for a Sensor
    /// </summary>
    public class SensorConfiguration : SentienceConfiguration
    {
        // TODO: Do config

        /// <summary>
        /// Deserialise callback
        /// </summary>
        /// <param name="context">Stream context</param>
        [OnDeserialized]
        internal void Deserialized(StreamingContext context)
        {

        }
    }
}
