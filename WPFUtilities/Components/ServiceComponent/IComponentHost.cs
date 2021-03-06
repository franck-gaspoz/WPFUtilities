using System;
using System.ComponentModel;

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
        BindingList<IComponentHost> ChildHosts { get; }

        /// <summary>
        /// child hosts collection reset event
        /// </summary>
        event EventHandler<ChildHostsCollectionChangedEventArgs> ChildHostsCollectionChangedEvent;

        /// <summary>
        /// trigger a child hosts collection changed event
        /// </summary>
        /// <param name="args">event args</param>
        void OnChildHostsChanged(ChildHostsCollectionChangedEventArgs args);

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
        /// parent host changed event
        /// </summary>
        event EventHandler<ParentHostChangedEventArgs> ParentHostChangedEvent;

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