using System;
using System.Windows;

using Microsoft.Xaml.Behaviors;

using WPFUtilities.Components.Application;
using WPFUtilities.Components.Component;

namespace WPFUtilities.Behaviors.Services
{
    /// <summary>
    /// setup data context from application host
    /// </summary>
    public class DataContextApplicationBehavior : Behavior<FrameworkElement>
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
                typeof(DataContextApplicationBehavior),
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
                typeof(DataContextApplicationBehavior),
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
            if (!(dependencyObject is FrameworkElement target)) return;
            if (eventArgs.NewValue is Type type)
                DataContextResolveSetter.
                    SetupServiceDependencyDataContext(
                        dependencyObject,
                        type,
                        (o) => GetComponentHost());
        }

        /// <summary>
        /// trigger setup data context when is enabled is set to true
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="eventArgs">event args</param>
        public static void IsEnabledChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (!(dependencyObject is FrameworkElement target)) return;
            if ((bool)eventArgs.NewValue)
                DataContextResolveSetter
                    .SetupServiceDependencyDataContext(
                        dependencyObject,
                        (o) => GetComponentHost());
        }

        /// <summary>
        /// trigger setup data context when attached
        /// <para>Behaviors:Interaction.Behaviors</para>
        /// </summary>
        protected override void OnAttached()
            => DataContextResolveSetter.SetupServiceDependencyDataContext(
                AssociatedObject,
                (o) => GetComponentHost());

        #endregion

        static IComponentHost GetComponentHost()
        {
            var application = System.Windows.Application.Current as
                ApplicationBase;
            if (application != null) return application.ApplicationHost;
            return null;
        }
    }
}
