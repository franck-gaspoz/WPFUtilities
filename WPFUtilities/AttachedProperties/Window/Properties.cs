using System.Windows;

using win = System.Windows;

namespace WPFUtilities.AttachedProperties.Window
{
    /// <summary>
    /// hide on close a window
    /// </summary>
    public static class Properties
    {
        #region hide on close

        /// <summary>
        /// get HideOnClose
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static bool GetHideOnClose(DependencyObject dependencyObject) => (bool)dependencyObject.GetValue(ValueProperty);

        /// <summary>
        /// get margin
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">value</param>
        public static void SetHideOnClose(DependencyObject dependencyObject, bool value) => dependencyObject.SetValue(ValueProperty, value);

        /// <summary>
        /// margin property
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.RegisterAttached(
                "HideOnClose",
                typeof(bool),
                typeof(Properties),
                new UIPropertyMetadata(false, HideOnCloseChanged));

        static void HideOnCloseChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (dependencyObject is win.Window window)
            {
                window.Closing += (o, e) =>
                {
                    e.Cancel = true;
                    ((win.Window)o).Hide();
                };
            }
        }

        #endregion
    }
}
