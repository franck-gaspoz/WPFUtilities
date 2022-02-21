using System;
using System.ComponentModel;
using System.Windows;

using Microsoft.Xaml.Behaviors;

using WPFUtilities.Extensions.Behaviors;
using WPFUtilities.Extensions.Services;

namespace WPFUtilities.Components.Services.Properties
{
    /// <summary>
    /// resolve a command from di scope
    /// <para>use application host if Scope value property is not defined or set to Global</para>
    /// <para>else:</para>
    /// <para>requires the Component.Host property to be set in the dependency object</para>
    /// <para>or require the property Component.Type to be setted in the dependency object</para>
    /// <para>and performs a lookup of the component host in parents logical tree, then sets the component host property</para>
    /// <para>Type is accepted on types FrameworkElement and Behavior</para>
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
                SetupFrameworkElementCommandPropertyFromCommandType(frameworkElement, frameworkElement, type);
            else
            {
                if (dependencyObject is Behavior behavior)
                    SetupBehaviorFromCommandType(behavior, type);
                else
                    throw new InvalidOperationException($"can't setup Command property on element '{dependencyObject}' of Type '{dependencyObject.GetType().Name}'");
            }
        }

        /// <summary>
        /// work on command property of behavior associated object
        /// </summary>
        /// <param name="behavior">associated object</param>
        /// <param name="type">command type</param>
        static void SetupBehaviorFromCommandType(Behavior behavior, Type type)
            => behavior.WithAssociatedObjectPropertyChanged(
                SetupBehaviorElementCommandPropertyFromCommandType);

        static void SetupBehaviorElementCommandPropertyFromCommandType(
            object associatedObject, Behavior behavior)
        {
            if (!(associatedObject is FrameworkElement frameworkElement))
                throw new Exception($"associated object '{associatedObject}' is not of type FrameworkElement");

            Type type = GetType(behavior);
            SetupFrameworkElementCommandPropertyFromCommandType(frameworkElement, behavior, type);
        }

        static void SetupFrameworkElementCommandPropertyFromCommandType(
            FrameworkElement source,
            DependencyObject target,
            Type commandType)
            => source.AssignServiceToProperty(target, commandType, "Command");
    }
}
