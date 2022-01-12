
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WPFUtilities.ComponentModels
{
    /// <summary>
    /// configure host services
    /// </summary>
    public interface IConfigureHostServices
    {
        /// <summary>
        /// configure host services
        /// </summary>
        /// <param name="context">host builder context</param>
        /// <param name="services">services</param>
        void Configure(HostBuilderContext context, IServiceCollection services);
    }
}
