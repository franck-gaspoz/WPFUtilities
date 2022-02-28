using System;
using System.ComponentModel;
using System.Windows;

using WPFUtilities.Components.ServiceComponent;

namespace WPFUtilities.Components.Services.Properties
{
    /// <summary>
    /// setup data context from component host
    /// <para>requires the Component.Host property to be set in the dependency object</para>
    /// <para>or require the property Component.Type to be setted in the dependency object</para>
    /// <para>and performs a lookup of the component host in parents logical tree, then sets the component host property</para>
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
            if (DesignerProperties.GetIsInDesignMode(dependencyObject)) return;
            if (!(dependencyObject is FrameworkElement target)
                || !(eventArgs.NewValue is Type type)) return;

            ComponentHostLookup.SetComponentHostPropertyFromResolvedComponentWhenLoaded(target);

            void Initialize(object src, EventArgs e)
            {
                target.Loaded -= Initialize;
                DataContextResolveSetter.SetupServiceDependencyDataContext(
                    target,
                    type,
                    (o) => ComponentHostLookup.GetComponentHost(o));
            }

            target.Loaded += Initialize;
        }

        /// <summary>
        /// trigger setup data context when is auto is set to true
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="eventArgs">event args</param>
        public static void IsAutoChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (DesignerProperties.GetIsInDesignMode(dependencyObject)) return;
            if (!(dependencyObject is FrameworkElement target)
                || !((bool)eventArgs.NewValue)) return;

            ComponentHostLookup.SetComponentHostPropertyFromResolvedComponentWhenLoaded(target);

            void Initialize(object src, EventArgs e)
            {
                target.Loaded -= Initialize;
                DataContextResolveSetter.SetupServiceDependencyDataContext(
                    dependencyObject,
                    (o) => ComponentHostLookup.GetComponentHost(o));
            }

            target.Loaded += Initialize;
        }

        #endregion

    }
}
