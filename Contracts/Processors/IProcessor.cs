using Base;
using Contracts.Processors.Events;

namespace Contracts.Processors
{
    /// <summary>
    /// Delegate for processor event handling
    /// </summary>
    /// <param name="processor">The processor sending the event</param>
    /// <param name="args">Arguments from the event</param>
    public delegate void ProcessorEvent(IProcessor processor, ProcessorEventArgs args);

    /// <summary>
    /// Base interface for a processor
    /// </summary>
    public interface IProcessor : ISentience
    {
    }
}
