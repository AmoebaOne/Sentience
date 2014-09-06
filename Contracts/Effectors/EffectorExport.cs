using System;
using System.ComponentModel.Composition;

namespace Contracts.Effectors
{
    /// <summary>
    /// Enum of effector types
    /// </summary>
    public enum EffectorFamily
    {
        /// <summary>
        /// An effector capable of holonomic motion
        /// </summary>
        HolonomicMotion,
        /// <summary>
        /// An effector capable only of non holonomic motion
        /// </summary>
        NonHolonomicMotion,
        /// <summary>
        /// An effector capable of planar motion
        /// </summary>
        Planar,
        /// <summary>
        /// For when you don't know the type (rarely used, please don't implement with this)
        /// </summary>
        Unknown
    }

    /// <summary>
    /// Metadata class for effector exports
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
    public class EffectorExport : ExportAttribute, IEffectorMetadata
    {
        /// <summary>
        /// Inform MEF we are metadata for IEffector
        /// </summary>
        public EffectorExport() : base(typeof(IEffector)) { }

        /// <summary>
        /// Which effector type we are from the enum
        /// </summary>
        public EffectorFamily Family { get; set; }

        /// <summary>
        /// Type which we actually are (not the interface)
        /// </summary>
        public Type Type { get; set; }
    }
}
