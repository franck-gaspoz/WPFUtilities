using System;
using System.Linq;
using System.Reflection;
using System.Windows;

using Microsoft.Xaml.Behaviors;

namespace WPFUtilities.Extensions.Behaviors
{
    /// <summary>
    /// interaction extensions
    /// </summary>
    public static class InteractionExtensions
    {
        /// <summary>
        /// get a behavior of type T from an object
        /// </summary>
        /// <typeparam name="T">behavior type</typeparam>
        /// <param name="obj">object</param>
        /// <returns></returns>
        public static T GetBehavior<T>(this DependencyObject obj)
            where T : Behavior
            => Interaction.GetBehaviors(obj)
                .OfType<T>()
                .FirstOrDefault();

        /// <summary>
        /// add method handler to behavior for event Changed
        /// </summary>
        /// <param name="behavior">behavior</param>
        /// <param name="handlerTypeOwner">type that owns the handler method</param>
        /// <param name="staticHandlerMethodName">name of the static method handler in handler type</param>
        public static void AddChangedEventHandler(
            this Behavior behavior,
            Type handlerTypeOwner,
            string staticHandlerMethodName
            )
        {
            var changedEventInfo = behavior.GetType().GetEvent("Changed");
            var changedEventHandler = handlerTypeOwner
                .GetMethod(staticHandlerMethodName,
                    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            var handler = Delegate.CreateDelegate(changedEventInfo.EventHandlerType, changedEventHandler);
            changedEventInfo.AddEventHandler(behavior, handler);
        }

        /// <summary>
        /// remove changed event handler
        /// </summary>
        /// <param name="behavior">behavior</param>
        /// <param name="handlerTypeOwner">type that owns the handler method</param>
        /// <param name="staticHandlerMethodName">name of the static method handler in handler type</param>
        public static void RemoveChangedEventHandler(
            this Behavior behavior,
            Type handlerTypeOwner,
            string staticHandlerMethodName
            )
        {
            var changedEventInfo = behavior.GetType().GetEvent("Changed");
            var changedEventHandler = handlerTypeOwner
                .GetMethod(staticHandlerMethodName,
                    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            var handler = Delegate.CreateDelegate(changedEventInfo.EventHandlerType, changedEventHandler);
            changedEventInfo.RemoveEventHandler(behavior, handler);
        }
    }
}
