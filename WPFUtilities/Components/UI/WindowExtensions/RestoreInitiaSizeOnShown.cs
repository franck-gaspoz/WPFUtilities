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
        /// get restore initial size on show
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static bool GetRestoreInitialSizeOnShow(DependencyObject dependencyObject) => (bool)dependencyObject.GetValue(RestoreInitialSizeProperty);

        /// <summary>
        /// set restore initial size on show
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">value</param>
        public static void SetRestoreInitialSizeOnShow(DependencyObject dependencyObject, bool value) => dependencyObject.SetValue(RestoreInitialSizeProperty, value);

        /// <summary>
        /// restore initial size on show property
        /// </summary>
        public static readonly DependencyProperty RestoreInitialSizeProperty =
            DependencyProperty.Register(
                "RestoreInitialSizeOnShow",
                typeof(bool),
                typeof(win.Window),
                new UIPropertyMetadata(false, RestoreInitialSizeOnShowChanged));

        static void RestoreInitialSizeOnShowChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (!(dependencyObject is win.Window window)) return;

            if ((bool)eventArgs.NewValue)
                window.IsVisibleChanged += Window_IsVisibleChanged_RestoreInitialSize;
            else
                window.IsVisibleChanged -= Window_IsVisibleChanged_RestoreInitialSize;
        }

        #endregion

        private static void Window_IsVisibleChanged_RestoreInitialSize(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue && sender is win.Window win)
            {
                var initialWidth = Window.GetInitialWidth(win);
                if (initialWidth == Window.NotInitializedSize)
                    Window.SetInitialWidth(win, win.Width);
                else
                    win.Width = initialWidth;

                var initialHeight = Window.GetInitialHeight(win);
                if (initialHeight == Window.NotInitializedSize)
                    Window.SetInitialHeight(win, win.Height);
                else
                    win.Height = initialHeight;
            }
        }
    }
}
