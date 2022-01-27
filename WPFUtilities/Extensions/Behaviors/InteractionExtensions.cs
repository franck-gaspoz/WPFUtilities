using System.Linq;
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
    }
}
