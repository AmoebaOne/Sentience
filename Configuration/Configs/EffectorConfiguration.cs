using System.Runtime.Serialization;
using Base;

namespace Configuration.Configs
{
    /// <summary>
    /// A configuration for an effector
    /// </summary>
    public class EffectorConfiguration : SentienceConfiguration
    {
        // TODO: Create effector configuration

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
