using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Base;
using Contracts.Exceptions;

namespace Contracts.Robots
{
    /// <summary>
    /// Factories implementors of the IRobot interface
    /// </summary>
    [Export]
    public class RobotFactory
    {
        /// <summary>
        /// List of all IRobots, loaded by MEF
        /// </summary>
        [ImportMany(typeof(IRobot), AllowRecomposition = true)]
        public IEnumerable<Lazy<IRobot, IRobotMetadata>> Robots { get; set; }

        /// <summary>
        /// Returns a list of IRobot implementers that have the certain metadata family
        /// </summary>
        /// <param name="family">The robot family to select</param>
        /// <returns>List of IRobot implementers</returns>
        public IEnumerable<T> getRobotsByFamily<T>(RobotFamily family) where T : IRobot
        {
            try
            {
                Log.Send(Log.Level.Debug, 10041, "Factorying robots by family", "Family: " + family.ToString());
                return Robots.Where(e => e.Metadata.Family.Equals(family)).Select(e => e.Value).Cast<T>();
            }
            catch (Exception e)
            {
                throw new FactoryException(401,
                    new Base.Messages(
                        "Failure to select or instanciate the robots selected, when selecting multiple by family with static type",
                        "Failed to select or construct robots",
                        "LINQ query failed to select a robot by family",
                        "Unable to load one of my systems"
                        ),
                        this,
                        Base.Log.Level.Critical,
                        e);
            }
        }

        /// <summary>
        /// Returns a list of IRobot implementations that have certain metadata type
        /// </summary>
        /// <returns>List of IRobot implementers</returns>
        public IEnumerable<T> getRobotsByType<T>() where T : IRobot
        {
            try
            {
                Log.Send(Log.Level.Debug, 10042, "Factorying robots by type", "Type: " + typeof(T).ToString());
                return Robots.Where(e => e.Metadata.Type.Equals(typeof(T))).Select(e => e.Value).Cast<T>();
            }
            catch (Exception e)
            {
                throw new FactoryException(402,
                    new Base.Messages(
                        "Failure to select or instanciate the robots selected, when selecting multiple by type with static type",
                        "Failed to select or construct robots",
                        "LINQ query failed to select a robot by type",
                        "Unable to load one of my systems"
                        ),
                        this,
                        Base.Log.Level.Critical,
                        e);
            }
        }

        /// <summary>
        /// Override of getRobotsByType for runtime resolution
        /// Can't use above when T varies at runtime
        /// Requires using method to cast at their end
        /// </summary>
        /// <param name="t">The type to return</param>
        /// <returns>List of IRobot implementers</returns>
        public IEnumerable<IRobot> getRobotsByType(Type t)
        {
            try
            {
                Log.Send(Log.Level.Debug, 10043, "Factorying robots by dynamic type", "Type: " + t.ToString());
                return Robots.Where(e => e.Metadata.Type.Equals(t)).Select(e => e.Value);
            }
            catch (Exception e)
            {
                throw new FactoryException(403,
                    new Base.Messages(
                        "Failure to select or instanciate the robots selected, when selecting multiple by type with dynamic type",
                        "Failed to select or construct robots",
                        "LINQ query failed to select a robot by type (dynamic)",
                        "Unable to load one of my systems"
                        ),
                        this,
                        Base.Log.Level.Critical,
                        e);
            }
        }

        /// <summary>
        /// Returns the first IRobot that has a certain family metadata
        /// </summary>
        /// <param name="family">The family to select</param>
        /// <returns>An IRobot implementation of the specific family</returns>
        public T getRobotByFamily<T>(RobotFamily family) where T : IRobot
        {
            Log.Send(Log.Level.Debug, 10044, "Factorying robot by family", "Family: " + family.ToString());
            Lazy<IRobot, IRobotMetadata> robot = Robots.FirstOrDefault(e => e.Metadata.Family.Equals(family));
            if (robot == null)
            {
                throw new RobotFactoryItemNotFoundException(404,
                    new Base.Messages(
                        "Unable to find a robot which matches a specified family when selecting by static type",
                        "Failed to find a robot by family",
                        "The LINQ query for family failed to retrieve a robot of the expected type",
                        "Unable to load one of my systems"
                        ),
                        this,
                        Base.Log.Level.Critical
                        );
            }
            return (T)robot.Value;
        }

        /// <summary>
        /// Reuturns the first IRobot that has a certain type metadata
        /// </summary>
        /// <returns>An IRobot implementation of the specific family</returns>
        public T getRobotByType<T>() where T : IRobot
        {
            Log.Send(Log.Level.Debug, 10045, "Factorying robot by type", "Type: " + typeof(T).ToString());
            Lazy<IRobot, IRobotMetadata> robot = Robots.FirstOrDefault(e => e.Metadata.Type.Equals(typeof(T)));
            if (robot == null)
            {
                throw new RobotFactoryItemNotFoundException(405,
                    new Base.Messages(
                        "Unable to find a robot which matches a specified type when selecting by static type",
                        "Failed to find a robot by type",
                        "The LINQ query for type (static) failed to retrieve a robot of the expected type",
                        "Unable to load one of my systems"
                        ),
                        this,
                        Base.Log.Level.Critical
                        );
            }
            return (T)robot.Value;
        }

        /// <summary>
        /// Override of getRobotByType for runtime resolution
        /// Can't use above when T varies at runtime
        /// Requires using method to cast at their end
        /// </summary>
        /// <param name="t">The type to return</param>
        /// <returns>An IRobot implementation of the specific family</returns>
        public IRobot getRobotByType(Type t)
        {
            Log.Send(Log.Level.Debug, 10046, "Factorying robot by dynamic type", "Type: " + t.ToString());
            Lazy<IRobot, IRobotMetadata> robot = Robots.FirstOrDefault(e => e.Metadata.Type.Equals(t));
            if (robot == null)
            {
                throw new RobotFactoryItemNotFoundException(406,
                    new Base.Messages(
                        "Unable to find a robot which matches a specified type when selecting by dynamic type",
                        "Failed to find a robot by (dynamic) type",
                        "The LINQ query for (dynamic) type failed to retrieve a robot of the expected type",
                        "Unable to load one of my systems"
                        ),
                        this,
                        Base.Log.Level.Critical
                        );
            }
            return robot.Value;
        }
    }
}
