using System.Windows;

using win = System.Windows;

namespace WPFUtilities.AttachedProperties.Window
{
    /// <summary>
    /// window behaviors defined as properties
    /// </summary>
    public static class Behaviors
    {
        #region hide on close

        /// <summary>
        /// get HideOnClose
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static bool GetHideOnClose(DependencyObject dependencyObject) => (bool)dependencyObject.GetValue(HideOnCloseProperty);

        /// <summary>
        /// set HideOnClose
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">value</param>
        public static void SetHideOnClose(DependencyObject dependencyObject, bool value) => dependencyObject.SetValue(HideOnCloseProperty, value);

        /// <summary>
        /// HideOnClose property
        /// </summary>
        public static readonly DependencyProperty HideOnCloseProperty =
            DependencyProperty.Register(
                "HideOnClose",
                typeof(bool),
                typeof(win.Window),
                new UIPropertyMetadata(false, HideOnCloseChanged));

        static void HideOnCloseChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if ((bool)eventArgs.NewValue && dependencyObject is win.Window window)
            {
                window.Closing += (o, e) =>
                {
                    e.Cancel = true;
                    ((win.Window)o).Hide();
                };
            }
        }

        #endregion

        #region restore initial location on show

        /// <summary>
        /// get RestoreInitialLocationOnShow
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static bool GetRestoreInitialLocationOnShow(DependencyObject dependencyObject) => (bool)dependencyObject.GetValue(RestoreInitialLocationProperty);

        /// <summary>
        /// get margin
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">value</param>
        public static void SetRestoreInitialLocationOnShow(DependencyObject dependencyObject, bool value) => dependencyObject.SetValue(RestoreInitialLocationProperty, value);

        /// <summary>
        /// margin property
        /// </summary>
        public static readonly DependencyProperty RestoreInitialLocationProperty =
            DependencyProperty.Register(
                "RestoreInitialLocationOnShow",
                typeof(bool),
                typeof(win.Window),
                new UIPropertyMetadata(false, RestoreInitialLocationOnShowChanged));

        static void RestoreInitialLocationOnShowChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if ((bool)eventArgs.NewValue && dependencyObject is win.Window window)
            {
                window.IsVisibleChanged += Window_IsVisibleChanged;
            }
        }

        private static void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue && sender is win.Window win)
            {
                var initialLeft = Location.GetInitialLeft(win);
                if (initialLeft == Location.NotInitializedLocation)
                    Location.SetInitialLeft(win, win.Left);
                else
                    win.Left = initialLeft;

                var initialTop = Location.GetInitialTop(win);
                if (initialTop == Location.NotInitializedLocation)
                    Location.SetInitialTop(win, win.Top);
                else
                    win.Top = initialTop;
            }
        }

        #endregion
    }
}
