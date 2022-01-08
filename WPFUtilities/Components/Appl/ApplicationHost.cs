
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using WPFUtilities.Components.Services;

namespace WPFUtilities.Components.Appl
{
    /// <summary>
    /// application host
    /// </summary>
    public class ApplicationHost : IApplicationHost
    {
        #region singleton pattern

        /// <summary>
        /// private constructor 
        /// </summary>
        ApplicationHost() { }

        static object _lock = new object();

        static ApplicationHost _instance;
        /// <summary>
        /// singleton instance
        /// </summary>
        public static ApplicationHost Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new ApplicationHost();
                    return _instance;
                }
            }
        }

        #endregion

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

            applicationBaseSettings.InitializeHost?.Invoke(HostBuilder);

            if (applicationBaseSettings.ConfigureLogging != null)
                HostBuilder.ConfigureLogging(applicationBaseSettings.ConfigureLogging);

            if (applicationBaseSettings.ConfigureServices != null)
                HostBuilder.ConfigureServices(applicationBaseSettings.ConfigureServices);
        }

        /// <inheritdoc/>
        public void Build()
        {
            Host = HostBuilder.Build();
            HostBuilderContext.Properties.Add(typeof(IHost), Host);
        }

        /// <summary>
        /// configure logging
        /// </summary>
        /// <param name="loggingBuilder">logging builder</param>
        void ConfigureLogging(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.ClearProviders();
            if (_applicationBaseSettings.ApplicationLoggingSettings.LogConsole)
                loggingBuilder.AddConsole();
            loggingBuilder.SetMinimumLevel(_applicationBaseSettings.ApplicationLoggingSettings.MinimumLogLevel);
        }

        /// <summary>
        /// setup default services
        /// </summary>
        /// <param name="hostBuilderContext">host builder context</param>
        /// <param name="services">services</param>
        void ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection services)
        {
            HostBuilderContext = hostBuilderContext;
            new ServicesDependenciesBuilder(HostBuilder, hostBuilderContext, services)
                .AddSingletonServices()
                .AddDependencyServices()
                .AddDependencyServicesInitializers();
        }

    }
}
