
using System.Collections;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using WPFUtilities.ComponentModels;
using WPFUtilities.Components.Logging.ListLogger;
using WPFUtilities.Extensions.Host;

namespace SampleApp.Components.Logging
{
    /// <summary>
    /// log view model implementation factory
    /// </summary>
    public class LogViewModelServiceDependencyInitializer
        : IConfigureHostServices
    //    : ISingletonServiceDependencyInitializer
    {
        HostBuilderContext _hostBuilderContext;

        /// <summary>
        /// add service logger for LogViewModel
        /// </summary>
        /// <param name = "hostBuilderContext" > host builder context</param>
        /// <param name ="services">services</param>
        public void Configure(
            HostBuilderContext hostBuilderContext,
            IServiceCollection services)
        {
            services.AddLogging(AddLogging);
            _hostBuilderContext = hostBuilderContext;
        }

        /// <summary>
        /// add logging service
        /// </summary>
        /// <param name="loggingBuilder">logging builder</param>
        void AddLogging(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddListLogger(ConfigureLogging);
        }

        /// <summary>
        /// setup logger configuration
        /// </summary>
        /// <param name="configuration">logger configuration</param>
        void ConfigureLogging(ListLoggerConfiguration configuration)
        {
            configuration.GetTarget = GetTarget;
        }

        /// <summary>
        /// get the view model from the creating host builder context
        /// </summary>
        /// <param name="listLogger">list logger caller</param>
        /// <returns>IList target</returns>
        IList GetTarget(IListLogger listLogger)
        {
            var host = _hostBuilderContext.GetHost();
            var logViewModel = host.Services.GetService<ILogViewModel>();
            return logViewModel.Messages;
        }
    }
}
