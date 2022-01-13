﻿
using Microsoft.Extensions.Hosting;

using WPFUtilities.ComponentModels;

namespace WPFUtilities.Components.Component
{
    /// <summary>
    /// component host
    /// </summary>
    public interface IComponentHost :
        IHostServicesConfigurator,
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
        IComponentServiceProvider Services { get; }

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