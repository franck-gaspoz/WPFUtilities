using System;
using System.Windows;

using Microsoft.Xaml.Behaviors;

using WPFUtilities.Components.Component;

namespace WPFUtilities.Behaviors.Services
{
    /// <summary>
    /// setup data context from component host
    /// </summary>
    public class DataContextBehavior : Behavior<FrameworkElement>
    {
        #region IsEnabled

        /// <summary>
        /// get IsEnabled dependency property value for object
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>true if is enabled</returns>
        public static bool GetIsEnabled(DependencyObject dependencyObject)
            => (bool)dependencyObject.GetValue(IsEnabledProperty);

        /// <summary>
        /// set IsEnabled dependency property value for object
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="value">value</param>
        public static void SetIsEnabled(DependencyObject dependencyObject, bool value)
            => dependencyObject.SetValue(IsEnabledProperty, value);

        /// <summary>
        /// IsEnabled dependency property
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled",
                typeof(bool),
                typeof(DataContextBehavior),
                new PropertyMetadata(false, IsEnabledChanged));

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
                typeof(DataContextBehavior),
                new PropertyMetadata(null, ResolveChanged));

        #endregion

        #region interactivity

        static bool _isInitialized = false;

        /// <summary>
        /// trigger setup data context when resolve is set
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="eventArgs">event args</param>
        public static void ResolveChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (!(dependencyObject is FrameworkElement target)
                || !(eventArgs.NewValue is Type type)) return;

            target.Loaded += (src, e) =>
            {
                if (!_isInitialized)
                {
                    DataContextResolveSetter.SetupServiceDependencyDataContext(
                        target,
                        type,
                        (o) => GetComponentHost(o));
                    _isInitialized = true;
                }
            };
        }

        /// <summary>
        /// trigger setup data context when is enabled is set to true
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="eventArgs">event args</param>
        public static void IsEnabledChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (!(dependencyObject is FrameworkElement target)
                || !((bool)eventArgs.NewValue)) return;

            target.Loaded += (src, e) =>
            {
                if (!_isInitialized)
                {
                    DataContextResolveSetter.SetupServiceDependencyDataContext(
                        dependencyObject,
                        (o) => GetComponentHost(o));
                    _isInitialized = true;
                }
            };
        }

        /// <summary>
        /// trigger setup data context when attached
        /// <para>Behaviors:Interaction.Behaviors</para>
        /// </summary>
        protected override void OnAttached()
        {
            AssociatedObject.Loaded += (src, e) =>
            {
                if (!_isInitialized)
                {
                    DataContextResolveSetter.SetupServiceDependencyDataContext(
                        AssociatedObject,
                        (o) => GetComponentHost(o));
                    _isInitialized = true;
                }
            };
        }

        #endregion

        static IComponentHost GetComponentHost(DependencyObject dependencyObject)
            => (IComponentHost)dependencyObject.GetValue(AttachedProperties.ComponentHostProperty);
    }
}
