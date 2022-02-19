using System;
using System.Windows;

using WPFUtilities.Components.Services.Properties;

namespace WPFUtilities.Extensions.Services
{
    /// <summary>
    /// framework element extensions methods that help resolve services framework element initilization
    /// </summary>
    public static class ServicesPropertiesHelperExtensions
    {
        /// <summary>
        /// set a property on target object, resolved from services collections of source component host, when source is loaded
        /// </summary>
        /// <param name="source">framework element source that fires the setup onces loaded</param>
        /// <param name="target">the target object having the porperty that must be setted. The target provides the scope property</param>
        /// <param name="serviceType">service type to be resolved to an instance from the source component host services collection</param>
        /// <param name="targetProperyName">the property name to be setted on the target</param>
        public static void AssignServiceToProperty(
            this FrameworkElement source,
            DependencyObject target,
            Type serviceType,
            string targetProperyName)
            => ServicesPropertiesHelper.AssignServiceToProperty(
                source,
                target,
                serviceType,
                targetProperyName);

        /// <summary>
        /// set a property on target object, resolved from services collections of source component host, when source is loaded
        /// </summary>
        /// <param name="source">framework element source that fires the setup onces loaded</param>
        /// <param name="target">the target object having the property that must be setted. The target provides the scope property</param>
        /// <param name="serviceType">service type to be resolved to an instance from the source component host services collection</param>
        /// <param name="dependencyProperty">the property name to be setted on the target</param>
        public static void AssignServiceToDependencyProperty(
            this FrameworkElement source,
            DependencyObject target,
            Type serviceType,
            DependencyProperty dependencyProperty)
            => ServicesPropertiesHelper.AssignServiceToDependencyProperty(
                source,
                target,
                serviceType,
                dependencyProperty);

        /// <summary>
        /// call an action with a service parameter, resolved from services collections of source component host, when source is loaded
        /// </summary>
        /// <param name="source">framework element source that fires the setup onces loaded</param>
        /// <param name="target">The target provides the scope property</param>
        /// <param name="serviceType">service type to be resolved to an instance from the source component host services collection</param>
        /// <param name="action">action called with service parameter</param>
        public static void WithService(
            this FrameworkElement source,
            DependencyObject target,
            Type serviceType,
            Action<object> action
            )
            => ServicesPropertiesHelper.WithService(
                source,
                target,
                serviceType,
                action
                );

        /// <summary>
        /// call an action with a service parameter, resolved from services collections of source component host, when source is loaded
        /// </summary>
        /// <typeparam name="T">service type to be resolved to an instance from the source component host services collection</typeparam>
        /// <param name="source">framework element source that fires the setup onces loaded</param>
        /// <param name="target">The target provides the scope property</param>
        /// <param name="action">action called with service parameter</param>
        public static void WithService<T>(
            this FrameworkElement source,
            DependencyObject target,
            Action<T> action
            )
            => ServicesPropertiesHelper.WithService<T>(
                source,
                target,
                action
                );

        /// <summary>
        /// call an action with a service parameter, resolved from services collections of source component host, when source is loaded
        /// </summary>
        /// <param name="source">framework element source that fires the setup onces loaded. provides the scope property</param>
        /// <param name="serviceType">service type to be resolved to an instance from the source component host services collection</param>
        /// <param name="action">action called with service parameter</param>
        public static void WithService(
            this FrameworkElement source,
            Type serviceType,
            Action<object> action
            )
            => WithService(source, source, serviceType, action);

        /// <summary>
        /// call an action with a service parameter, resolved from services collections of source component host, when source is loaded
        /// </summary>
        /// <typeparam name="T">service type to be resolved to an instance from the source component host services collection</typeparam>
        /// <param name="source">framework element source that fires the setup onces loaded. provides the scope property</param>
        /// <param name="action">action called with service parameter</param>
        public static void WithService<T>(
            this FrameworkElement source,
            Action<T> action
            )
            => WithService<T>(source, action);
    }
}
