using System;

using Microsoft.Extensions.Logging;

namespace WPFUtilities.Components.Logging
{
    /// <summary>
    /// logging to a list
    /// </summary>
    public class ListLogger : ILogger
    {
        string _name;

        Func<ListLoggerConfiguration> _getCurrentConfiguration;

        /// <summary>
        /// creates a new instance
        /// </summary>
        public ListLogger(
            string name,
            Func<ListLoggerConfiguration> getCurrentConfiguration)
            => (_name, _getCurrentConfiguration) = (name, getCurrentConfiguration);

        /// <inheritdoc/>
        public IDisposable BeginScope<TState>(TState state)
            => default;

        /// <inheritdoc/>
        public bool IsEnabled(LogLevel logLevel)
            => _getCurrentConfiguration().LogLevels.Contains(logLevel);

        /// <inheritdoc/>
        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;
            var config = _getCurrentConfiguration();

        }
    }
}
