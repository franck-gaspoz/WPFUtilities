using System;
using System.Collections.Concurrent;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WPFUtilities.Components.Logging.ListLogger
{
    /// <summary>
    /// list logger provider
    /// </summary>
    public sealed class ListLoggerProvider : ILoggerProvider
    {
        private readonly IDisposable _onChangeToken;
        private ListLoggerConfiguration _currentConfig;
        private readonly ConcurrentDictionary<string, ListLogger> _loggers =
            new ConcurrentDictionary<string, ListLogger>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// creates a new list logger provider
        /// </summary>
        /// <param name="config">list logger configuration</param>
        public ListLoggerProvider(IOptionsMonitor<ListLoggerConfiguration> config)
        {
            _currentConfig = config.CurrentValue;
            _onChangeToken = config.OnChange(ConfigChanged);
        }

        void ConfigChanged(ListLoggerConfiguration updatedConfig)
        {
            _currentConfig = updatedConfig;
        }

        /// <summary>
        /// create list logger
        /// </summary>
        /// <param name="categoryName">category name</param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name =>
                new ListLogger(name, GetCurrentConfig));

        /// <summary>
        /// get current config
        /// </summary>
        /// <returns>list logger configuration</returns>
        public ListLoggerConfiguration GetCurrentConfig() => _currentConfig;

        /// <summary>
        /// dispose
        /// </summary>
        public void Dispose()
        {
            _loggers.Clear();
            _onChangeToken.Dispose();
        }
    }
}
