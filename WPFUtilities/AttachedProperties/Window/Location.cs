using System.Windows;

using win = System.Windows;

namespace WPFUtilities.AttachedProperties.Window
{
    /// <summary>
    /// window location properties
    /// </summary>
    public static class Location
    {
        #region initial left

        /// <summary>
        /// get InitialLeft
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static bool GetInitialLeft(DependencyObject dependencyObject) => (bool)dependencyObject.GetValue(InitialLeftProperty);

        /// <summary>
        /// set InitialLeft
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">value</param>
        public static void SetInitialLeft(DependencyObject dependencyObject, bool value) => dependencyObject.SetValue(InitialLeftProperty, value);

        /// <summary>
        /// InitialLeft property
        /// </summary>
        public static readonly DependencyProperty InitialLeftProperty =
            DependencyProperty.Register(
                "InitialLeft",
                typeof(double),
                typeof(win.Window),
                new UIPropertyMetadata(0));

        #endregion

        #region initial right

        /// <summary>
        /// get InitialRight
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static bool GetInitialRight(DependencyObject dependencyObject) => (bool)dependencyObject.GetValue(InitialRightProperty);

        /// <summary>
        /// set InitialRight
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">value</param>
        public static void SetInitialRight(DependencyObject dependencyObject, bool value) => dependencyObject.SetValue(InitialRightProperty, value);

        /// <summary>
        /// InitialRight property
        /// </summary>
        public static readonly DependencyProperty InitialRightProperty =
            DependencyProperty.Register(
                "InitialRight",
                typeof(double),
                typeof(win.Window),
                new UIPropertyMetadata(0));

        #endregion
    }
}
