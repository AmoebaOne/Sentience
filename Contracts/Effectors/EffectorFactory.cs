using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Base;
using Contracts.Exceptions;

namespace Contracts.Effectors
{
    /// <summary>
    /// Factories implementors of the IEffector interface
    /// </summary>
    [Export]
    public class EffectorFactory
    {
        /// <summary>
        /// List of all IEffectors, loaded by MEF
        /// </summary>
        [ImportMany(typeof(IEffector), AllowRecomposition = true)]
        public IEnumerable<Lazy<IEffector, IEffectorMetadata>> Effectors { get; set; }

        /// <summary>
        /// Returns a list of IEffector implementers that have the certain metadata family
        /// </summary>
        /// <param name="family">The effector family to select</param>
        /// <returns>List of IEffector implementers</returns>
        public IEnumerable<T> getEffectorsByFamily<T>(EffectorFamily family) where T : IEffector
        {
            try
            {
                Log.Send(Log.Level.Debug, 10020, "Factorying effectors by family", "Family: " + family.ToString());
                return Effectors.Where(e => e.Metadata.Family.Equals(family)).Select(e => e.Value).Cast<T>();
            }
            catch (Exception e)
            {
                throw new FactoryException(201,
                    new Base.Messages(
                        "Failure to select or instanciate the effectors selected, when selecting multiple by family with static type",
                        "Failed to select or construct effectors",
                        "LINQ query failed to select an effector by family",
                        "Unable to load one of my outputs"
                        ),
                        this,
                        Base.Log.Level.Critical,
                        e);
            }
        }

        /// <summary>
        /// Returns a list of IEffector implementations that have certain metadata type
        /// </summary>
        /// <returns>List of IEffector implementers</returns>
        public IEnumerable<T> getEffectorsByType<T>() where T : IEffector
        {
            try
            {
                Log.Send(Log.Level.Debug, 10021, "Factorying effectors by type", "Type: " + typeof(T).ToString());
                return Effectors.Where(e => e.Metadata.Type.Equals(typeof(T))).Select(e => e.Value).Cast<T>();
            }
            catch (Exception e)
            {
                throw new FactoryException(202,
                    new Base.Messages(
                        "Failed to select or instanciate the effectors selected, when selecting multiple by type with static type",
                        "Failed to select or construct effectors",
                        "LINQ query failed to select an effector by type",
                        "Unable to load one of my outputs"
                        ),
                        this,
                        Base.Log.Level.Critical,
                        e);
            }
        }

        /// <summary>
        /// Override of getEffectorsByType for runtime resolution
        /// Can't use above when T varies at runtime
        /// Requires using method to cast at their end
        /// </summary>
        /// <param name="t">The type to return</param>
        /// <returns>List of IEffector implementers</returns>
        public IEnumerable<IEffector> getEffectorsByType(Type t)
        {
            try
            {
                Log.Send(Log.Level.Debug, 10022, "Factorying effectors by dynamic type", "Type: " + t.ToString());
                return Effectors.Where(e => e.Metadata.Type.Equals(t)).Select(e => e.Value);
            }
            catch (Exception e)
            {
                throw new FactoryException(203,
                    new Base.Messages(
                        "Unable to select or instanciate the effectors selected, when selecting by type specified at runtime",
                        "Failed to select or construct effectors",
                        "LINQ query failed to select an effector by type (specified at runtime)",
                        "Unable to load one of my outputs"
                        ),
                        this,
                        Base.Log.Level.Critical,
                        e);
            }
        }

        /// <summary>
        /// Returns the first IEffector that has a certain family metadata
        /// </summary>
        /// <param name="family">The family to select</param>
        /// <returns>An IEffector implementation of the specific family</returns>
        public T getEffectorByFamily<T>(EffectorFamily family) where T : IEffector
        {
            Log.Send(Log.Level.Debug, 10023, "Factorying effector by family", "Family: " + family.ToString());
            Lazy<IEffector, IEffectorMetadata> effector = Effectors.FirstOrDefault(e => e.Metadata.Family.Equals(family));
            if (effector == null)
            {
                throw new EffectorFactoryItemNotFoundException(204,
                    new Base.Messages(
                        "Unable to find an effector which matches a specified family when selecting by static type",
                        "Failed to find an effector by family",
                        "The LINQ query for family failed to retrieve an effector of the expected type",
                        "Unable to load one of my outputs"
                        ),
                        this,
                        Base.Log.Level.Critical
                        );
            }
            return (T)effector.Value;
        }

        /// <summary>
        /// Reuturns the first IEffector that has a certain type metadata
        /// </summary>
        /// <returns>An IEffector implementation of the specific family</returns>
        public T getEffectorByType<T>() where T : IEffector
        {
            Log.Send(Log.Level.Debug, 10024, "Factorying effector by type", "Type: " + typeof(T).ToString());
            Lazy<IEffector, IEffectorMetadata> effector = Effectors.FirstOrDefault(e => e.Metadata.Type.Equals(typeof(T)));
            if (effector == null)
            {
                throw new EffectorFactoryItemNotFoundException(205,
                    new Base.Messages(
                        "Unable to find an effector which matches a specified type when selecting by static type",
                        "Failed to find an effector by type",
                        "The LINQ query for type failed to retrieve an effector of the expected type",
                        "Unable to load one of my outputs"
                        ),
                        this,
                        Base.Log.Level.Critical
                        );
            }
            return (T)effector.Value;
        }

        /// <summary>
        /// Override of getEffectorByType for runtime resolution
        /// Can't use above when T varies at runtime
        /// Requires using method to cast at their end
        /// </summary>
        /// <param name="t">The type to return</param>
        /// <returns>An IEffector implementation of the specific family</returns>
        public IEffector getEffectorByType(Type t)
        {
            Log.Send(Log.Level.Debug, 10025, "Factorying effector by dynamic type", "Type: " + t.ToString());
            Lazy<IEffector, IEffectorMetadata> effector = Effectors.FirstOrDefault(e => e.Metadata.Type.Equals(t));
            if (effector == null)
            {
                throw new EffectorFactoryItemNotFoundException(206,
                    new Base.Messages(
                        "Unable to find an effector which maches a specified type when selecting by dynamic type",
                        "Failed to find an effector by type",
                        "The LINQ query for type failed to retrieve an effector of the expected type, specified dynamically",
                        "Unable to load one of my outputs"
                        ),
                        this,
                        Base.Log.Level.Critical
                        );
            }
            return effector.Value;
        }
    }
}
