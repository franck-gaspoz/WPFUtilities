using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

using WPFUtilities.Components.ServiceComponent;

using properties = WPFUtilities.Components.Services.Properties;

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
            if (DesignerProperties.GetIsInDesignMode(dependencyObject))
                return;

            if (!(dependencyObject is FrameworkElement target)
                || !(eventArgs.NewValue is Type type)) return;

            ComponentHostLookup.SetComponentHostPropertyFromResolvedComponentWhenLoaded(target);

            void InitializeAtLoaded(object src, EventArgs e)
            {
                target.Loaded -= InitializeAtLoaded;
                var host = properties.Component.GetComponentHost(dependencyObject)
                    ?? throw new InvalidOperationException("target host is null");
                var command = host.Services.GetRequiredService(type);
                var commandSource = target as ICommandSource
                    ?? throw new InvalidOperationException("target is not ICommandSource");
                var commandProperty = target.GetType().GetProperty("Command")
                    ?? throw new InvalidOperationException("target has no property Command");
                commandProperty.SetValue(target, command);
            }

            target.Loaded += InitializeAtLoaded;
        }
    }
}
