using System;
using System.ComponentModel;
using System.Windows;

using Microsoft.Xaml.Behaviors;

using WPFUtilities.Components.Services.Command;
using WPFUtilities.Extensions.Behaviors;
using WPFUtilities.Extensions.Reflections;
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
    public static partial class Command
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
            if (DesignerProperties.GetIsInDesignMode(dependencyObject)) return;

            if (!(eventArgs.NewValue is Type type)) return;
            if (dependencyObject is FrameworkElement frameworkElement)
                SetupFrameworkElementCommandProperty(frameworkElement, frameworkElement, type);
            else
            {
                if (dependencyObject is Behavior behavior)
                    ResolveBehaviorCommand(behavior, type);
                else
                    throw new InvalidOperationException($"can't setup Command property on element '{dependencyObject}' of Type '{dependencyObject.GetType().Name}'");
            }
        }

        /// <summary>
        /// work on command property of behavior associated object
        /// </summary>
        /// <param name="behavior">associated object</param>
        /// <param name="type">command type</param>
        static void ResolveBehaviorCommand(Behavior behavior, Type type)
            => behavior.WithAssociatedObject(
                ResolveBehaviorElementCommandProperty);

        static void ResolveBehaviorElementCommandProperty(
            object associatedObject, Behavior behavior)
        {
            if (!(associatedObject is FrameworkElement frameworkElement))
                throw new Exception($"associated object '{associatedObject}' is not of type FrameworkElement");

            Type type = GetType(behavior);
            SetupFrameworkElementCommandProperty(frameworkElement, behavior, type);
        }

        /// <summary>
        /// setup the framework element property 'Command'
        /// </summary>
        /// <param name="source">framework element providing the component host</param>
        /// <param name="target">object target owning the property 'Command'</param>
        /// <param name="commandType">value of the 'Command' property</param>
        static void SetupFrameworkElementCommandProperty(
            FrameworkElement source,
            DependencyObject target,
            Type commandType)
        {
            if (DesignerProperties.GetIsInDesignMode(source)) return;
            if (DesignerProperties.GetIsInDesignMode(target)) return;

            if (commandType.HasInterface<IServiceCommand>())
                ServiceCommandPropertiesHelper.AssignRelayCommandToProperty(
                    source, target, commandType);
            else
                source.AssignServiceToProperty(target, commandType, "Command");
        }
    }
}
