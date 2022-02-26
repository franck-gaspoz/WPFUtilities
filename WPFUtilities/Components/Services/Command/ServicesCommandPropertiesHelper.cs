using System;
using System.Windows;

using WPFUtilities.Components.ServiceComponent;
using WPFUtilities.Components.Services.Command;
using WPFUtilities.Extensions.Reflections;

namespace WPFUtilities.Components.Services.Properties
{
    /// <summary>
    /// framework element extensions methods that help resolve service commands framework element initialization
    /// </summary>
    internal static class ServiceCommandPropertiesHelper
    {
        /// <summary>
        /// set the 'Command' property on target object, resolved from services collections of source component host, when source is loaded
        /// <para>if command is IServiceCommand, wrap the command with a RelayServiceCommand</para>
        /// </summary>
        /// <param name="source">framework element source that fires the setup onces loaded and provides the component host</param>
        /// <param name="target">the target object having the porperty 'Command' that must be setted. The target provides the scope property</param>
        /// <param name="commandType">service type to be resolved to an instance from the source component host services collection</param>
        /// <exception cref="InvalidOperationException">target has no property Command</exception>
        internal static void AssignRelayCommandToProperty(
            FrameworkElement source,
            DependencyObject target,
            Type commandType)
        {
            if (!commandType.HasInterface<IServiceCommand>())
                throw new InvalidOperationException($"command type ({commandType.FullName}) is required to have interface IServiceCommand");

            ComponentHostLookup.SetComponentHostPropertyFromResolvedComponentWhenLoaded(source);

            void InitializeAtLoaded(object src, EventArgs e)
            {
                source.Loaded -= InitializeAtLoaded;

                ServicesPropertiesHelper.InitializeSourceServiceAndPerformAction(
                    source,
                    scopeOwner: target,
                    commandType,
                    (service) =>
                    {
                        var targetProperty = target.GetType().GetProperty("Command")
                            ?? throw new InvalidOperationException($"target '{target}' has no property Command");

                        var context = new ServiceCommandExecuteContext
                        {
                            Caller = source
                        };
                        var relayCommand = new RelayServiceCommand(
                            service as IServiceCommand,
                            context);
                        targetProperty.SetValue(target, relayCommand);
                    }); ;
            }

            if (!source.IsLoaded)
                source.Loaded += InitializeAtLoaded;
            else
                InitializeAtLoaded(source, null);
        }

    }
}
