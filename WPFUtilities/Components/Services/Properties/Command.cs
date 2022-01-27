using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;

using Microsoft.Xaml.Behaviors;

using WPFUtilities.Components.ServiceComponent;
using WPFUtilities.Extensions.Behaviors;

using properties = WPFUtilities.Components.Services.Properties;

namespace WPFUtilities.Components.Services.Properties
{
    /// <summary>
    /// resolve a command from di scope
    /// <para>use application host if Scope value property is not defined or set to Global</para>
    /// <para>else:</para>
    /// <para>requires the Component.Host property to be set in the dependency object</para>
    /// <para>or require the property Component.Type to be setted in the dependency object</para>
    /// <para>and performs a lookup of the component host in parents logical tree, then sets the component host property</para>
    /// </summary>
    public static class Command
    {
        #region type

        /// <summary>
        /// get Type dependency property value for object
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

        #endregion

        /// <summary>
        /// trigger setup command when type is set
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="eventArgs">event args</param>
        public static void TypeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (DesignerProperties.GetIsInDesignMode(dependencyObject))
                return;

            if (!(eventArgs.NewValue is Type type)) return;
            if (dependencyObject is FrameworkElement frameworkElement)
                SetupFrameworkElementFromCommandType(frameworkElement, frameworkElement, type);
            else
            {
                if (dependencyObject is Behavior behavior)
                    SetupBehaviorFromCommandType(behavior, type);
                else
                    throw new InvalidOperationException($"can't setup Command property on element '{dependencyObject}' of Type '{dependencyObject.GetType().Name}'");
            }
        }

        static void SetupBehaviorFromCommandType(Behavior behavior, Type type)
            =>
            behavior.AddChangedEventHandler
                (typeof(Command),
                nameof(BehaviorAssociatedObjectChanged));

        static void BehaviorAssociatedObjectChanged(object sender, EventArgs e)
        {
            if (sender is Behavior behavior)
            {
                var getAssociatedObject = behavior.GetType().GetMethod("get_AssociatedObject", BindingFlags.NonPublic | BindingFlags.Instance);
                var associatedObject = getAssociatedObject.Invoke(behavior, new object[] { });
                if (associatedObject != null)
                {
                    behavior.RemoveChangedEventHandler
                        (typeof(Command),
                        nameof(BehaviorAssociatedObjectChanged));
                    if (associatedObject is FrameworkElement frameworkElement)
                    {
                        Type type = GetType(behavior);
                        SetupFrameworkElementFromCommandType(frameworkElement, behavior, type);
                    }
                    else
                        throw new Exception($"associated object '{associatedObject}' is not of type FrameworkElement");
                }
            }
            else
                throw new InvalidOperationException($"sender '{sender}' is not of type Behavior");
        }

        static void SetupFrameworkElementFromCommandType(
            FrameworkElement source,
            object target,
            Type type)
        {
            ComponentHostLookup.SetComponentHostPropertyFromResolvedComponentWhenLoaded(source);

            void InitializeAtLoaded(object src, EventArgs e)
            {
                source.Loaded -= InitializeAtLoaded;

                var scope = properties.Scope.GetValue(source);

                var host = properties.Component.GetComponentHost(source)
                    ?? throw new InvalidOperationException("target host is null");

                if (scope == Scopes.Global)
                    host = host.RootHost;

                var command = host.Services.GetRequiredService(type);

                var commandProperty = target.GetType().GetProperty("Command")
                    ?? throw new InvalidOperationException("target has no property Command");

                commandProperty.SetValue(target, command);
            }

            source.Loaded += InitializeAtLoaded;
        }
    }
}
