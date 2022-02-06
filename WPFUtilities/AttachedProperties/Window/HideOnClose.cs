using System.Windows;

using win = System.Windows;

namespace WPFUtilities.AttachedProperties.Window
{
    /// <summary>
    /// hide on close
    /// </summary>
    public static partial class Features
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

        #endregion

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
    }
}
