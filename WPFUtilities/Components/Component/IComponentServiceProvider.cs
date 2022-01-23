﻿using System;
using System.Collections.Generic;

namespace WPFUtilities.Components.Component
{
    /// <summary>
    /// component host service provider
    /// </summary>
    public interface IComponentServiceProvider
    {
        /// <summary>
        /// get a component by the type. it is configured and built before it is returned
        /// </summary>
        /// <typeparam name="T">component type</typeparam>
        /// <returns>component or null if no dependency has been found</returns>
        T GetComponent<T>() where T : IServiceComponent;

        /// <summary>
        /// get a component by the type. it is configured and built before it is returned
        /// </summary>
        /// <param name="type">component type</param>
        /// <returns>component or null if no dependency has been found</returns>
        IServiceComponent GetComponent(Type type);

        /// <summary>
        /// get a service by the type
        /// </summary>
        /// <param name="type">component type</param>
        /// <returns>service or null if no dependency has been found</returns>
        object GetService(Type type);

        /// <summary>
        /// get a service by the type
        /// </summary>
        /// <typeparam name="T">component type</typeparam>
        /// <returns>service or null if no dependency has been found</returns>
        T GetService<T>();

        /// <summary>
        /// get an enumeration of services of type
        /// </summary>
        /// <param name="type">component type</param>
        /// <returns>services enumeration. empty if no dependency has been found</returns>
        IEnumerable<object> GetServices(Type type);

        /// <summary>
        /// get an enumeration of services of type
        /// </summary>
        /// <typeparam name="T">component type</typeparam>
        /// <returns>services enumeration. empty if no dependency has been found</returns>
        IEnumerable<T> GetServices<T>();

        /// <summary>
        /// get a component by the type. it is configured and built before it is returned
        /// </summary>
        /// <typeparam name="T">component type</typeparam>
        /// <exception cref="System.InvalidOperationException">there is not service of given type</exception>
        /// <returns>component</returns>
        T GetRequiredComponent<T>() where T : IServiceComponent;

        /// <summary>
        /// get a component by the type. it is configured and built before it is returned
        /// </summary>
        /// <param name="type">component type</param>
        /// <exception cref="System.InvalidOperationException">there is not service of given type</exception>
        /// <returns>component</returns>
        IServiceComponent GetRequiredComponent(Type type);

        /// <summary>
        /// get a service by the type
        /// </summary>
        /// <param name="type">component type</param>
        /// <exception cref="System.InvalidOperationException">there is not service of given type</exception>
        /// <returns>service</returns>
        object GetRequiredService(Type type);

        /// <summary>
        /// get a service by the type
        /// </summary>
        /// <typeparam name="T">component type</typeparam>
        /// <exception cref="System.InvalidOperationException">there is not service of given type</exception>
        /// <returns>service</returns>
        T GetRequiredService<T>();

        /// <summary>
        /// get an enumeration of services of type
        /// </summary>
        /// <param name="type">component type</param>
        /// <exception cref="System.InvalidOperationException">there is not service of given type</exception>
        /// <returns>services</returns>
        IEnumerable<object> GetRequiredServices(Type type);

        /// <summary>
        /// get an enumeration of services of type
        /// </summary>
        /// <typeparam name="T">component type</typeparam>
        /// <exception cref="System.InvalidOperationException">there is not service of given type</exception>
        /// <returns>services enumeration</returns>
        IEnumerable<T> GetRequiredServices<T>();

    }
}
