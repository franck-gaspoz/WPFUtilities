using System;
using System.Windows;

using WPFUtilities.Components.ServiceComponent;

using properties = WPFUtilities.Components.Services.Properties;

namespace WPFUtilities.Components.Services.Properties
{
    /// <summary>
    /// framework element extensions methods that help resolve services framework element initialization
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

                InitializeSourceServiceAndPerformAction(
                    source,
                    target,
                    serviceType,
                    (service) =>
                    {
                        var targetProperty = target.GetType().GetProperty(targetProperyName)
                            ?? throw new InvalidOperationException("target has no property Command");
                        targetProperty.SetValue(target, service);
                    });
            }

            if (!source.IsLoaded)
                source.Loaded += InitializeAtLoaded;
            else
                InitializeAtLoaded(source, null);
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

                InitializeSourceServiceAndPerformAction(
                    source,
                    target,
                    serviceType,
                    (service) =>
                    {
                        target.SetValue(dependencyProperty, service);
                    });
            }

            if (!source.IsLoaded)
                source.Loaded += InitializeAtLoaded;
            else
                InitializeAtLoaded(source, null);
        }

        /// <summary>
        /// get a service from services collections of source component host. source must be loaded
        /// </summary>
        /// <exception cref="InvalidOperationException">source framework element is not loaded</exception>
        /// <typeparam name="T">service type</typeparam>
        /// <param name="source">framework element source</param>
        /// <returns>service of type T or null if not found</returns>
        public static T GetService<T>(
            FrameworkElement source
            )
            => (T)GetService(source, typeof(T));

        /// <summary>
        /// get a service from services collections of source component host. source must be loaded
        /// </summary>
        /// <exception cref="InvalidOperationException">source framework element is not loaded</exception>
        /// <param name="source">framework element source</param>
        /// <param name="serviceType">service type</param>
        /// <returns>service or null if not found</returns>
        public static object GetService(
            FrameworkElement source,
            Type serviceType
            )
        {
            if (!source.IsLoaded) throw new InvalidOperationException("source is not loaded");
            ComponentHostLookup.SetComponentHostPropertyFromResolvedComponentWhenLoaded(source);
            var service = LookupService(source, serviceType);
            return service;
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
                InitializeSourceServiceAndPerformAction(source, target, serviceType, action);
            }

            if (!source.IsLoaded)
                source.Loaded += InitializeAtLoaded;
            else
                InitializeAtLoaded(source, null);
        }

        /// <summary>
        /// trigger action when source host is initialized (once loaded) requiring a service and concerning a target
        /// </summary>
        /// <param name="source">component host source</param>
        /// <param name="scopeOwner">scope property owner<</param>
        /// <param name="serviceType">required service type</param>
        /// <param name="action">action to be triggered: have the service as parameter</param>
        /// <exception cref="InvalidOperationException">service not found</exception>
        public static void InitializeSourceServiceAndPerformAction(
            FrameworkElement source,
            DependencyObject scopeOwner,
            Type serviceType,
            Action<object> action
            )
        {
            var service = LookupService(source, scopeOwner, serviceType);
            if (service == null) throw new InvalidOperationException($"service not found: {serviceType.Name}");
            action?.Invoke(service);
        }

        /// <summary>
        /// try to find a service from a component host
        /// </summary>
        /// <param name="source">component host source</param>
        /// <param name="serviceType">service type</param>
        /// <returns>service or null if not found</returns>
        public static object LookupService(FrameworkElement source, Type serviceType)
            => LookupService(source, source, serviceType);

        /// <summary>
        /// try to find a service from a component host
        /// </summary>
        /// <param name="source">component host source</param>
        /// <param name="scopeOwner">scope property owner<</param>
        /// <param name="serviceType">service type</param>
        /// <returns>service or null if not found</returns>
        /// <exception cref="InvalidOperationException">source host is null</exception>
        public static object LookupService(FrameworkElement source, DependencyObject scopeOwner, Type serviceType)
        {
            var scope = properties.Scope.GetValue(scopeOwner);

            var host = properties.Component.GetComponentHost(source)
                ?? throw new InvalidOperationException("source host is null");

            if (scope == Scopes.Global)
                host = host.RootHost;

            var service = host.Services.GetService(serviceType);
            return service;
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

        /// <summary>
        /// get a service from a dependency property, if null it is resolve from source component host, if still null the creates function is invokated. 
        /// the dependency property is initialized once instance is resolved or created
        /// </summary>        
        /// <exception cref="InvalidOperationException">source framework element is not loaded</exception>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">source</param>
        /// <param name="dependencyProperty">dependency property storing the service</param>
        /// <param name="create">create function or null</param>
        /// <returns>service or null</returns>
        public static T GetResolveCreateServiceFromProperty<T>(
            FrameworkElement source,
            DependencyProperty dependencyProperty,
            Func<T> create = null)
        {
            var service = (T)source.GetValue(dependencyProperty);
            if (service != null) return service;
            service = GetService<T>(source);
            if (service == null && create != null)
                service = create();
            if (service != null)
                source.SetValue(dependencyProperty, service);
            return default(T);
        }
    }
}
