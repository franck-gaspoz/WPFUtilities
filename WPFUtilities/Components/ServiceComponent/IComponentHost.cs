
using Microsoft.Extensions.Hosting;

using WPFUtilities.ComponentModels;
using WPFUtilities.Components.Services;

namespace WPFUtilities.Components.ServiceComponent
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
        /// root component host
        /// </summary>
        IComponentHost RootHost { get; }

        /// <summary>
        /// build the component host
        /// </summary>
        void Build();

        /// <summary>
        /// concrete type name of the component this host belongs to
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// get the type name of the component this host belongs to with the hierarchy of its parents type names
        /// </summary>
        string FullName { get; }
    }
}