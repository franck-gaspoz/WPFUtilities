﻿using Microsoft.Extensions.Hosting;

namespace WPFUtilities.Components.Appl
{
    /// <summary>
    /// application host
    /// </summary>
    public interface IApplicationHost
    {
        /// <summary>
        /// host
        /// </summary>
        IHost Host { get; }

        /// <summary>
        /// host builder
        /// </summary>
        IHostBuilder HostBuilder { get; }

        /// <summary>
        /// host builder context
        /// </summary>
        HostBuilderContext HostBuilderContext { get; }

        /// <summary>
        /// configure the host
        /// </summary>
        /// <param name="applicationBaseSettings">application base settings</param>
        void Configure(IApplicationBaseSettings applicationBaseSettings);

        /// <summary>
        /// build the host
        /// </summary>
        void Build();
    }
}