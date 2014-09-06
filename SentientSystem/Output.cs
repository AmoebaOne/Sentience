using System;
using Base;
using Configuration;

namespace SentientSystem
{
    /// <summary>
    /// Output mechanism for the entire system
    /// </summary>
    public class Output : ISentience
    {
        /// <summary>
        /// The configuration for the output
        /// </summary>
        private OutputConfiguration config = new OutputConfiguration();

        /// <summary>
        /// Whether or not the output is enabled
        /// </summary>
        private bool enabled = false;

        /// <summary>
        /// Handles initialisation
        /// </summary>
        public void Initialise()
        {
            this.enabled = true;
        }

        /// <summary>
        /// Deactivates the output
        /// </summary>
        public void Deactivate()
        {
            this.enabled = false;
        }

        /// <summary>
        /// Send some output
        /// </summary>
        /// <param name="output">The output string</param>
        public void Send(string output)
        {
            if (enabled)
            {
                switch (config.method)
                {
                    case OutputConfiguration.Types.Console:
                        Console.WriteLine(output);
                        break;
                    case OutputConfiguration.Types.LineDisplay:
                        OutputLineDisplay(output);
                        break;
                    case OutputConfiguration.Types.None:
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Handles configuration
        /// </summary>
        /// <param name="config">Configuration</param>
        public void Configure(SentienceConfiguration config)
        {
            OutputConfiguration oc = config as OutputConfiguration;
            if (oc != null)
            {
                this.config = oc;
            }
            else
            {
                throw new ConfigurationException(156,
                    new Messages(
                        "The Sentience Output class has not been given a valid configuration file, it is expecting OutputConfiguration but was given (" + oc.GetType().ToString() + ")",
                        "Sentience Output class given incorrect configuration type",
                        "The Sentience Output class was given a configuration of type (" + oc.GetType().ToString() + ") when it was expecting OutputConfiguration",
                        "Error setting up output system"
                        ),
                        this,
                        Log.Level.Critical
                    );
            }
        }

        /// <summary>
        /// Outputs to a line display
        /// </summary>
        /// <param name="output">The string to output</param>
        private void OutputLineDisplay(string output)
        {
            // TODO: Output to a line display
        }
    }
}
