using System;
using System.Windows;

using WPFUtilities.Components.ServiceComponent;

using properties = WPFUtilities.Components.Services.Properties;

namespace WPFUtilities.Components.Services.Properties
{
    /// <summary>
    /// framework element extensions methods that help resolve services framework element initilization
    /// </summary>
    public static class ServicesPropertiesHelper
    {
        /// <summary>
        /// set a property on target object, resolved from services collections of source component host, when source is loaded
        /// </summary>
        /// <param name="source">framework element source that fires the setup onces loaded</param>
        /// <param name="target">the target object having the porperty that must be setted. The target provides the scope property</param>
        /// <param name="serviceType">service type to be resolved to an instance from the source component host services collection</param>
        /// <param name="targetProperyName">the property name to be setted on the target</param>
        public static void AssignServiceToProperty(
            FrameworkElement source,
            DependencyObject target,
            Type serviceType,
            string targetProperyName)
        {
            ComponentHostLookup.SetComponentHostPropertyFromResolvedComponentWhenLoaded(source);

            void InitializeAtLoaded(object src, EventArgs e)
            {
                source.Loaded -= InitializeAtLoaded;

                var scope = properties.Scope.GetValue(target);

                var host = properties.Component.GetComponentHost(source)
                    ?? throw new InvalidOperationException("source host is null");

                if (scope == Scopes.Global)
                    host = host.RootHost;

                var command = host.Services.GetRequiredService(serviceType);

                var targetProperty = target.GetType().GetProperty(targetProperyName)
                    ?? throw new InvalidOperationException("target has no property Command");

                targetProperty.SetValue(target, command);
            }

            source.Loaded += InitializeAtLoaded;
        }

        /// <summary>
        /// set a property on target object, resolved from services collections of source component host, when source is loaded
        /// </summary>
        /// <param name="source">framework element source that fires the setup onces loaded</param>
        /// <param name="target">the target object having the property that must be setted. The target provides the scope property</param>
        /// <param name="serviceType">service type to be resolved to an instance from the source component host services collection</param>
        /// <param name="dependencyProperty">the property name to be setted on the target</param>
        public static void AssignServiceToDependencyProperty(
            FrameworkElement source,
            DependencyObject target,
            Type serviceType,
            DependencyProperty dependencyProperty)
        {
            ComponentHostLookup.SetComponentHostPropertyFromResolvedComponentWhenLoaded(source);

            void InitializeAtLoaded(object src, EventArgs e)
            {
                source.Loaded -= InitializeAtLoaded;

                var scope = properties.Scope.GetValue(target);

                var host = properties.Component.GetComponentHost(source)
                    ?? throw new InvalidOperationException("source host is null");

                if (scope == Scopes.Global)
                    host = host.RootHost;

                var command = host.Services.GetRequiredService(serviceType);

                target.SetValue(dependencyProperty, command);
            }

            source.Loaded += InitializeAtLoaded;
        }

        /// <summary>
        /// call an action with a service parameter, resolved from services collections of source component host, when source is loaded
        /// </summary>
        /// <param name="source">framework element source that fires the setup onces loaded</param>
        /// <param name="target">The target provides the scope property</param>
        /// <param name="serviceType">service type to be resolved to an instance from the source component host services collection</param>
        /// <param name="action">action called with service parameter</param>
        public static void WithService(
            FrameworkElement source,
            DependencyObject target,
            Type serviceType,
            Action<object> action
            )
        {
            ComponentHostLookup.SetComponentHostPropertyFromResolvedComponentWhenLoaded(source);

            void InitializeAtLoaded(object src, EventArgs e)
            {
                source.Loaded -= InitializeAtLoaded;

                var scope = properties.Scope.GetValue(target);

                var host = properties.Component.GetComponentHost(source)
                    ?? throw new InvalidOperationException("source host is null");

                if (scope == Scopes.Global)
                    host = host.RootHost;

                var service = host.Services.GetRequiredService(serviceType);
                action?.Invoke(service);
            }

            source.Loaded += InitializeAtLoaded;
        }

        /// <summary>
        /// call an action with a service parameter, resolved from services collections of source component host, when source is loaded
        /// </summary>
        /// <typeparam name="T">service type to be resolved to an instance from the source component host services collection</typeparam>
        /// <param name="source">framework element source that fires the setup onces loaded</param>
        /// <param name="target">The target provides the scope property</param>
        /// <param name="action">action called with service parameter</param>
        public static void WithService<T>(
            FrameworkElement source,
            DependencyObject target,
            Action<T> action
            )
            => WithService(source, target, typeof(T), (o) => action((T)o));

        /// <summary>
        /// call an action with a service parameter, resolved from services collections of source component host, when source is loaded
        /// </summary>
        /// <param name="source">framework element source that fires the setup onces loaded. provides the scope property</param>
        /// <param name="serviceType">service type to be resolved to an instance from the source component host services collection</param>
        /// <param name="action">action called with service parameter</param>
        public static void WithService(
            FrameworkElement source,
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
            FrameworkElement source,
            Action<T> action
            )
            => WithService(source, source, typeof(T), (o) => action((T)o));
    }
}
