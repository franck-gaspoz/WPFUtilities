
using System;
using System.Collections.Generic;
using System.ComponentModel;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using WPFUtilities.ComponentModels;
using WPFUtilities.Components.Services;

using mshosting = Microsoft.Extensions.Hosting;

namespace WPFUtilities.Components.ServiceComponent
{
    /// <summary>
    /// abstraction of a component
    /// </summary>
    public class ComponentHost :
        IComponentHost,
        IServicesConfigurator,
        IHostLoggingConfigurator
    {
        #region properties

        string _name;
        /// <inheritdoc/>
        public string Name
        {
            get => _name ?? GetType().Name;
            set => _name = value;
        }

        /// <inheritdoc/>
        public IHost Host { get; protected set; }

        /// <inheritdoc/>
        public BindingList<IComponentHost> ChildHosts { get; private set; }

        /// <inheritdoc/>
        public void OnChildHostsChanged(ChildHostsCollectionChangedEventArgs args = null)
        {
            var eventArgs = args ?? new ChildHostsCollectionChangedEventArgs
            {
                Host = this
            };
            ChildHostsCollectionChangedEvent?.Invoke(this, eventArgs);
            if (RootHost != null && RootHost != this)
                RootHost.OnChildHostsChanged(eventArgs);
        }

        /// <inheritdoc/>
        public event EventHandler<ChildHostsCollectionChangedEventArgs> ChildHostsCollectionChangedEvent;

        /// <inheritdoc/>
        public event EventHandler<ParentHostChangedEventArgs> ParentHostChangedEvent;

        /// <inheritdoc/>
        public void RegisterChildHost(IComponentHost host)
        {
            if (!ChildHosts.Contains(host))
                ChildHosts.Add(host);
        }

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

        IComponentHost _parentHost;
        /// <inheritdoc/>
        public IComponentHost ParentHost
        {
            get => _parentHost;
            set
            {
                var oldValue = _parentHost;
                _parentHost = value;
                OnParentHostChanged(
                    new ParentHostChangedEventArgs
                    {
                        Host = this,
                        OldParent = oldValue,
                        NewParent = _parentHost
                    });
            }
        }

        void OnParentHostChanged(ParentHostChangedEventArgs args)
            => ParentHostChangedEvent?.Invoke(this, args);

        /// <inheritdoc/>
        public IComponentHost RootHost
        {
            get
            {
                IComponentHost rootHost = this;
                while (rootHost.ParentHost != null)
                    rootHost = rootHost.ParentHost;
                return rootHost;
            }
        }

        #endregion

        /// <summary>
        /// creates a new root component host
        /// </summary>
        public ComponentHost() => Initialize();

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
            ChildHosts = new BindingList<IComponentHost>();
            ChildHosts.ListChanged += (o, e) => OnChildHostsChanged();
            Services = new ServiceComponentProvider(this);
            HostBuilder = mshosting.Host.CreateDefaultBuilder();
            HostBuilder.ConfigureServices((context, services) => ConfigureServices(context, services));
            HostBuilder.ConfigureLogging((context, loggingBuilder) => ConfigureLogging(context, loggingBuilder));
        }

        private void ChildHosts_ListChanged(object sender, ListChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Build()
        {
            Host = HostBuilder.Build();
            HostBuilderContext.Properties.Add(typeof(IHost), Host);
            HostBuilderContext.Properties.Add(typeof(IComponentHost), this);
        }

        /// <inheritdoc/>
        public string FullName
        {
            get
            {
                var parts = new List<string>();
                IComponentHost host = this;
                while (host != null)
                {
                    parts.Add(host.Name);
                    host = host.ParentHost;
                }
                parts.Reverse();
                return string.Join(".", parts);
            }
        }

        #region IHostServicesConfigurator

        /// <inheritdoc/>
        public virtual void ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection services)
        {
            HostBuilderContext = hostBuilderContext;
            services.AddSingleton<IComponentHost, ComponentHost>((serviceProvider) => this);
            services.AddSingleton<IServiceComponentProvider>((serviceProvider) => Services);
        }

        #endregion

        #region IHostLoggingConfigurator

        /// <inheritdoc/>
        public virtual void ConfigureLogging(HostBuilderContext context, ILoggingBuilder loggingBuilder)
        {

        }

        #endregion

        /// <inheritdoc/>
        public override string ToString()
            => this.FullName;
    }
}
