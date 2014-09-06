using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Base;
using Contracts.Exceptions;

namespace Contracts.Processors
{
    /// <summary>
    /// Factories implementors of the IProcessor interface
    /// </summary>
    [Export]
    public class ProcessorFactory
    {
        /// <summary>
        /// List of all IProcessors, loaded by MEF
        /// </summary>
        [ImportMany(typeof(IProcessor), AllowRecomposition=true)]
        public IEnumerable<Lazy<IProcessor, IProcessorMetadata>> Processors { get; set; }

        /// <summary>
        /// Returns a list of IProcessor implementers that have the certain metadata family
        /// </summary>
        /// <param name="family">The processor family to select</param>
        /// <returns>List of IProcessor implementers</returns>
        public IEnumerable<T> getProcessorsByFamily<T>(ProcessorFamily family) where T : IProcessor
        {
            try
            {
                Log.Send(Log.Level.Debug, 10029, "Factorying processors by family", "Family: " + family.ToString());
                return Processors.Where(e => e.Metadata.Family.Equals(family)).Select(e => e.Value).Cast<T>();
            }
            catch (Exception e)
            {
                throw new FactoryException(301,
                    new Base.Messages(
                        "Failure to select or instanciate the processors selected, when selecting multiple by family with static type",
                        "Failed to select or construct processors",
                        "LINQ query failed to select a processor by family",
                        "Unable to load one of my processors"
                        ),
                        this,
                        Base.Log.Level.Critical,
                        e);
            }
        }

        /// <summary>
        /// Returns a list of IProcessor implementations that have certain metadata type
        /// </summary>
        /// <returns>List of IProcessor implementers</returns>
        public IEnumerable<T> getProcessorsByType<T>() where T : IProcessor
        {
            try
            {
                Log.Send(Log.Level.Debug, 10030, "Factorying processors by type", "Type: " + typeof(T).ToString());
                return Processors.Where(e => e.Metadata.Type.Equals(typeof(T))).Select(e => e.Value).Cast<T>();
            }
            catch (Exception e)
            {
                throw new FactoryException(302,
                    new Base.Messages(
                        "Failure to select or instanciate the processors selected, when selecting multiple by type with static type",
                        "Failed to select or construct processors",
                        "LINQ query failed to select a processor by type, static type",
                        "Unable to load one of my outputs"
                        ),
                        this,
                        Base.Log.Level.Critical,
                        e);
            }
        }

        /// <summary>
        /// Override of getProcessorsByType for runtime resolution
        /// Can't use above when T varies at runtime
        /// Requires using method to cast at their end
        /// </summary>
        /// <param name="t">The type to return</param>
        /// <returns>List of IProcessor implementers</returns>
        public IEnumerable<IProcessor> getProcessorsByType(Type t)
        {
            try
            {
                Log.Send(Log.Level.Debug, 10031, "Factorying processors by dynamic type", "Type: " + t.ToString());
                return Processors.Where(e => e.Metadata.Type.Equals(t)).Select(e => e.Value);
            }
            catch (Exception e)
            {
                throw new FactoryException(303,
                    new Base.Messages(
                        "Failure to select or instanciate the processors selected, when selecting multiple by type with dynamic type",
                        "Failed to select or construct processors",
                        "LINQ query failed to select a processor by type, dynamic type",
                        "Unable to load one of my processors"
                        ),
                        this,
                        Base.Log.Level.Critical,
                        e);
            }
        }

        /// <summary>
        /// Returns the first IProcessor that has a certain family metadata
        /// </summary>
        /// <param name="family">The family to select</param>
        /// <returns>An IProcessor implementation of the specific family</returns>
        public T getProcessorByFamily<T>(ProcessorFamily family) where T : IProcessor
        {
            Log.Send(Log.Level.Debug, 10032, "Factorying processor by family", "Family: " + family.ToString());
            Lazy<IProcessor, IProcessorMetadata> processor = Processors.FirstOrDefault(e => e.Metadata.Family.Equals(family));
            if (processor == null)
            {
                throw new ProcessorFactoryItemNotFoundException(304,
                    new Base.Messages(
                        "No processors found when loading by family with static type",
                        "Unable to find a processor",
                        "The LINQ query to select a processor by family with static type failed to return an item",
                        "Unable to load one of my processors"
                        ),
                        this,
                        Base.Log.Level.Critical
                        );
            }
            return (T)processor.Value;
        }

        /// <summary>
        /// Reuturns the first IProcessor that has a certain type metadata
        /// </summary>
        /// <returns>An IProcessor implementation of the specific family</returns>
        public T getProcessorByType<T>() where T : IProcessor
        {
            Log.Send(Log.Level.Debug, 10033, "Factorying processor by type", "Type: " + typeof(T).ToString());
            Lazy<IProcessor, IProcessorMetadata> processor = Processors.FirstOrDefault(e => e.Metadata.Type.Equals(typeof(T)));
            if (processor == null)
            {
                throw new ProcessorFactoryItemNotFoundException(305,
                    new Base.Messages(
                        "No processors found when loading by type with static type",
                        "Unable to find a processor",
                        "The LINQ query to select a processor by type with static type failed to return an item",
                        "Unable to load one of my processors"
                        ),
                        this,
                        Base.Log.Level.Critical
                        );
            }
            return (T)processor.Value;
        }

        /// <summary>
        /// Override of getProcessorByType for runtime resolution
        /// Can't use above when T varies at runtime
        /// Requires using method to cast at their end
        /// </summary>
        /// <param name="t">The type to return</param>
        /// <returns>An IProcessor implementation of the specific family</returns>
        public IProcessor getProcessorByType(Type t)
        {
            Log.Send(Log.Level.Debug, 10034, "Factorying processor by dynamic type", "Type: " + t.ToString());
            Lazy<IProcessor, IProcessorMetadata> processor = Processors.FirstOrDefault(e => e.Metadata.Type.Equals(t));
            if (processor == null)
            {
                throw new ProcessorFactoryItemNotFoundException(304,
                    new Base.Messages(
                        "No processors found when loading by type with dynamic type",
                        "Unable to find a processor",
                        "The LINQ query to select a processor by type with dynamic type failed to return an item",
                        "Unable to load one of my processors"
                        ),
                        this,
                        Base.Log.Level.Critical
                        );
            }
            return processor.Value;
        }
    }
}
