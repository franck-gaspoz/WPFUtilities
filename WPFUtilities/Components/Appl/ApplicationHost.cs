
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
        public void Build(IApplicationBaseSettings applicationBaseSettings)
        {
            var hostBuilder = new HostBuilder()
                    .ConfigureServices(configureServices);
            if (applicationBaseSettings.ConfigureServices != null)
                hostBuilder.ConfigureServices(applicationBaseSettings.ConfigureServices);
            Host = hostBuilder.Build();
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
