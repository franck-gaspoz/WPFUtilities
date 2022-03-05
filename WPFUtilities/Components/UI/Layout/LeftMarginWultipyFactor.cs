using System.Windows;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// set child spacing
    /// </summary>
    public static partial class Layout
    {
        #region left margin multiply factor property

        /// <summary>
        /// get left margin multiply factor
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static double GetLeftMarginMultiplyFactor(DependencyObject dependencyObject) => (double)dependencyObject.GetValue(LeftMarginMultiplyFactorProperty);

        /// <summary>
        /// set left margin multiply factor
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="LeftMarginMultiplyFactor">LeftMarginMultiplyFactor</param>
        public static void SetLeftMarginMultiplyFactor(DependencyObject dependencyObject, double LeftMarginMultiplyFactor) => dependencyObject.SetValue(LeftMarginMultiplyFactorProperty, LeftMarginMultiplyFactor);

        /// <summary>
        /// left margin multiply factor property
        /// </summary>
        public static readonly DependencyProperty LeftMarginMultiplyFactorProperty =
            DependencyProperty.RegisterAttached(
                "LeftMarginMultiplyFactor",
                typeof(double),
                typeof(Layout),
                new UIPropertyMetadata(0d, LeftMarginMultiplyFromChanged));

        #endregion
    }
}
