using System;

using Microsoft.Extensions.Logging;

namespace WPFUtilities.Components.Logging
{
    /// <summary>
    /// logging to a list
    /// </summary>
    public sealed class ListLogger : ILogger, IListLogger
    {
        string _name;

        /// <inheritdoc/>
        public Func<ListLoggerConfiguration> GetCurrentConfiguration { get; set; }

        /// <summary>
        /// creates a new instance
        /// </summary>
        public ListLogger(
            string name,
            Func<ListLoggerConfiguration> getCurrentConfiguration)
            => (_name, GetCurrentConfiguration) = (name, getCurrentConfiguration);

        /// <inheritdoc/>
        public IDisposable BeginScope<TState>(TState state)
            => default;

        /// <inheritdoc/>
        public bool IsEnabled(LogLevel logLevel)
            => GetCurrentConfiguration().LogLevels.Contains(logLevel);

        /// <inheritdoc/>
        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;
            var config = GetCurrentConfiguration();

            if (config.Target == null)
                config.Target = config.GetTarget(this);

            var s = $"[{eventId.Id,2}: {logLevel,-12}]";
            s += $"     {_name} - ";
            s += Environment.NewLine + $"{formatter(state, exception)}";
            config.Target?.Add(s);
        }
    }
}
