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
                typeof(DataContextBehavior),
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
                typeof(DataContextBehavior),
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

            void Initialize(object src, EventArgs e)
            {
                target.Loaded -= Initialize;
                DataContextResolveSetter.SetupServiceDependencyDataContext(
                    dependencyObject,
                    (o) => GetComponentHost(o));
            }

            target.Loaded += Initialize;
        }

        /// <summary>
        /// trigger setup data context when attached
        /// <para>Behaviors:Interaction.Behaviors</para>
        /// </summary>
        protected override void OnAttached()
        {
            void Initialize(object src, EventArgs e)
            {
                AssociatedObject.Loaded -= Initialize;
                DataContextResolveSetter.SetupServiceDependencyDataContext(
                    AssociatedObject,
                    (o) => GetComponentHost(o));
            }

            AssociatedObject.Loaded += Initialize;
        }

        #endregion

        static IComponentHost GetComponentHost(DependencyObject dependencyObject)
            => (IComponentHost)dependencyObject.GetValue(AttachedProperties.ComponentHostProperty);
    }
}
