using System.Windows;

using win = System.Windows;

namespace WPFUtilities.AttachedProperties.Window
{
    /// <summary>
    /// window location properties
    /// </summary>
    public static class Location
    {
        /// <summary>
        /// initial left,top value when not initialized
        /// </summary>
        public const double NotInitializedLocation = double.MinValue;

        #region initial left

        /// <summary>
        /// get InitialLeft
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static double GetInitialLeft(DependencyObject dependencyObject) => (double)dependencyObject.GetValue(InitialLeftProperty);

        /// <summary>
        /// set InitialLeft
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">value</param>
        public static void SetInitialLeft(DependencyObject dependencyObject, double value) => dependencyObject.SetValue(InitialLeftProperty, value);

        /// <summary>
        /// InitialLeft property
        /// </summary>
        public static readonly DependencyProperty InitialLeftProperty =
            DependencyProperty.Register(
                "InitialLeft",
                typeof(double),
                typeof(win.Window),
                new UIPropertyMetadata(NotInitializedLocation));

        #endregion

        #region initial top

        /// <summary>
        /// get InitialTop
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static double GetInitialTop(DependencyObject dependencyObject) => (double)dependencyObject.GetValue(InitialTopProperty);

        /// <summary>
        /// set InitialTop
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">value</param>
        public static void SetInitialTop(DependencyObject dependencyObject, double value) => dependencyObject.SetValue(InitialTopProperty, value);

        /// <summary>
        /// InitialTop property
        /// </summary>
        public static readonly DependencyProperty InitialTopProperty =
            DependencyProperty.Register(
                "InitialTop",
                typeof(double),
                typeof(win.Window),
                new UIPropertyMetadata(NotInitializedLocation));

        #endregion
    }
}
