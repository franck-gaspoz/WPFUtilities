
using Microsoft.Extensions.Hosting;

using WPFUtilities.ComponentModels;
using WPFUtilities.Components.Services;

namespace WPFUtilities.Components.Component
{
    /// <summary>
    /// component host
    /// </summary>
    public interface IComponentHost :
        IServicesConfigurator,
        IHostLoggingConfigurator
    {
        /// <summary>
        /// host
        /// </summary>
        IHost Host { get; }

        /// <summary>
        /// component host builder
        /// </summary>
        IHostBuilder HostBuilder { get; }

        /// <summary>
        /// host builder context
        /// </summary>
        HostBuilderContext HostBuilderContext { get; }

        /// <summary>
        /// component service provider
        /// </summary>
        IServiceComponentProvider Services { get; }

        /// <summary>
        /// parent host or null if this is a root component host
        /// </summary>
        IComponentHost ParentHost { get; set; }

        /// <summary>
        /// build the component host
        /// </summary>
        void Build();

    }
}