using System.ComponentModel;
using System.Windows;

using WPFUtilities.Extensions.DependencyObjects;
using WPFUtilities.Extensions.FrameworkElements;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// set child spacing
    /// </summary>
    public static partial class Layout
    {
        #region left margin multiply from property

        /// <summary>
        /// get left margin multiply from
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static double GetLeftMarginMultiplyFrom(DependencyObject dependencyObject) => (double)dependencyObject.GetValue(LeftMarginMultiplyFromProperty);

        /// <summary>
        /// set left margin multiply from
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="LeftMarginMultiplyFrom">LeftMarginMultiplyFrom</param>
        public static void SetLeftMarginMultiplyFrom(DependencyObject dependencyObject, double LeftMarginMultiplyFrom) => dependencyObject.SetValue(LeftMarginMultiplyFromProperty, LeftMarginMultiplyFrom);

        /// <summary>
        /// left margin multiply from property
        /// </summary>
        public static readonly DependencyProperty LeftMarginMultiplyFromProperty =
            DependencyProperty.RegisterAttached(
                "LeftMarginMultiplyFrom",
                typeof(double),
                typeof(Layout),
                new UIPropertyMetadata(0d, LeftMarginMultiplyFromChanged));

        #endregion

        static void LeftMarginMultiplyFromChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (DesignerProperties.GetIsInDesignMode(dependencyObject)) return;
            if (!(dependencyObject is FrameworkElement elem)) return;

            elem.OnLoaded((routed) =>
            {
                var margin = new Thickness(
                    elem.GetValue<double>(LeftMarginMultiplyFromProperty)
                        * elem.GetValue<double>(LeftMarginMultiplyFactorProperty),
                    elem.Margin.Top,
                    elem.Margin.Right,
                    elem.Margin.Bottom
                    );
                elem.Margin = margin;
            });
        }
    }
}
