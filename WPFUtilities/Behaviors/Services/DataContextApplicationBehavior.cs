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
                typeof(DataContextApplicationBehavior),
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
        public static void IsAutoChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
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
