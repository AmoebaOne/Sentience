using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Base;
using Contracts.Exceptions;

namespace Contracts.Sensors
{
    /// <summary>
    /// Factories implementors of the IEffector interface
    /// </summary>
    [Export]
    public class SensorFactory
    {
        /// <summary>
        /// List of all ISensors, loaded by MEF
        /// </summary>
        [ImportMany(typeof(ISensor), AllowRecomposition = true)]
        public IEnumerable<Lazy<ISensor, ISensorMetadata>> Sensors { get; set; }

        /// <summary>
        /// Returns a list of ISensor implementers that have the certain metadata family
        /// </summary>
        /// <param name="family">The sensor family to select</param>
        /// <returns>List of ISensor implementers</returns>
        public IEnumerable<T> getSensorsByFamily<T>(SensorFamily family) where T : ISensor
        {
            try
            {
                Log.Send(Log.Level.Debug, 10047, "Factorying sensors by family", "Family: " + family.ToString());
                return Sensors.Where(e => e.Metadata.Family.Equals(family)).Select(e => e.Value).Cast<T>();
            }
            catch (Exception e)
            {
                throw new FactoryException(501,
                    new Base.Messages(
                        "Failure to select or instanciate the sensors selected, when selecting multiple by family with static type",
                        "Failed to select or construct sensors",
                        "LINQ query failed to select a sensor by family",
                        "Unable to load one of my sensors"
                        ),
                        this,
                        Base.Log.Level.Critical,
                        e);
            }
        }

        /// <summary>
        /// Returns a list of ISensor implementations that have certain metadata type
        /// </summary>
        /// <returns>List of ISensor implementers</returns>
        public IEnumerable<T> getSensorsByType<T>() where T : ISensor
        {
            try
            {
                Log.Send(Log.Level.Debug, 10048, "Factorying sensors by type", "Type: " + typeof(T).ToString());
                return Sensors.Where(e => e.Metadata.Type.Equals(typeof(T))).Select(e => e.Value).Cast<T>();
            }
            catch (Exception e)
            {
                throw new FactoryException(502,
                    new Base.Messages(
                        "Failure to select or instanciate the sensors selected, when selecting multiple by type with static type",
                        "Failed to select or construct sensors",
                        "LINQ query failed to select a sensor by type",
                        "Unable to load one of my sensors"
                        ),
                        this,
                        Base.Log.Level.Critical,
                        e);
            }
        }

        /// <summary>
        /// Override of getSensorsByType for runtime resolution
        /// Can't use above when T varies at runtime
        /// Requires using method to cast at their end
        /// </summary>
        /// <param name="t">The type to return</param>
        /// <returns>List of ISensor implementers</returns>
        public IEnumerable<ISensor> getSensorsByType(Type t)
        {
            try
            {
                Log.Send(Log.Level.Debug, 10049, "Factorying sensors by dynamic type", "Type: " + t.ToString());
                return Sensors.Where(e => e.Metadata.Type.Equals(t)).Select(e => e.Value);
            }
            catch (Exception e)
            {
                throw new FactoryException(503,
                    new Base.Messages(
                        "Failure to select or instanciate the sensors selected, when selecting multiple by type with dynamic type",
                        "Failed to select or construct sensors",
                        "LINQ query failed to select a sensor by (dynamic) type",
                        "Unable to load one of my sensors"
                        ),
                        this,
                        Base.Log.Level.Critical,
                        e);
            }
        }

        /// <summary>
        /// Returns the first ISensor that has a certain family metadata
        /// </summary>
        /// <param name="family">The family to select</param>
        /// <returns>An ISensor implementation of the specific family</returns>
        public T getSensorByFamily<T>(SensorFamily family) where T : ISensor
        {
            Log.Send(Log.Level.Debug, 10050, "Factorying sensor by family", "Family: " + family.ToString());
            Lazy<ISensor, ISensorMetadata> sensor = Sensors.FirstOrDefault(e => e.Metadata.Family.Equals(family));
            if (sensor == null)
            {
                throw new SensorFactoryItemNotFoundException(504,
                    new Base.Messages(
                        "Unable to find a sensor which matches a specified family when selecting by static type",
                        "Failed to find a sensor by family",
                        "The LINQ query for family failed to retrieve a sensor of the expected type",
                        "Unable to load one of my sensors"
                        ),
                        this,
                        Base.Log.Level.Critical
                        );
            }
            return (T)sensor.Value;
        }

        /// <summary>
        /// Reuturns the first ISensor that has a certain type metadata
        /// </summary>
        /// <returns>An ISensor implementation of the specific family</returns>
        public T getSensorByType<T>() where T : ISensor
        {
            Log.Send(Log.Level.Debug, 10051, "Factorying sensor by type", "Type: " + typeof(T).ToString());
            Lazy<ISensor, ISensorMetadata> sensor = Sensors.FirstOrDefault(e => e.Metadata.Type.Equals(typeof(T)));
            if (sensor == null)
            {
                throw new SensorFactoryItemNotFoundException(505,
                    new Base.Messages(
                        "Unable to find a sensor which matches a specified type when selecting by static type",
                        "Failed to find a sensor by type",
                        "The LINQ query for type failed to retrieve a sensor of the expected type",
                        "Unable to load one of my sensors"
                        ),
                        this,
                        Base.Log.Level.Critical
                        );
            }
            return (T)sensor.Value;
        }

        /// <summary>
        /// Override of getSensorByType for runtime resolution
        /// Can't use above when T varies at runtime
        /// Requires using method to cast at their end
        /// </summary>
        /// <param name="t">The type to return</param>
        /// <returns>An ISensor implementation of the specific family</returns>
        public ISensor getSensorByType(Type t)
        {
            Log.Send(Log.Level.Debug, 10052, "Factorying sensor by dynamic type", "Type: " + t.ToString());
            Lazy<ISensor, ISensorMetadata> sensor = Sensors.FirstOrDefault(e => e.Metadata.Type.Equals(t));
            if (sensor == null)
            {
                throw new SensorFactoryItemNotFoundException(506,
                    new Base.Messages(
                        "Unable to find a sensor which matches a specified type when selecting by dynamic type",
                        "Failed to find a sensor by (dynamic) type",
                        "The LINQ query for (dynamic) type failed to retrieve a sensor of the expected type",
                        "Unable to load one of my sensors"
                        ),
                        this,
                        Base.Log.Level.Critical
                        );
            }
            return sensor.Value;
        }
    }
}
