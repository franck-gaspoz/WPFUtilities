
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using WPFUtilities.ComponentModels;

namespace WPFUtilities.Components.Component
{
    /// <summary>
    /// service component abstraction
    /// </summary>
    public abstract class AbstractServiceComponent : IServiceComponent, IServicesConfigurator
    {
        /// <summary>
        /// component host
        /// </summary>
        public IComponentHost ComponentHost { get; protected set; }
            = new ComponentHost();

        /// <summary>
        /// create a new service component instance
        /// <para>can get services dependencies trought constructor</para>
        /// <para>get parent scope dependencies</para>
        /// </summary>
        public AbstractServiceComponent()
        {

        }

        /// <inheritdoc/>
        public void Configure()
        {
            ComponentHost.HostBuilder.ConfigureServices((context, services) =>
            {
                ConfigureServices(context, services);
            });
        }

        /// <inheritdoc/>
        public virtual void Build() => ComponentHost.Build();

        /// <inheritdoc/>
        public abstract void ConfigureServices(HostBuilderContext context, IServiceCollection services);
    }
}
