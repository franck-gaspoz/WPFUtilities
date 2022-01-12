using System;

using Microsoft.Extensions.DependencyInjection;

namespace WPFUtilities.Components.Component
{
    /// <summary>
    /// component provider
    /// </summary>
    public class ComponentProvider
    {
        readonly IServiceCollection _services;

        /// <summary>
        /// component provider
        /// </summary>
        /// <param name="services">services</param>
        public ComponentProvider(IServiceCollection services)
        {

        }

        /// <summary>
        /// resolve a component from its service type 
        /// </summary>
        /// <param name="serviceType">service type</param>
        /// <returns>component or null if not found</returns>
        public object ResolveComponent(Type serviceType)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// resolve a component from its service type
        /// </summary>
        /// <typeparam name="T">component type</typeparam>
        /// <param name="serviceType">service type</param>
        /// <returns>component or null if not found</returns>
        public T GetComponent<T>(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}
