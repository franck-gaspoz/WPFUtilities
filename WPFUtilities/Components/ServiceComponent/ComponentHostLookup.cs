using System;
using System.Windows;

using properties = WPFUtilities.Components.Services.Properties;

namespace WPFUtilities.Components.ServiceComponent
{
    /// <summary>
    /// component host lookup feature
    /// </summary>
    public static class ComponentHostLookup
    {
        /// <summary>
        /// find a component host from the dependency object attached property ComponentHostProperty. Recurse up in logicial tree
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>narrow component host or null if not found</returns>
        public static IComponentHost GetComponentHost(DependencyObject dependencyObject)
        {
            IComponentHost componentHost = null;
            while (dependencyObject != null &&
                (componentHost = (IComponentHost)dependencyObject
                    .GetValue(properties.Component.ComponentHostProperty)) == null)
            {
                dependencyObject = LogicalTreeHelper.GetParent(dependencyObject);
            }
            return (IComponentHost)componentHost;
        }

        /// <summary>
        /// resolve the component host the element belongs to when the element is loaded. do it immediately if already loaded
        /// <para>does nothing if component host is already set</para>
        /// <para>does nothing if component host is set in dependency object component host property</para>
        /// <para>if component type is set in dependency object type property, try to find and init component property from inherited component host, then set component property</para>
        /// <para>if no type property, try to set component host property from inherited (logical tree parents) component host</para>
        /// </summary>
        /// <param name="frameworkElement">framework element</param>
        public static void SetComponentHostPropertyFromResolvedComponentWhenLoaded(FrameworkElement frameworkElement)
        {
            if (!frameworkElement.IsLoaded)
                frameworkElement.Loaded += ResolveAndSetComponent;
            else
                ResolveAndSetComponent(frameworkElement, null);
        }

        static void ResolveAndSetComponent(object src, EventArgs e)
        {
            if (!(src is FrameworkElement frameworkElement)) return;

            frameworkElement.Loaded -= ResolveAndSetComponent;

            IComponentHost host;
            host = properties.Component.GetComponentHost(frameworkElement);
            if (host != null) return;

            var componentType = (Type)frameworkElement.GetValue(properties.Component.TypeProperty);
            if (componentType != null)
            {
                // resolve the component
                host = ComponentHostLookup.GetComponentHost(frameworkElement);
                if (host != null)
                {
                    // resolve the component (that build and init it)
                    var component = host.Services.GetComponent(componentType);
                    // assign contextual host to the framework element
                    // from the associated component if found, otherwize to the context owner
                    properties.Component.SetComponentHost(
                        frameworkElement,
                        component != null ? component.ComponentHost : host);
                }
            }
            else
            {
                // no type provided, set from inherited if any
                host = GetComponentHost(frameworkElement);
                if (host != null)
                    properties.Component.SetComponentHost(frameworkElement, host);
            }
        }
    }
}
