using System.Windows;

using win = System.Windows;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// restore initial location on show
    /// </summary>
    public static partial class Window
    {
        #region restore initial location on show

        /// <summary>
        /// get restore initial location on show
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static bool GetRestoreInitialLocationOnShow(DependencyObject dependencyObject) => (bool)dependencyObject.GetValue(RestoreInitialLocationProperty);

        /// <summary>
        /// set restore initial location on show
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">value</param>
        public static void SetRestoreInitialLocationOnShow(DependencyObject dependencyObject, bool value) => dependencyObject.SetValue(RestoreInitialLocationProperty, value);

        /// <summary>
        /// restore initial location on show property
        /// </summary>
        public static readonly DependencyProperty RestoreInitialLocationProperty =
            DependencyProperty.Register(
                "RestoreInitialLocationOnShow",
                typeof(bool),
                typeof(win.Window),
                new UIPropertyMetadata(false, RestoreInitialLocationOnShowChanged));

        static void RestoreInitialLocationOnShowChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (!(dependencyObject is win.Window window)) return;

            if ((bool)eventArgs.NewValue)
                window.IsVisibleChanged += Window_IsVisibleChanged_RestoreInitialLocation;
            else
                window.IsVisibleChanged -= Window_IsVisibleChanged_RestoreInitialLocation;
        }

        #endregion

        private static void Window_IsVisibleChanged_RestoreInitialLocation(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue && sender is win.Window win)
            {
                var initialLeft = Window.GetInitialLeft(win);
                if (initialLeft == Window.NotInitializedLocation)
                    Window.SetInitialLeft(win, win.Left);
                else
                    win.Left = initialLeft;

                var initialTop = Window.GetInitialTop(win);
                if (initialTop == Window.NotInitializedLocation)
                    Window.SetInitialTop(win, win.Top);
                else
                    win.Top = initialTop;
            }
        }
    }
}
