using System;
using System.Collections;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;

namespace WPFUtilities.Components.Logging
{
    /// <summary>
    /// list logger configuration
    /// </summary>
    public class ListLoggerConfiguration
    {
        /// <summary>
        /// target list
        /// </summary>
        public IList Target { get; set; }

        /// <summary>
        /// enabled log levels
        /// </summary>
        public IList<LogLevel> LogLevels { get; }
            = new List<LogLevel> {
                LogLevel.Debug,
                LogLevel.Information,
                LogLevel.Error,
                LogLevel.Warning,
                LogLevel.Critical,
                LogLevel.Trace
            };

        /// <summary>
        /// get log target function
        /// </summary>
        public Func<IListLogger, IList> GetTarget;
    }
}