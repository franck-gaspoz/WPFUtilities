using System;
using System.Windows;

namespace WPFUtilities.Extensions.FrameworkElements
{
    /// <summary>
    /// framework elements extensions
    /// </summary>
    public static class FrameworkElementExtensions
    {
        /// <summary>
        /// trigger an action on framework element Loaded event
        /// </summary>
        /// <typeparam name="T">filter framework element type</typeparam>
        /// <param name="obj">source</param>
        /// <param name="action">action to trigger</param>
        /// <param name="repeat">if true repeat action on next event</param>
        public static void OnLoaded<T>(
            this T obj,
            Action<RoutedEventArgs> action,
            bool repeat = false)
            where T : FrameworkElement
        {
            if (obj.IsLoaded)
            {
                action(null);
                return;
            }

            void Loaded(object sender, RoutedEventArgs routed)
            {
                if (!(sender is T tobj)) return;
                if (!repeat) tobj.Loaded -= Loaded;
                action(routed);
            }
            obj.Loaded += Loaded;
        }

        /// <summary>
        /// trigger an action on framework element Sized event
        /// </summary>
        /// <typeparam name="T">filter framework element type</typeparam>
        /// <param name="obj">source</param>
        /// <param name="action">action to trigger</param>
        /// <param name="repeat">if true repeat action on next event</param>
        public static void OnSizeChanged<T>(
            this T obj,
            Action<RoutedEventArgs> action,
            bool repeat = true)
            where T : FrameworkElement
        {
            void Sized(object sender, SizeChangedEventArgs sizeChanged)
            {
                if (!(sender is T tobj)) return;
                if (!repeat) tobj.SizeChanged -= Sized;
                action(sizeChanged);
            }
            obj.SizeChanged += Sized;
        }
    }
}
