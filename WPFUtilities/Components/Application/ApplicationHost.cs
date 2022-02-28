
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using WPFUtilities.ComponentModels;
using WPFUtilities.Components.ServiceComponent;
using WPFUtilities.Components.Services;

namespace WPFUtilities.Components.Application
{
    /// <summary>
    /// application host
    /// </summary>
    public sealed class ApplicationHost : ComponentHost,
        IComponentHost,
        IApplicationHost,
        IServicesConfigurator,
        IHostLoggingConfigurator
    {

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
        public override void ConfigureLogging(HostBuilderContext context, ILoggingBuilder loggingBuilder)
        {
            base.ConfigureLogging(context, loggingBuilder);
            loggingBuilder.ClearProviders();
            if (_applicationBaseSettings.ApplicationLoggingSettings.LogConsole)
                loggingBuilder.AddConsole();
            loggingBuilder.SetMinimumLevel(_applicationBaseSettings.ApplicationLoggingSettings.MinimumLogLevel);
        }

        /// <inheritdoc/>
        public override void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            base.ConfigureServices(context, services);
            HostBuilderContext = context;
            new ServicesDependenciesBuilder(HostBuilder, context, services)
                .AddSingletonServices()
                .AddDependencyServices()
                .AddDependencyServicesInitializers();
        }
    }
}
