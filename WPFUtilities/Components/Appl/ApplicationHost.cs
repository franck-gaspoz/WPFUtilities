
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using WPFUtilities.ComponentModels;

namespace WPFUtilities.Components.Appl
{
    /// <summary>
    /// application host
    /// </summary>
    public class ApplicationHost : Singleton<ApplicationHost>, IApplicationHost
    {
        /// <inheritdoc/>
        public IHost Host { get; private set; }

        /// <inheritdoc/>
        public IHostBuilder HostBuilder { get; private set; }

        IApplicationBaseSettings _applicationBaseSettings;

        /// <inheritdoc/>
        public void Configure(IApplicationBaseSettings applicationBaseSettings)
        {
            _applicationBaseSettings = applicationBaseSettings;

            HostBuilder = new HostBuilder()
                .ConfigureLogging(configureLogging)
                .ConfigureServices(configureServices);

            applicationBaseSettings.InitializeHost?.Invoke(HostBuilder);

            if (applicationBaseSettings.ConfigureServices != null)
                HostBuilder.ConfigureServices(applicationBaseSettings.ConfigureServices);
        }

        /// <summary>
        /// build the host
        /// </summary>
        public void Build()
        {
            Host = HostBuilder.Build();
        }

        /// <summary>
        /// configure logging
        /// </summary>
        /// <param name="loggingBuilder">logging builder</param>
        private void configureLogging(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.SetMinimumLevel(_applicationBaseSettings.ApplicationLoggingSettings.MinimumLogLevel);
        }

        /// <summary>
        /// setup default services
        /// </summary>
        /// <param name="services">services</param>
        void configureServices(IServiceCollection services)
        {

        }
    }
}
