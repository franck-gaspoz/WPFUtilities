using Microsoft.Extensions.Hosting;

namespace WPFUtilities.Components.ServiceComponent
{
    /// <summary>
    /// services components configurator
    /// </summary>
    public interface IServiceComponentsConfigurator
    {
        /// <summary>
        /// configure host services
        /// </summary>
        /// <param name="context">host builder context</param>
        /// <param name="services">services</param>
        void ConfigureServices(HostBuilderContext context, IServiceComponentCollection services);
    }
}
