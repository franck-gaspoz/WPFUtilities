
using System.Collections.Generic;

using Microsoft.Extensions.Hosting;

using WPFUtilities.ComponentModels;
using WPFUtilities.Components.Services;

namespace WPFUtilities.Components.ServiceComponent
{
    /// <summary>
    /// component host: host of IServicecomponents
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
        /// child hosts
        /// </summary>
        IReadOnlyList<IComponentHost> ChildHosts { get; }

        /// <summary>
        /// register a host as a child of this host
        /// </summary>
        /// <param name="host">host</param>
        void RegisterChildHost(IComponentHost host);

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