
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WPFUtilities.ComponentModels
{
    /// <summary>
    /// host services configurator
    /// </summary>
    public interface IHostServicesConfigurator
    {
        /// <summary>
        /// configure host services
        /// </summary>
        /// <param name="context">host builder context</param>
        /// <param name="services">services</param>
        void ConfigureServices(HostBuilderContext context, IServiceCollection services);
    }
}
