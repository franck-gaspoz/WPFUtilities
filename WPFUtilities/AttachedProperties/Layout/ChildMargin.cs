using System.Windows;
using System.Windows.Controls;

namespace WPFUtilities.AttachedProperties.Layout
{
    /// <summary>
    /// margin setter property behavior
    /// </summary>
    public static class ChildMargin
    {
        /// <summary>
        /// get margin
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static Thickness GetValue(DependencyObject dependencyObject) => (Thickness)dependencyObject.GetValue(ValueProperty);

        /// <summary>
        /// set margin
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">value</param>
        public static void SetValue(DependencyObject dependencyObject, Thickness value) => dependencyObject.SetValue(ValueProperty, value);

        /// <summary>
        /// margin property
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.RegisterAttached(
                "Value",
                typeof(Thickness),
                typeof(ChildMargin),
                new UIPropertyMetadata(new Thickness(), ChildMarginChanged));

        static void ChildMarginChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (dependencyObject is Panel panel)
                panel.Loaded += (o, e) =>
                    CreateThicknessForChildrens(panel);

        }

        static void CreateThicknessForChildrens(Panel panel)
        {
            var childMargin = (Thickness)panel.GetValue(ValueProperty);
            foreach (var child in panel.Children)
            {
                var fe = child as FrameworkElement;
                if (fe == null) continue;
                fe.Margin = childMargin;
            }
        }
    }
}
