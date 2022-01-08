using System.Windows;
using System.Windows.Controls;

using Microsoft.Xaml.Behaviors;

namespace WPFUtilities.Behaviors.Layout
{
    /// <summary>
    /// margin setter behavior
    /// </summary>
    public class ChildMarginSetterBehavior : Behavior<Panel>
    {
        /// <summary>
        /// command
        /// </summary>
        public Thickness ChildMargin
        {
            get => (Thickness)GetValue(ChildMarginProperty);
            set { SetValue(ChildMarginProperty, value); }
        }

        /// <summary>
        /// get margin
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns></returns>
        public static Thickness GetChildMargin(DependencyObject dependencyObject) => (Thickness)dependencyObject.GetValue(ChildMarginProperty);

        /// <summary>
        /// get margin
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value"></param>
        public static void SetChildMargin(DependencyObject dependencyObject, Thickness value) => dependencyObject.SetValue(ChildMarginProperty, value);

        /// <summary>
        /// margin property
        /// </summary>
        public static readonly DependencyProperty ChildMarginProperty =
            DependencyProperty.RegisterAttached(
                "ChildMargin", typeof(Thickness),
                typeof(ChildMarginSetterBehavior),
                new UIPropertyMetadata(new Thickness()));

        /// <inheritdoc/>
        protected override void OnAttached() => AssociatedObject.Loaded += Panel_Loaded;

        /// <inheritdoc/>
        protected override void OnDetaching() => AssociatedObject.Loaded -= Panel_Loaded;

        private void Panel_Loaded(object sender, RoutedEventArgs e) => CreateThicknessForChildrens();

        void CreateThicknessForChildrens()
        {
            var childMargin = ChildMargin;
            foreach (var child in AssociatedObject.Children)
            {
                var fe = child as FrameworkElement;
                if (fe == null) continue;
                fe.Margin = childMargin;
            }
        }
    }
}
