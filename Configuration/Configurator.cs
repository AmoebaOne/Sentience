using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Configuration
{
    /// <summary>
    /// The configuration engine
    /// </summary>
    public class Configurator
    {
        /// <summary>
        /// The default configuration directory
        /// </summary>
        const string DEFAULT_CONFIG_DIR = "Config\\";

        /// <summary>
        /// The default configuration file name
        /// </summary>
        public const string DEFAULT_CONFIG_FILE = "default";

        /// <summary>
        /// The file extension of configuration files
        /// </summary>
        const string DEFAULT_CONFIG_EXTN = ".sentience";

        Dictionary<string, string> Configurations;

        /// <summary>
        /// Constructors
        /// </summary>
        public Configurator()
        {
        }

        /// <summary>
        /// Returns a list of the available configuration files
        /// </summary>
        /// <returns>List of config files</returns>
        public string[] listConfigurationFiles()
        {
            string[] files = Directory.GetFiles(@DEFAULT_CONFIG_DIR, "*" + DEFAULT_CONFIG_EXTN);
            for (int i = 0; i < files.Length; i++)
            {
                files[i] = files[i].Replace(DEFAULT_CONFIG_DIR, "").Replace(DEFAULT_CONFIG_EXTN, "");
            }
            return files;
        }

        /// <summary>
        /// Specify the configuration file to use
        /// </summary>
        /// <param name="file">The configuration file name</param>
        public bool useConfigurationFile(string file)
        {
            try
            {
                string config = this.readConfigurationFile(DEFAULT_CONFIG_DIR + file + DEFAULT_CONFIG_EXTN);
                this.Configurations = new Dictionary<string, string>();
                this.parseConfiguration(config);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets configuration of a particular type
        /// </summary>
        /// <typeparam name="T">The type to get configuration for (should be the configuration object)</typeparam>
        /// <param name="config">The configuration parameter name to retrieve</param>
        /// <param name="deserialisationType">The type to deserialise the object to</param>
        /// <returns>The configuration object</returns>
        public T getConfiguration<T>(string config, Type deserialisationType = null) where T : SentienceConfiguration
        {
            try
            {
                Type dType = deserialisationType;
                if (deserialisationType == null)
                {
                    dType = typeof(T);
                }
                return JsonConvert.DeserializeObject(Configurations[config.ToLower()], dType) as T;
            }
            catch (KeyNotFoundException e)
            {
                throw new ConfigurationException(150,
                    new Base.Messages(
                        "The requested configuration key (" + config + ") cannot be located in the currently loaded configuration file",
                        "Invalid key in configuration",
                        "Please validate your configuration to ensure that the required keys are specified. Looking for: " + config,
                        "The system configuration is incorrect."
                        ),
                        this,
                        Base.Log.Level.Critical,
                        e
                        );
            }
        }

        /// <summary>
        /// Reads a configuration file from the store
        /// </summary>
        /// <param name="file">The file to read</param>
        /// <returns>The contents of the file</returns>
        private string readConfigurationFile(string file)
        {
            return File.ReadAllText(@file);
        }

        /// <summary>
        /// Parses a configuration string
        /// </summary>
        /// <param name="config">The configuration string</param>
        private void parseConfiguration(string config)
        {
            JObject o = JObject.Parse(config);
            IList<string> keys = o.Properties().Select(p => p.Name).ToList();
            foreach (KeyValuePair<string, JToken> jo in o)
            {
                Configurations[jo.Key.ToLower()] = jo.Value.ToString();
            }
        }
    }
}
