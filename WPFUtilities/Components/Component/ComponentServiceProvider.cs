using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

namespace WPFUtilities.Components.Component
{
    /// <summary>
    /// component host service provider
    /// </summary>
    public class ComponentServiceProvider
        : IComponentServiceProvider
    {
        IComponentHost _host;

        /// <summary>
        /// service provider
        /// </summary>
        /// <param name="host"></param>
        public ComponentServiceProvider(IComponentHost host)
        {
            _host = host;
        }

        /// <inheritdoc/>
        public T GetComponent<T>() where T : IServiceComponent
        {
            var component = _host.Host.Services.GetService<T>();
            if (component == null && _host.ParentHost != null)
                return _host.ParentHost.Services.GetComponent<T>();
            InitializeComponent(component);
            return component;
        }

        /// <inheritdoc/>
        public IServiceComponent GetComponent(Type type)
        {
            var component = (IServiceComponent)_host.Host.Services.GetService(type);
            if (component == null && _host.ParentHost != null)
                return _host.ParentHost.Services.GetComponent(type);
            InitializeComponent(component);
            return component;
        }

        /// <inheritdoc/>
        public object GetService(Type type)
        {
            var service = _host.Host.Services.GetService(type);
            if (service == null && _host.ParentHost != null)
                return _host.ParentHost.Services.GetService(type);
            return service;
        }

        /// <inheritdoc/>
        public T GetService<T>()
        {
            var service = _host.Host.Services.GetService<T>();
            if (service == null && _host.ParentHost != null)
                return _host.ParentHost.Services.GetService<T>();
            return service;
        }

        /// <inheritdoc/>
        public IEnumerable<object> GetServices(Type type)
        {
            var services = _host.Host.Services.GetServices(type);
            if (services == null && _host.ParentHost != null)
                return _host.ParentHost.Services.GetServices(type);
            return services;
        }

        /// <inheritdoc/>
        public IEnumerable<T> GetServices<T>()
        {
            var services = _host.Host.Services.GetServices<T>();
            if (services == null && _host.ParentHost != null)
                return _host.ParentHost.Services.GetServices<T>();
            return services;
        }

        /// <inheritdoc/>
        public T GetRequiredComponent<T>() where T : IServiceComponent
        {
            var component = GetComponent<T>();
            if (component == null) throw new InvalidOperationException($"no service of type {typeof(T).Name} has been found");
            return component;
        }

        /// <inheritdoc/>
        public IServiceComponent GetRequiredComponent(Type type)
        {
            var component = (IServiceComponent)_host.Host.Services.GetService(type);
            if (component == null && _host.ParentHost != null)
                return _host.ParentHost.Services.GetComponent(type);
            InitializeComponent(component);
            return component;
        }

        /// <inheritdoc/>
        public object GetRequiredService(Type type)
        {
            var service = _host.Host.Services.GetService(type);
            if (service == null && _host.ParentHost != null)
                return _host.ParentHost.Services.GetService(type);
            return service;
        }

        /// <inheritdoc/>
        public T GetRequiredService<T>()
        {
            var service = _host.Host.Services.GetService<T>();
            if (service == null && _host.ParentHost != null)
                return _host.ParentHost.Services.GetService<T>();
            return service;
        }

        /// <inheritdoc/>
        public IEnumerable<object> GetRequiredServices(Type type)
        {
            var services = _host.Host.Services.GetServices(type);
            if (services == null && _host.ParentHost != null)
                return _host.ParentHost.Services.GetServices(type);
            return services;
        }

        /// <inheritdoc/>
        public IEnumerable<T> GetRequiredServices<T>()
        {
            var services = _host.Host.Services.GetServices<T>();
            if (services == null && _host.ParentHost != null)
                return _host.ParentHost.Services.GetServices<T>();
            return services;
        }

        void InitializeComponent(IServiceComponent component)
        {
            component.ComponentHost.ParentHost = _host;
            component.ConfigureServices();
            component.Build();
        }

    }
}
