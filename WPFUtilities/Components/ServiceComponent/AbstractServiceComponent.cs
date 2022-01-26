using Microsoft.Extensions.Hosting;

using WPFUtilities.ComponentModels;

namespace WPFUtilities.Components.ServiceComponent
{
    /// <summary>
    /// service component abstraction
    /// </summary>
    public abstract class AbstractServiceComponent : IServiceComponent, IServiceComponentsConfigurator
    {
        static InstanceCounter _instanceCounter = new InstanceCounter();

        /// <summary>
        /// component host
        /// </summary>
        public IComponentHost ComponentHost { get; protected set; }
            = new ComponentHost();

        /// <inheritdoc/>
        public bool IsBuilt { get; private set; }

        /// <summary>
        /// create a new service component instance
        /// <para>can get services dependencies trought constructor</para>
        /// <para>get parent scope dependencies</para>
        /// </summary>
        public AbstractServiceComponent()
        {
            var type = this.GetType();
            _instanceCounter.Increment(type);
            ComponentHost.Name = type.Name + $"[#{_instanceCounter[type]}]";
        }

        /// <summary>
        /// returns the component host name
        /// </summary>
        /// <returns>a string indicating component host types and instance number</returns>
        public override string ToString()
            => ComponentHost.ToString();

        /// <inheritdoc/>
        public void Configure()
        {
            ComponentHost.HostBuilder.ConfigureServices((context, services) =>
            {
                ConfigureServices(
                    context,
                    new ServiceComponentCollection(ComponentHost, services)
                    );
            });
        }

        /// <inheritdoc/>
        public virtual void Build()
        {
            ComponentHost.Build();
            IsBuilt = true;
        }

        /// <inheritdoc/>
        public abstract void ConfigureServices(HostBuilderContext context, IServiceComponentCollection services);
    }
}
