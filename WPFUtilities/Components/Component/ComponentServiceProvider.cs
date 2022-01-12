using System;

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

        /// <summary>
        /// resolve a component by the type. it is configured and built before it is returned
        /// </summary>
        /// <typeparam name="T">component type</typeparam>
        /// <returns>component or null if no dependency has been found</returns>
        public T GetComponent<T>() where T : IServiceComponent
        {
            var component = _host.Host.Services.GetService<T>();
            if (component == null && _host.ParentHost != null)
                return _host.ParentHost.Services.GetComponent<T>();
            InitializeComponent(component);
            return component;
        }

        /// <summary>
        /// resolve a component by the type. it is configured and built before it is returned
        /// </summary>
        /// <param name="type">component type</param>
        /// <returns>component or null if no dependency has been found</returns>
        public IServiceComponent GetComponent(Type type)
        {
            var component = (IServiceComponent)_host.Host.Services.GetService(type);
            if (component == null && _host.ParentHost != null)
                return _host.ParentHost.Services.GetComponent(type);
            InitializeComponent(component);
            return component;
        }

        void InitializeComponent(IServiceComponent component)
        {
            component.ComponentHost.ParentHost = _host;
            component.ConfigureServices();
            component.Build();
        }

    }
}
