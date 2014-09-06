using System;
using System.ComponentModel.Composition;

namespace Contracts.Processors
{
    /// <summary>
    /// Enum for the various processor types available
    /// </summary>
    public enum ProcessorFamily
    {
        /// <summary>
        /// An effector processor
        /// </summary>
        Effector,
        /// <summary>
        /// A sensor processor
        /// </summary>
        Sensor,
        /// <summary>
        /// A subsumption layer processor
        /// </summary>
        Subsumption
    }

    /// <summary>
    /// A class for metadata of processor exporting classes
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ProcessorExport : ExportAttribute
    {
        /// <summary>
        /// Constructor informs MEF we are metadata for the IProcessor export
        /// </summary>
        public ProcessorExport() : base(typeof(IProcessor)) { }

        /// <summary>
        /// Which processor type we are from the enum
        /// </summary>
        public ProcessorFamily Family { get; set; }

        /// <summary>
        /// Type which we actually are (not the interface)
        /// </summary>
        public Type Type { get; set; }
    }
}
