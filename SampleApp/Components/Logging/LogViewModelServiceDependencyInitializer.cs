
using System.Collections;

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
        IHostBuilder _hostBuilder;
        HostBuilderContext _hostBuilderContext;
        IServiceCollection _services;

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
            _hostBuilder = hostBuilder;
            _hostBuilderContext = hostBuilderContext;
            _services = services;
            services.AddLogging(AddLogging);
        }

        void AddLogging(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddListLogger(ConfigureLogging);
        }

        void ConfigureLogging(ListLoggerConfiguration configure)
        {
            configure.GetTarget = GetTarget;
        }

        IList GetTarget(IListLogger listLogger)
        {
            var host = (IHost)_hostBuilderContext.Properties[typeof(IHost)];
            var logViewModel = host.Services.GetService<ILogViewModel>();
            return logViewModel.Messages;
        }
    }
}
