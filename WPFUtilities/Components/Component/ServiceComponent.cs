
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using WPFUtilities.ComponentModels;

namespace WPFUtilities.Components.Component
{
    /// <summary>
    /// service component abstraction
    /// </summary>
    public abstract class ServiceComponent : IServiceComponent, IConfigureHostServices
    {
        /// <summary>
        /// component host
        /// </summary>
        public IHost Host { get; protected set; }

        /// <summary>
        /// component host builder
        /// </summary>
        readonly protected IHostBuilder _hostBuilder;

        /// <summary>
        /// host builder context
        /// </summary>
        protected HostBuilderContext _hostBuilderContext;

        /// <summary>
        /// creates a new instance
        /// </summary>
        public ServiceComponent()
        {
            _hostBuilder = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder();
        }

        /// <summary>
        /// configure services dependencies for owned host
        /// </summary>
        public void Configure()
        {
            _hostBuilder.ConfigureServices((context, services) =>
            {
                _hostBuilderContext = context;
                Configure(context, services);
            });
        }

        /// <summary>
        /// build the owned host
        /// </summary>
        public void Build()
        {
            Host = _hostBuilder.Build();
            _hostBuilderContext.Properties.Add(typeof(IHost), Host);
        }

        /// <inheritdoc/>
        public abstract void Configure(HostBuilderContext context, IServiceCollection services);
    }
}
