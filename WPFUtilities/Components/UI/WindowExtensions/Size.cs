using System.Windows;

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
        public const double NotInitializedSize = double.MinValue;

        #region initial width

        /// <summary>
        /// get Initial width
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static double GetInitialWidth(DependencyObject dependencyObject) => (double)dependencyObject.GetValue(InitialWidthProperty);

        /// <summary>
        /// set Initial width
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">value</param>
        public static void SetInitialWidth(DependencyObject dependencyObject, double value) => dependencyObject.SetValue(InitialWidthProperty, value);

        /// <summary>
        /// Initial width property
        /// </summary>
        public static readonly DependencyProperty InitialWidthProperty =
            DependencyProperty.Register(
                "InitialWidth",
                typeof(double),
                typeof(win.Window),
                new UIPropertyMetadata(NotInitializedLocation));

        #endregion

        #region initial height

        /// <summary>
        /// get Initial height
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static double GetInitialHeight(DependencyObject dependencyObject) => (double)dependencyObject.GetValue(InitialHeightProperty);

        /// <summary>
        /// set Initial height
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">value</param>
        public static void SetInitialHeight(DependencyObject dependencyObject, double value) => dependencyObject.SetValue(InitialHeightProperty, value);

        /// <summary>
        /// Initial height property
        /// </summary>
        public static readonly DependencyProperty InitialHeightProperty =
            DependencyProperty.Register(
                "InitialHeight",
                typeof(double),
                typeof(win.Window),
                new UIPropertyMetadata(NotInitializedLocation));

        #endregion
    }
}
