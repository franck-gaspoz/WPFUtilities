using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using WPFUtilities.ComponentModels;

namespace WPFUtilities.Components.Application
{
    /// <summary>
    /// application host
    /// </summary>
    public interface IApplicationHost : IHostServicesConfigurator
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
        /// configure logging
        /// </summary>
        /// <param name="context">host builder context</param>
        /// <param name="loggingBuilder">logging builder</param>
        void ConfigureLogging(HostBuilderContext context, ILoggingBuilder loggingBuilder);

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
