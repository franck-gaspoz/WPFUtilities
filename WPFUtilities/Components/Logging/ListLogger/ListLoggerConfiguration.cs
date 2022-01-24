using System.Collections;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;

namespace WPFUtilities.Components.Logging.ListLogger
{
    /// <summary>
    /// list logger configuration
    /// </summary>
    public class ListLoggerConfiguration
    {
        /// <summary>
        /// target list
        /// </summary>
        public List<IList> Targets { get; set; }
            = new List<IList>();

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
    }
}