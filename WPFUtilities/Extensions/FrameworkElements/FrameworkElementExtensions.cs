using System;
using System.Windows;
using System.Windows.Controls;

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
        /// trigger an action on framework element Unloaded event
        /// </summary>
        /// <typeparam name="T">filter framework element type</typeparam>
        /// <param name="obj">source</param>
        /// <param name="action">action to trigger</param>
        /// <param name="repeat">if true repeat action on next event</param>
        public static void OnUnloaded<T>(
            this T obj,
            Action<RoutedEventArgs> action,
            bool repeat = false)
            where T : FrameworkElement
        {
            if (!obj.IsLoaded)
            {
                action(null);
                return;
            }

            void Unloaded(object sender, RoutedEventArgs routed)
            {
                if (!(sender is T tobj)) return;
                if (!repeat) tobj.Unloaded -= Unloaded;
                action(routed);
            }
            obj.Unloaded += Unloaded;
        }

        /// <summary>
        /// trigger an action on framework element Initialized event
        /// </summary>
        /// <typeparam name="T">filter framework element type</typeparam>
        /// <param name="obj">source</param>
        /// <param name="action">action to trigger</param>
        /// <param name="repeat">if true repeat action on next event</param>
        public static void OnInitialized<T>(
            this T obj,
            Action<EventArgs> action,
            bool repeat = false)
            where T : FrameworkElement
        {
            if (obj.IsInitialized)
            {
                action(null);
                return;
            }

            void Initialized(object sender, EventArgs args)
            {
                if (!(sender is T tobj)) return;
                if (!repeat) tobj.Initialized -= Initialized;
                action(args);
            }
            obj.Initialized += Initialized;
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
            Action<SizeChangedEventArgs> action,
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

        /// <summary>
        /// trigger an action on framework element ContextMenuClosing event
        /// </summary>
        /// <typeparam name="T">filter framework element type</typeparam>
        /// <param name="obj">source</param>
        /// <param name="action">action to trigger</param>
        /// <param name="repeat">if true repeat action on next event</param>
        public static void OnContextMenuClosing<T>(
            this T obj,
            Action<ContextMenuEventArgs> action,
            bool repeat = true)
            where T : FrameworkElement
        {
            void ContextMenuClosing(object sender, ContextMenuEventArgs args)
            {
                if (!(sender is T tobj)) return;
                if (!repeat) tobj.ContextMenuClosing -= ContextMenuClosing;
                action(args);
            }
            obj.ContextMenuClosing += ContextMenuClosing;
        }

        /// <summary>
        /// trigger an action on framework element ContextMenuOpening event
        /// </summary>
        /// <typeparam name="T">filter framework element type</typeparam>
        /// <param name="obj">source</param>
        /// <param name="action">action to trigger</param>
        /// <param name="repeat">if true repeat action on next event</param>
        public static void OnContextMenuOpening<T>(
            this T obj,
            Action<ContextMenuEventArgs> action,
            bool repeat = true)
            where T : FrameworkElement
        {
            void ContextMenuOpening(object sender, ContextMenuEventArgs args)
            {
                if (!(sender is T tobj)) return;
                if (!repeat) tobj.ContextMenuOpening -= ContextMenuOpening;
                action(args);
            }
            obj.ContextMenuOpening += ContextMenuOpening;
        }

        /// <summary>
        /// trigger an action on framework element GotFocus event
        /// </summary>
        /// <typeparam name="T">filter framework element type</typeparam>
        /// <param name="obj">source</param>
        /// <param name="action">action to trigger</param>
        /// <param name="repeat">if true repeat action on next event</param>
        public static void OnGotFocus<T>(
            this T obj,
            Action<RoutedEventArgs> action,
            bool repeat = true)
            where T : FrameworkElement
        {
            void GotFocus(object sender, RoutedEventArgs args)
            {
                if (!(sender is T tobj)) return;
                if (!repeat) tobj.GotFocus -= GotFocus;
                action(args);
            }
            obj.GotFocus += GotFocus;
        }

        /// <summary>
        /// trigger an action on framework element ToolTipClosing event
        /// </summary>
        /// <typeparam name="T">filter framework element type</typeparam>
        /// <param name="obj">source</param>
        /// <param name="action">action to trigger</param>
        /// <param name="repeat">if true repeat action on next event</param>
        public static void OnToolTipClosing<T>(
            this T obj,
            Action<ToolTipEventArgs> action,
            bool repeat = true)
            where T : FrameworkElement
        {
            void ToolTipClosing(object sender, ToolTipEventArgs args)
            {
                if (!(sender is T tobj)) return;
                if (!repeat) tobj.ToolTipClosing -= ToolTipClosing;
                action(args);
            }
            obj.ToolTipClosing += ToolTipClosing;
        }

        /// <summary>
        /// trigger an action on framework element ToolTipOpening event
        /// </summary>
        /// <typeparam name="T">filter framework element type</typeparam>
        /// <param name="obj">source</param>
        /// <param name="action">action to trigger</param>
        /// <param name="repeat">if true repeat action on next event</param>
        public static void OnToolTipOpening<T>(
            this T obj,
            Action<ToolTipEventArgs> action,
            bool repeat = true)
            where T : FrameworkElement
        {
            void ToolTipOpening(object sender, ToolTipEventArgs args)
            {
                if (!(sender is T tobj)) return;
                if (!repeat) tobj.ToolTipClosing -= ToolTipOpening;
                action(args);
            }
            obj.ToolTipOpening += ToolTipOpening;
        }

    }
}
