using System;
using System.Windows;

using WPFUtilities.Components.ServiceComponent;

namespace WPFUtilities.Components.Services.Properties
{
    /// <summary>
    /// resolve a command from di scope
    /// </summary>
    public static class Command
    {
        /// <summary>
        /// get Type dependency property value for object
        /// <para>requires the Component.Host property to be set in the dependency object</para>
        /// <para>or require the property Component.Type to be setted in the dependency object</para>
        /// <para>and performs a lookup of the component host in parents logical tree, then sets the component host property</para>
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>true if is enabled</returns>
        public static Type GetType(DependencyObject dependencyObject)
            => (Type)dependencyObject.GetValue(TypeProperty);

        /// <summary>
        /// set Type dependency property value for object
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="value">value</param>
        public static void SetType(DependencyObject dependencyObject, Type value)
            => dependencyObject.SetValue(TypeProperty, value);

        /// <summary>
        /// Type dependency property
        /// </summary>
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.RegisterAttached(
                "Type",
                typeof(Type),
                typeof(Command),
                new PropertyMetadata(null, TypeChanged));

        /// <summary>
        /// trigger setup command when type is set
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="eventArgs">event args</param>
        public static void TypeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (!(dependencyObject is FrameworkElement target)
                || !(eventArgs.NewValue is Type type)) return;

            ComponentHostLookup.SetComponentHostPropertyFromResolvedComponentWhenLoaded(target);

            /*void Initialize(object src, EventArgs e)
            {
                target.Loaded -= Initialize;
                DataContextResolveSetter.SetupServiceDependencyDataContext(
                    target,
                    type,
                    (o) => GetComponentHost(o));
            }

            target.Loaded += Initialize;*/
        }
    }
}
