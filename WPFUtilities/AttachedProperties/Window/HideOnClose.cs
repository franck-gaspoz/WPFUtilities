using System.ComponentModel;
using System.Windows;

using win = System.Windows;

namespace WPFUtilities.AttachedProperties.Window
{
    /// <summary>
    /// hide on close a window
    /// </summary>
    public static class HideOnClose
    {
        /// <summary>
        /// get IsEnabled
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static bool GetIsEnabled(DependencyObject dependencyObject) => (bool)dependencyObject.GetValue(ValueProperty);

        /// <summary>
        /// get margin
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">value</param>
        public static void SetIsEnabled(DependencyObject dependencyObject, bool value) => dependencyObject.SetValue(ValueProperty, value);

        /// <summary>
        /// margin property
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "IsEnabled",
                typeof(bool),
                typeof(win.Window),
                new UIPropertyMetadata(new bool(), IsEnabledChanged));

        static void IsEnabledChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (dependencyObject is win.Window window)
            {
                window.Closing += Window_Closing;
            }
        }

        private static void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            ((win.Window)sender).Hide();
        }
    }
}
