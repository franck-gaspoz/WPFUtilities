using System;

using Microsoft.Extensions.Logging;

namespace WPFUtilities.Components.Logging.ListLogger
{
    /// <summary>
    /// logging to a list
    /// </summary>
    public interface IListLogger
    {
        /// <summary>
        /// get current configuration
        /// </summary>
        Func<ListLoggerConfiguration> GetCurrentConfiguration { get; set; }

        /// <summary>
        /// check if enable for log level
        /// </summary>
        /// <param name="logLevel">log level</param>
        /// <returns>true if enabled, <see langword="false"/>otherwize</returns>
        bool IsEnabled(LogLevel logLevel);

        /// <summary>
        /// log
        /// </summary>
        /// <typeparam name="TState">state type</typeparam>
        /// <param name="logLevel">log level</param>
        /// <param name="eventId">event id</param>
        /// <param name="state">state</param>
        /// <param name="exception">exception</param>
        /// <param name="formatter">formatter</param>
        void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter);
    }
}