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
        /// resolve any associated component when framework element is loaded, before data context is initialized
        /// <para>does nothing if component host ais already set</para>
        /// </summary>
        /// <param name="frameworkElement">framework element</param>
        public static void SetComponentHostPropertyFromResolvedComponentWhenLoaded(FrameworkElement frameworkElement)
        {
            void ResolveAndSetComponent(object src, EventArgs e)
            {
                frameworkElement.Loaded -= ResolveAndSetComponent;
                var componentType = (Type)frameworkElement.GetValue(properties.Component.TypeProperty);
                if (componentType != null)
                {
                    // resolve the component
                    var host = ComponentHostLookup.GetComponentHost(frameworkElement);
                    if (host != null)
                    {
                        // resolve the component (that build and init it)
                        var component = host.Services.GetComponent(componentType);
                        // assign contextual host to the framework element
                        frameworkElement.SetValue(
                            properties.Component.ComponentHostProperty,
                            component.ComponentHost);
                    }
                }
            }

            frameworkElement.Loaded += ResolveAndSetComponent;
        }
    }
}
