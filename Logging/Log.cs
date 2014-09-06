using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using Base;

namespace Logging
{
    /// <summary>
    /// Logging interface
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// Types of logging output
        /// </summary>
        public enum Source
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
            Failure
        }

        /// <summary>
        /// Whether or not it has been configured yet
        /// </summary>
        private static bool Configured = false;

        /// <summary>
        /// List of sources to log to
        /// </summary>
        private static Source[] Sources = { Source.None };

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
            // Simple test to avoid all the execution if unnecessary
            if ((Sources.Length < 1 && Sources[0] == Source.None) || !Levels.Contains(level))
            {
                return;
            }

            Trace.WriteLine(getLevelText(level).ToUpper() + " (" + code.ToString() + ") " + headline);
            
            Trace.Indent();
            Trace.WriteLine("Detail: " + detail);
            Trace.Unindent();

            if (context != null)
            {
                Trace.Indent();
                Trace.WriteLine("Provided context:");
                Trace.Indent();
                foreach (TextWriterTraceListener tw in Trace.Listeners)
                {
                    ObjectDumper.Write(context, 5, tw.Writer);
                }
                Trace.Unindent(); Trace.Unindent();
            }

            if (exception != null)
            {
                Trace.Indent();
                Trace.WriteLine("Exception: ");
                Trace.Indent();
                Trace.WriteLine("Type: " + exception.GetType().ToString());
                Trace.Unindent(); Trace.Unindent();
            }
            Trace.WriteLine("");
        }

        /// <summary>
        /// Public logging setup
        /// </summary>
        /// <param name="sources">List of sources to log to</param>
        public static void ConfigureSources(Source[] sources)
        {
            Sources = sources;
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
            if (Configured)
            {
                return;
            }

            Trace.Listeners.Clear();

            TextWriterTraceListener t;

            foreach (Source s in Sources)
            {
                switch (s)
                {
                    case Source.Console:
                        t = new TextWriterTraceListener(System.Console.Out);
                        Trace.Listeners.Add(t);
                        break;
                    case Source.File:
                        t = new TextWriterTraceListener(File.CreateText(Logfile));
                        Trace.Listeners.Add(t);
                        break;
                    case Source.None:
                    default:
                        break;
                }
            }

            Configured = true;
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
                default:
                    return "Unknown";
            }
        }
    }
}
