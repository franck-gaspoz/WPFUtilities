
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using WPFUtilities.Components.Logging;
using WPFUtilities.Components.Services;

namespace SampleApp.Components.Logging
{
    /// <summary>
    /// log view model implementation factory
    /// </summary>
    public class LogViewModelServiceDependencyInitializer
        : IServiceDependencyInitializer
    {
        /// <summary>
        /// add service logger for LogViewModel
        /// </summary>
        /// <param name="hostBuilder">host builder</param>
        /// <param name="hostBuilderContext">host builder context</param>
        /// <param name="services">services</param>
        public void Initialize(
            IHostBuilder hostBuilder,
            HostBuilderContext hostBuilderContext,
            IServiceCollection services)
        {
            services.AddLogging(AddLogging);
        }

        void AddLogging(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddListLogger(ConfigureLogging);
        }

        void ConfigureLogging(ListLoggerConfiguration configure)
        {

        }
    }
}
