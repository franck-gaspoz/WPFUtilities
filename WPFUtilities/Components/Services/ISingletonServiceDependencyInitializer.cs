
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WPFUtilities.Components.Services
{
    /// <summary>
    /// dependency service initializer. allows a class to be instantiated and called at host initialization time
    /// </summary>
    public interface ISingletonServiceDependencyInitializer
    {
        /// <summary>
        /// creates instance
        /// </summary>
        /// <param name="hostBuilder">host builder</param>
        /// <param name="hostBuilderContext">host builder context</param>
        /// <param name="services">services</param>
        void Initialize(
            IHostBuilder hostBuilder,
            HostBuilderContext hostBuilderContext,
            IServiceCollection services);
    }
}
