using System;
using Base;
using Contracts.Effectors.Commands;
using Contracts.Effectors.Events;

namespace Contracts.Effectors
{
    /// <summary>
    /// Delegate for effector event handling
    /// </summary>
    /// <param name="effector">The effector sending the event</param>
    /// <param name="args">Arguments from the event</param>
    public delegate void EffectorEvent(IEffector effector, EffectorEventArgs args);

    /// <summary>
    /// Delegate for events that command effectors
    /// </summary>
    /// <param name="e">The effector command</param>
    public delegate void EffectorCommandEvent(EffectorCommand e);

    /// <summary>
    /// Delegate for events when the state changes
    /// </summary>
    /// <param name="e">The new state</param>
    public delegate void EffectorStateChange(EffectorState e);

    /// <summary>
    /// Effector base interface
    /// </summary>
    public interface IEffector : ISentience
    {
        /// <summary>
        /// Event handler called when all requested effects have been completed
        /// </summary>
        event EffectorEvent EffectComplete;

        /// <summary>
        /// Event handler which is called when the effector is stuck and should be disabled
        /// </summary>
        event EffectorEvent EffectorStuck;

        /// <summary>
        /// Event handler called when the effector's state changes
        /// </summary>
        event EffectorStateChange StateChange;

        /// <summary>
        /// Function to handle effector commands
        /// </summary>
        /// <param name="e">The effector command</param>
        void handleCommand(EffectorCommand e);

        /// <summary>
        /// Returns the type of configuration this object expects
        /// </summary>
        /// <returns>Configuration type</returns>
        Type getConfigurationType();
    }
}
