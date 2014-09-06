using System.Diagnostics;
using System.IO;
using System.Linq;
using System;

namespace Base
{
    /// <summary>
    /// Logging interface
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// Types of logging output
        /// </summary>
        public enum Target
        {
            /// <summary>
            /// No source
            /// </summary>
            None,
            /// <summary>
            /// Log to the console
            /// </summary>
            Console,
            /// <summary>
            /// Log to a file
            /// </summary>
            File,
            /// <summary>
            /// Log to the UI
            /// </summary>
            UI
        }

        /// <summary>
        /// Log levels in increasing severity
        /// </summary>
        public enum Level
        {
            /// <summary>
            /// Metric, used for analysis of events etc
            /// </summary>
            Metric,
            /// <summary>
            /// A data event has occurred (assumed VERY high frequency)
            /// </summary>
            DataEvent,
            /// <summary>
            /// Low level message
            /// </summary>
            Message,
            /// <summary>
            /// Debug message
            /// </summary>
            Debug,
            /// <summary>
            /// Notification of state
            /// </summary>
            Notification,
            /// <summary>
            /// An alert of potential problem state
            /// </summary>
            Alert,
            /// <summary>
            /// Warning of a condition that is considered bad
            /// </summary>
            Warning,
            /// <summary>
            /// An error condition
            /// </summary>
            Error,
            /// <summary>
            /// An emergency condition (quite bad)
            /// </summary>
            Emergency,
            /// <summary>
            /// A critical situation (really bad)
            /// </summary>
            Critical,
            /// <summary>
            /// Totally screwed (probably going to go die now)
            /// </summary>
            Failure,
            /// <summary>
            /// Log all error messages
            /// </summary>
            All
        }

        /// <summary>
        /// Whether or not it has been configured yet
        /// </summary>
        private static bool Configured = false;

        /// <summary>
        /// List of sources to log to
        /// </summary>
        private static Target[] Targets = { Target.None };

        /// <summary>
        /// Which levels should get logged out
        /// </summary>
        private static Level[] Levels = { Level.Alert, Level.Critical, Level.Warning, Level.Error, Level.Emergency, Level.Failure };

        /// <summary>
        /// Name of the logfile to write to
        /// </summary>
        private static string Logfile = "trace.txt";

        /// <summary>
        /// Executes a log message
        /// </summary>
        public static void Send(Level level, int code, string headline, string detail, object context = null, SentienceException exception = null)
        {
            lock (Log.Logfile)
            {
                // Simple test to avoid all the execution if unnecessary
                if ((Targets.Length < 1 && Targets[0] == Target.None) || (!Levels.Contains(level) && !Levels.Contains(Level.All)))
                {
                    return;
                }

                TimeSpan time = DateTime.Now - new DateTime(1970, 1, 1);
                Trace.WriteLine(getLevelText(level).ToUpper() + " (" + code.ToString() + ") at " + time.TotalMilliseconds.ToString() + " " + headline);

                Trace.Indent();
                Trace.WriteLine("Detail: " + detail);
                Trace.Unindent();

                if (context != null)
                {
                    Trace.Indent();
                    Trace.WriteLine("Provided context:");
                    Trace.Indent();
                    foreach (TraceListener tw in Trace.Listeners)
                    {
                        if (tw is TextWriterTraceListener)
                        {
                            TextWriterTraceListener twl = tw as TextWriterTraceListener;
                            ObjectDumper.Write(context, 5, twl.Writer);
                        }
                    }
                    Trace.Unindent(); Trace.Unindent();
                }

                if (exception != null)
                {
                    Trace.Indent();
                    Trace.WriteLine("Exception: ");
                    Trace.Indent();
                    Trace.WriteLine("Type: " + exception.GetType().ToString());
                    Trace.WriteLine("Full description: " + exception.Messages.Full);
                    Trace.WriteLine("Error summary: " + exception.Messages.Summary);
                    Trace.WriteLine("Developer message: " + exception.Messages.Developer);
                    Trace.WriteLine("User message: " + exception.Messages.User);
                    Trace.Unindent(); Trace.Unindent();
                }
                Trace.WriteLine("");
            }
        }

        /// <summary>
        /// Configures the entire system with 
        /// </summary>
        /// <param name="config">The configuration</param>
        public static void Configure(SentienceConfiguration config)
        {
            LogConfiguration lc = config as LogConfiguration;
            if (lc == null)
            {
                return;
            }
            ConfigureTargets(lc.Targets);
            ConfigureLogFile(lc.Logfile);
            ConfigureLevels(lc.Levels);
        }

        /// <summary>
        /// Public logging setup
        /// </summary>
        /// <param name="targets">List of sources to log to</param>
        public static void ConfigureTargets(Target[] targets)
        {
            Targets = targets;
            Configured = false;
            Configure();
        }

        /// <summary>
        /// Configure the filename of the file to log to
        /// </summary>
        /// <param name="filename">The filename</param>
        public static void ConfigureLogFile(string filename)
        {
            Logfile = filename;
            Configured = false;
            Configure();
        }

        /// <summary>
        /// Configure which levels should be written
        /// </summary>
        /// <param name="levels">The levels to include in logging</param>
        public static void ConfigureLevels(Level[] levels)
        {
            Levels = levels;
        }

        /// <summary>
        /// Allows for configuration whenever the first message comes in
        /// </summary>
        private static void Configure()
        {
            lock (Log.Logfile)
            {
                if (Configured)
                {
                    return;
                }
                Configured = true;

                Trace.Listeners.Clear();

                TextWriterTraceListener t;

                foreach (Target s in Targets)
                {
                    switch (s)
                    {
                        case Target.Console:
                            t = new TextWriterTraceListener(System.Console.Out);
                            Trace.Listeners.Add(t);
                            break;
                        case Target.File:
                            t = new TextWriterTraceListener(File.CreateText(Logfile));
                            Trace.Listeners.Add(t);
                            break;
                        case Target.None:
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Returns a text string for a log level
        /// </summary>
        /// <param name="level">The level</param>
        /// <returns>Text representation</returns>
        private static string getLevelText(Level level)
        {
            switch (level)
            {
                case Level.Alert:
                    return "Alert";
                case Level.Critical:
                    return "Critical";
                case Level.Debug:
                    return "Debug";
                case Level.Emergency:
                    return "Emergency";
                case Level.Error:
                    return "Error";
                case Level.Failure:
                    return "Failure";
                case Level.Message:
                    return "Message";
                case Level.Notification:
                    return "Notification";
                case Level.Warning:
                    return "Warning";
                case Level.Metric:
                    return "Metric";
                default:
                    return "Unknown";
            }
        }
    }
}
