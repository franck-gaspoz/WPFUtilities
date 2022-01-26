using System;
using System.Windows;

using WPFUtilities.Components.ServiceComponent;

using properties = WPFUtilities.Components.ServiceComponent.Properties;

namespace WPFUtilities.Components.Services.Properties
{
    /// <summary>
    /// setup data context from component host
    /// </summary>
    public static class DataContext
    {
        #region IsAuto

        /// <summary>
        /// get IsAuto dependency property value for object
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>true if is enabled</returns>
        public static bool GetIsAuto(DependencyObject dependencyObject)
            => (bool)dependencyObject.GetValue(IsAutoProperty);

        /// <summary>
        /// set IsAuto dependency property value for object
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="value">value</param>
        public static void SetIsAuto(DependencyObject dependencyObject, bool value)
            => dependencyObject.SetValue(IsAutoProperty, value);

        /// <summary>
        /// IsAuto dependency property
        /// </summary>
        public static readonly DependencyProperty IsAutoProperty =
            DependencyProperty.RegisterAttached(
                "IsAuto",
                typeof(bool),
                typeof(DataContext),
                new PropertyMetadata(false, IsAutoChanged));

        #endregion

        #region Resolve

        /// <summary>
        /// get Resolve dependency property value for object
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>true if is enabled</returns>
        public static Type GetResolve(DependencyObject dependencyObject)
            => (Type)dependencyObject.GetValue(ResolveProperty);

        /// <summary>
        /// set Resolve dependency property value for object
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="value">value</param>
        public static void SetResolve(DependencyObject dependencyObject, Type value)
            => dependencyObject.SetValue(ResolveProperty, value);

        /// <summary>
        /// Resolve dependency property
        /// </summary>
        public static readonly DependencyProperty ResolveProperty =
            DependencyProperty.RegisterAttached(
                "Resolve",
                typeof(Type),
                typeof(DataContext),
                new PropertyMetadata(null, ResolveChanged));

        #endregion

        #region interactivity

        /// <summary>
        /// trigger setup data context when resolve is set
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="eventArgs">event args</param>
        public static void ResolveChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (!(dependencyObject is FrameworkElement target)
                || !(eventArgs.NewValue is Type type)) return;

            DelayedResolveComponent(target);

            void Initialize(object src, EventArgs e)
            {
                target.Loaded -= Initialize;
                DataContextResolveSetter.SetupServiceDependencyDataContext(
                    target,
                    type,
                    (o) => GetComponentHost(o));
            }

            target.Loaded += Initialize;
        }

        /// <summary>
        /// trigger setup data context when is enabled is set to true
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="eventArgs">event args</param>
        public static void IsAutoChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (!(dependencyObject is FrameworkElement target)
                || !((bool)eventArgs.NewValue)) return;

            DelayedResolveComponent(target);

            void Initialize(object src, EventArgs e)
            {
                target.Loaded -= Initialize;
                DataContextResolveSetter.SetupServiceDependencyDataContext(
                    dependencyObject,
                    (o) => GetComponentHost(o));
            }

            target.Loaded += Initialize;
        }

        #endregion

        /// <summary>
        /// resolve any associated component when framework element is loaded, before data context is initialized
        /// </summary>
        /// <param name="frameworkElement">framework element</param>
        static void DelayedResolveComponent(FrameworkElement frameworkElement)
        {
            void ResolveComponent(object src, EventArgs e)
            {
                frameworkElement.Loaded -= ResolveComponent;
                var componentType = (Type)frameworkElement.GetValue(properties.TypeProperty);
                if (componentType != null)
                {
                    // resolve the component
                    var host = GetComponentHost(frameworkElement);
                    if (host != null)
                    {
                        // resolve the component (that build and init it)
                        var component = host.Services.GetComponent(componentType);
                        // assign contextual host to the framework element
                        frameworkElement.SetValue(
                            properties.ComponentHostProperty,
                            component.ComponentHost);
                    }
                }
            }

            frameworkElement.Loaded += ResolveComponent;
        }

        static IComponentHost GetComponentHost(DependencyObject dependencyObject)
        {
            IComponentHost componentHost = null;
            while (dependencyObject != null &&
                (componentHost = (IComponentHost)dependencyObject
                    .GetValue(properties.ComponentHostProperty)) == null)
            {
                dependencyObject = LogicalTreeHelper.GetParent(dependencyObject);
            }
            return (IComponentHost)componentHost;
        }
    }
}
