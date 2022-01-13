using System;

namespace WPFUtilities.Components.Component
{
    /// <summary>
    /// component host service provider
    /// </summary>
    public interface IComponentServiceProvider
    {
        /// <summary>
        /// resolve a component by the type. it is configured and built before it is returned
        /// </summary>
        /// <typeparam name="T">component type</typeparam>
        /// <returns>component or null if no dependency has been found</returns>
        T GetComponent<T>() where T : IServiceComponent;

        /// <summary>
        /// resolve a component by the type. it is configured and built before it is returned
        /// </summary>
        /// <param name="type">component type</param>
        /// <returns>component or null if no dependency has been found</returns>
        IServiceComponent GetComponent(Type type);

        /// <summary>
        /// resolve a service by the type
        /// </summary>
        /// <param name="type">component type</param>
        /// <returns>service or null if no dependency has been found</returns>
        object GetService(Type type);

    }
}
