using System;
using System.ComponentModel.Composition;

namespace Contracts.Sensors
{
    /// <summary>
    /// Enum for the various sensor types available
    /// </summary>
    public enum SensorFamily
    {
        /// <summary>
        /// A camera sensor type
        /// </summary>
        Camera,
        /// <summary>
        /// A depth sensor
        /// </summary>
        Depth,
        /// <summary>
        /// An orientation sensor
        /// </summary>
        Orientation,
        /// <summary>
        /// Acceleration sensor
        /// </summary>
        Acceleration,
        /// <summary>
        /// Velocity sensor
        /// </summary>
        Velocity,
        /// <summary>
        /// Displacement sensor
        /// </summary>
        Displacement,
        /// <summary>
        /// GPS sensor
        /// </summary>
        GPS,
        /// <summary>
        /// Rotation sensor
        /// </summary>
        Rotation,
        /// <summary>
        /// Angular velocity sensor
        /// </summary>
        AngularVelocity,
        /// <summary>
        /// For when you don't know the type but need a SensorFamily. Please don't implement with this, very rarely used
        /// </summary>
        Unknown
    }

    /// <summary>
    /// A class for metadata of sensor exporting classes
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
    public class SensorExport : ExportAttribute
    {
        /// <summary>
        /// Constructor informs MEF we are metadata for the ISensor export
        /// </summary>
        public SensorExport() : base(typeof(ISensor)) { }

        /// <summary>
        /// Which sensor type we are from the enum
        /// </summary>
        public SensorFamily Family { get; set; }

        /// <summary>
        /// Type which we actually are (not the interface)
        /// </summary>
        public Type Type { get; set; }
    }
}
