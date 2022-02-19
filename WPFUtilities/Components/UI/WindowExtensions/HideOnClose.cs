using System.ComponentModel;
using System.Windows;

using win = System.Windows;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// hide on close
    /// </summary>
    public static partial class Window
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
            DependencyProperty.RegisterAttached(
                "HideOnClose",
                typeof(bool),
                typeof(win.Window),
                new UIPropertyMetadata(false, HideOnCloseChanged));

        #endregion

        static void HideOnCloseChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (dependencyObject is win.Window window)
            {
                if ((bool)eventArgs.NewValue)
                    window.Closing += OnWindowClosing;
                else
                    window.Closing -= OnWindowClosing;
            }
        }

        private static void OnWindowClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            (sender as win.Window)?.Hide();
        }
    }
}
