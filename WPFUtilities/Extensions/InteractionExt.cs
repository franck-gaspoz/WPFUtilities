using System.Linq;
using System.Windows;

using Microsoft.Xaml.Behaviors;

namespace WPFUtilities.Extensions
{
    internal static class InteractionExt
    {
        public static T GetBehavior<T>(this DependencyObject obj)
            where T : Behavior
        {
            return Interaction.GetBehaviors(obj)
                .OfType<T>()
                .FirstOrDefault();
        }
    }
}
