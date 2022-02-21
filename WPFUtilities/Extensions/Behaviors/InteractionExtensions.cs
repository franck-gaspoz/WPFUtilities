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

        /// <summary>
        /// get behavior AssociatedObject private attribute value
        /// </summary>
        /// <param name="behavior">behavior</param>
        /// <returns>object or null</returns>
        public static object GetAssociatedObject(this Behavior behavior)
        {
            var getAssociatedObject = behavior.GetType().GetMethod("get_AssociatedObject", BindingFlags.NonPublic | BindingFlags.Instance);
            var associatedObject = getAssociatedObject.Invoke(behavior, new object[] { });
            return associatedObject;
        }

        /// <summary>
        /// triggers an action when behavior associated object is set
        /// <para>attempt the behavior is attached to its associated object</para>
        /// <para>trigger action immediately if all conditions are reached</para>
        /// </summary>
        /// <param name="behavior">behavior</param>
        /// <param name="action">action to be triggered</param>
        public static void WithAssociatedObject(
            this Behavior behavior,
            Action<object, Behavior> action)
        {
            void BehaviorAssociatedObjectChanged(object sender, EventArgs e)
            {
                if (!(sender is Behavior _behavior))
                    throw new InvalidOperationException($"sender '{sender}' is not of type Behavior");

                var _associatedObject = _behavior.GetAssociatedObject();
                if (_associatedObject != null)
                {
                    behavior.RemoveChangedEventHandler
                        (typeof(InteractionExtensions),
                        nameof(BehaviorAssociatedObjectChanged));
                    action(
                        _associatedObject,
                        behavior
                        );
                }
            }

            var associatedObject = behavior.GetAssociatedObject();
            if (associatedObject == null)
                behavior.AddChangedEventHandler
                    (typeof(InteractionExtensions),
                    nameof(BehaviorAssociatedObjectChanged));
            else
                action(associatedObject, behavior);
        }
    }
}
