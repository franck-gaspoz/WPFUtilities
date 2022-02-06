using System.Windows;

using win = System.Windows;

namespace WPFUtilities.AttachedProperties.Window
{
    /// <summary>
    /// restore initial location on show
    /// </summary>
    public static partial class Features
    {
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
                window.IsVisibleChanged += RestoreInitialLocation_Window_IsVisibleChanged;
            }
        }

        #endregion

        private static void RestoreInitialLocation_Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
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
    }
}
