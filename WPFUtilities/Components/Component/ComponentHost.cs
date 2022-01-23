
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using WPFUtilities.ComponentModels;

using mshosting = Microsoft.Extensions.Hosting;

namespace WPFUtilities.Components.Component
{
    /// <summary>
    /// abstraction of a component
    /// </summary>
    public class ComponentHost :
        IComponentHost,
        IHostServicesConfigurator,
        IHostLoggingConfigurator
    {
        #region properties

        /// <inheritdoc/>
        public IHost Host { get; protected set; }

        /// <summary>
        /// component host builder
        /// </summary>
        public IHostBuilder HostBuilder { get; protected set; }

        /// <summary>
        /// host builder context
        /// </summary>
        public HostBuilderContext HostBuilderContext { get; protected set; }

        /// <inheritdoc/>
        public IServiceComponentProvider Services { get; protected set; }

        /// <inheritdoc/>
        public IComponentHost ParentHost { get; set; }

        /// <summary>
        /// creates a new root component host
        /// </summary>
        public ComponentHost() => Initialize();

        #endregion

        /// <summary>
        /// creates a new component host having a parent component host
        /// </summary>
        /// <param name="parent">parent host</param>
        public ComponentHost(ComponentHost parent)
        {
            ParentHost = parent;
            Initialize();
        }

        /// <summary>
        /// initialize
        /// </summary>
        void Initialize()
        {
            Services = new ServiceComponentProvider(this);
            HostBuilder = mshosting.Host.CreateDefaultBuilder();
            HostBuilder.ConfigureServices((context, services) => ConfigureServices(context, services));
            HostBuilder.ConfigureLogging((context, loggingBuilder) => ConfigureLogging(context, loggingBuilder));
        }

        /// <inheritdoc/>
        public void Build()
        {
            Host = HostBuilder.Build();
            HostBuilderContext.Properties.Add(typeof(IHost), Host);
        }

        #region IHostServicesConfigurator

        /// <inheritdoc/>
        public virtual void ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection services)
        {
            HostBuilderContext = hostBuilderContext;
        }

        #endregion

        #region IHostLoggingConfigurator

        /// <inheritdoc/>
        public virtual void ConfigureLogging(HostBuilderContext context, ILoggingBuilder loggingBuilder)
        {

        }

        #endregion
    }
}
