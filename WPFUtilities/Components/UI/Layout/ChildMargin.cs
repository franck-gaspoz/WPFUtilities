using System.Windows;
using System.Windows.Controls;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// margin setter property behavior
    /// </summary>
    public static partial class Layout
    {
        /// <summary>
        /// get margin
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static Thickness GetChildMargin(DependencyObject dependencyObject) => (Thickness)dependencyObject.GetValue(ChildMarginProperty);

        /// <summary>
        /// set margin
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="ChildMargin">ChildMargin</param>
        public static void SetChildMargin(DependencyObject dependencyObject, Thickness ChildMargin) => dependencyObject.SetValue(ChildMarginProperty, ChildMargin);

        /// <summary>
        /// margin property
        /// </summary>
        public static readonly DependencyProperty ChildMarginProperty =
            DependencyProperty.RegisterAttached(
                "ChildMargin",
                typeof(Thickness),
                typeof(Layout),
                new UIPropertyMetadata(new Thickness(), ChildMarginChanged));

        static void ChildMarginChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (!(dependencyObject is Panel panel)) return;

            if (!panel.IsLoaded)
                panel.Loaded += Panel_Loaded_SetChildrensMargin;
            else
                SetChildrensMargin(panel);
        }

        private static void Panel_Loaded_SetChildrensMargin(object sender, RoutedEventArgs e)
        {
            if (!(sender is Panel panel)) return;

            panel.Loaded -= Panel_Loaded_SetChildrensMargin;
            SetChildrensMargin(panel);
        }

        static void SetChildrensMargin(Panel panel)
        {
            var childMargin = (Thickness)panel.GetValue(ChildMarginProperty);
            foreach (var child in panel.Children)
            {
                var fe = child as FrameworkElement;
                if (fe == null) continue;
                fe.Margin = childMargin;
            }
        }
    }
}
