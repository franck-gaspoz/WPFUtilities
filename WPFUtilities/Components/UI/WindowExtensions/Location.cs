using System.Windows;

using WPFUtilities.Extensions.DependencyObjects;

using win = System.Windows;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// window location properties
    /// </summary>
    public static partial class Window
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
            DependencyObjectExtensions.Register(
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
            DependencyObjectExtensions.Register(
                "InitialTop",
                typeof(double),
                typeof(win.Window),
                new UIPropertyMetadata(NotInitializedLocation));

        #endregion
    }
}
