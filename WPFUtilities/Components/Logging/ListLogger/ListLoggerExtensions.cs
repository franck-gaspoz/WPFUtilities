
using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;

namespace WPFUtilities.Components.Logging.ListLogger
{
    /// <summary>
    /// list logger extensions
    /// </summary>
    public static class ListLoggerExtensions
    {
        /// <summary>
        /// add list logger to logging builder
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddListLogger(
            this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, ListLoggerProvider>());

            LoggerProviderOptions.RegisterProviderOptions
                <ListLoggerConfiguration, ListLoggerProvider>(builder.Services);

            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<IConfigureOptions<ListLoggerConfiguration>, ListLoggerConfigurationSetup>());

            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<IOptionsChangeTokenSource<ListLoggerConfiguration>,
                    LoggerProviderOptionsChangeTokenSource<ListLoggerConfiguration, ListLoggerProvider>>());

            return builder;
        }

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
            loggingBuilder.AddListLogger();
            loggingBuilder.Services.Configure(configure);
            return loggingBuilder;
        }
    }
}
