
using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace WPFUtilities.Components.Logging
{
    /// <summary>
    /// list logger extensions
    /// </summary>
    public static class ListLoggerExtensions
    {
        /// <summary>
        /// add list logger to logging builder
        /// </summary>
        /// <param name="loggingBuilder">logging builder</param>
        /// <param name="configure">configure action</param>
        /// <returns>the logging builder used to call this method</returns>
        public static ILoggingBuilder AddListLogger(
            this ILoggingBuilder loggingBuilder,
            Action<ListLoggerConfiguration> configure)
        {
            loggingBuilder.AddConfiguration();
            loggingBuilder.Services.Configure(configure);
            return loggingBuilder;
        }
    }
}
