
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using WPFUtilities.ComponentModels;
using WPFUtilities.Components.Services;

namespace WPFUtilities.Components.Application
{
    /// <summary>
    /// application host
    /// </summary>
    public sealed class ApplicationHost :
        IApplicationHost,
        IHostServicesConfigurator,
        IHostLoggingConfigurator
    {
        /// <inheritdoc/>
        public IHost Host { get; private set; }

        /// <inheritdoc/>
        public IHostBuilder HostBuilder { get; private set; }

        /// <inheritdoc/>
        public HostBuilderContext HostBuilderContext { get; private set; }

        IApplicationBaseSettings _applicationBaseSettings;

        /// <inheritdoc/>
        public void Configure(IApplicationBaseSettings applicationBaseSettings)
        {
            _applicationBaseSettings = applicationBaseSettings;

            HostBuilder = new HostBuilder()
                .ConfigureLogging(ConfigureLogging);

            HostBuilder
                .ConfigureServices((hostBuilderContext, services)
                    => ConfigureServices(hostBuilderContext, services));
        }

        /// <inheritdoc/>
        public void Build()
        {
            Host = HostBuilder.Build();
            HostBuilderContext.Properties.Add(typeof(IHost), Host);
        }

        /// <inheritdoc/>
        public void ConfigureLogging(HostBuilderContext context, ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.ClearProviders();
            if (_applicationBaseSettings.ApplicationLoggingSettings.LogConsole)
                loggingBuilder.AddConsole();
            loggingBuilder.SetMinimumLevel(_applicationBaseSettings.ApplicationLoggingSettings.MinimumLogLevel);
        }

        /// <inheritdoc/>
        public void ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection services)
        {
            HostBuilderContext = hostBuilderContext;
            new ServicesDependenciesBuilder(HostBuilder, hostBuilderContext, services)
                .AddSingletonServices()
                .AddDependencyServices()
                .AddDependencyServicesInitializers();
        }

    }
}
