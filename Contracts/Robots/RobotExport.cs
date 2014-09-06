using System;
using System.ComponentModel.Composition;

namespace Contracts.Robots
{
    /// <summary>
    /// Robot types
    /// </summary>
    public enum RobotFamily
    {
        /// <summary>
        /// A mobile robot type
        /// </summary>
        Mobile,
        /// <summary>
        /// A static robot type
        /// </summary>
        Static,
        /// <summary>
        /// Either a mobile or static robot (doesn't matter)
        /// </summary>
        Any
    }

    /// <summary>
    /// Metadata class for robot exports
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
    public class RobotExport : ExportAttribute, IRobotMetadata
    {
        /// <summary>
        /// MEF knows we are metadata for IRobot
        /// </summary>
        public RobotExport() : base(typeof(IRobot)) { }

        /// <summary>
        /// Which robot type from enum
        /// </summary>
        public RobotFamily Family { get; set; }

        /// <summary>
        /// Type which we actually are (not the interface)
        /// </summary>
        public Type Type { get; set; }
    }
}
