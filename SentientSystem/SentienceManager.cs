using System;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Base;
using Configuration;
using Configuration.Configs;
using Contracts.Composition;
using Contracts.Robots;

namespace SentientSystem
{
    /// <summary>
    /// Manager for running the Sentience system
    /// </summary>
    class SentienceManager : DirectoryComposable, ISentience
    {
        /// <summary>
        /// Robot factory
        /// </summary>
        [Import]
        [SuppressMessage("Microsoft.Performance", "CA1823")]
        private RobotFactory Factory;

        /// <summary>
        /// The configuration
        /// </summary>
        private Configurator config = new Configurator();

        /// <summary>
        /// The output mechanism
        /// </summary>
        private Output output = new Output();

        /// <summary>
        /// Args from the startup
        /// </summary>
        private StartupConfiguration myConfig;

        /// <summary>
        /// The robot we are going to be running
        /// </summary>
        private IRobot robot;

        /// <summary>
        /// Constructor
        /// </summary>
        public SentienceManager()
        {
            Compose();
        }

        /// <summary>
        /// Initialisation
        /// </summary>
        public void Initialise()
        {
            try
            {
                InitialiseOutput();
            }
            catch (Exception)
            {
                Console.WriteLine("Oops! Failed to start up");
                return;
            }
            try
            {
                InitialiseLogging();
            }
            catch (Exception)
            {
                output.Send("Oops! Failed to get going");
                return;
            }
            try
            {
                Log.Send(Log.Level.Metric, 6000, "Initialising robot", "Robot initialising commencing");
                InitialiseRobot();
            }
            catch (SentienceException e)
            {
                output.Send("Failed to start robot");
                Log.Send(Log.Level.Critical, 100, "Failed to start robot", "The robot initialisation call threw a SentienceException", this, e);
                return;
            }
        }

        /// <summary>
        /// Deactivates the system
        /// </summary>
        public void Deactivate()
        {
            output.Deactivate();
            robot.Deactivate();
        }

        /// <summary>
        /// Configure with startup arguments
        /// </summary>
        /// <param name="config">Configuration</param>
        public void Configure(SentienceConfiguration config)
        {
            StartupConfiguration sc = config as StartupConfiguration;
            if (sc != null)
            {
                myConfig = sc;
            }
            else
            {
                throw new ConfigurationException(155,
                    new Messages(
                        "The SentienceManager has not been given a valid configuration file, it is expecting StartupConfiguration but was given (" + sc.GetType().ToString() + ")",
                        "SentienceManager given incorrect configuration type",
                        "The SentienceManager was given a configuration of type (" + sc.GetType().ToString() + ") when it was expecting StartupConfiguration",
                        "Error starting system"
                        ),
                        this,
                        Log.Level.Critical
                    );
            }

            // Figure out which configuration to use
            if (myConfig.args.Length > 0 && myConfig.args[0].Length > 0)
            {
                Log.Send(Log.Level.Debug, 10001, "Configuring Sentience with argument", "SentienceManager is being configured with an argument from the StartupConfiguration, which is " + myConfig.args[0]);
                establishConfiguration(myConfig.args[0]);
            }
            else
            {
                Log.Send(Log.Level.Debug, 10002, "Configuring Sentience with default configuration", "SentienceManager is being configured with the default configuration");
                establishConfiguration();
            }
        }

        /// <summary>
        /// Establishes which configuration to use
        /// </summary>
        private void establishConfiguration(string specified = null)
        {
            string[] configs = config.listConfigurationFiles();
            if (specified != null && configs.Contains(specified))
            {
                config.useConfigurationFile(specified);
            }
            else
            {
                if (specified != null)
                {
                    // TODO: Write output that the specified file is not available and using default
                    // This should be tricky considering we don't have any configuration as such yet
                }
                config.useConfigurationFile(Configurator.DEFAULT_CONFIG_FILE);
            }
        }

        /// <summary>
        /// Initialises the logging
        /// </summary>
        private void InitialiseLogging()
        {
            // Set up logging
            LogConfiguration lc = config.getConfiguration<LogConfiguration>("Log");
            Log.Configure(lc);
            Log.Send(Log.Level.Debug, 10003, "Log configured", "The log has been configured");
        }

        /// <summary>
        /// Initialises the output
        /// </summary>
        private void InitialiseOutput()
        {
            // Set up and start up the output
            OutputConfiguration oc = config.getConfiguration<OutputConfiguration>("Output");
            output.Configure(oc);
            output.Initialise();
            Log.Send(Log.Level.Debug, 10004, "Output initialised", "The output has been initialised");
        }

        /// <summary>
        /// Initialises the robot
        /// </summary>
        private void InitialiseRobot()
        {
            Log.Send(Log.Level.Debug, 10005, "Robot Initialisation", "Commencing initialisation of the Robot");
            RobotConfiguration rc = config.getConfiguration<RobotConfiguration>("Robot");
            Log.Send(Log.Level.Debug, 10006, "Robot configuration loaded", "Loaded configuration for the robot initialisation");
            try
            {
                this.robot = Factory.getRobotByType(rc.RobotType);
                Log.Send(Log.Level.Debug, 10007, "Robot loaded", "The robot has been loaded from the factory");
            }
            catch (Exception e)
            {
                throw new SentienceException(102,
                        new Messages(
                            "The robot configuration specified a type which is not available to instanciate",
                            "Robot configuration specified that does not exist",
                            "The robot specified by configuration is not available",
                            "Error starting robot"
                            ),
                            this,
                            Log.Level.Critical,
                            e
                        );
            }
            Log.Send(Log.Level.Debug, 10008, "Commencing robot configuration", "Configuring the robot with the loaded robot configuration");
            robot.Configure(config.getConfiguration<RobotConfiguration>("Robot", robot.getConfigurationType()));
            Log.Send(Log.Level.Debug, 10114, "Providing global configuration", "Giving the robot system the global configuration class");
            robot.giveGlobalConfiguration(config);
            Log.Send(Log.Level.Debug, 10009, "Commencing initialisation", "Commencing initialisation of the robot");
            robot.Initialise();
            Log.Send(Log.Level.Debug, 10010, "Initialisation complete", "Initialisation has been completed");
        }
    }
}
