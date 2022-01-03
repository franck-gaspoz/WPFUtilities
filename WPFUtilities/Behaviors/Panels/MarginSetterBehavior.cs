using System.Windows;
using System.Windows.Controls;

namespace WPFUtilities.Behaviors
{
    /// <summary>
    /// margin setter behavior
    /// </summary>
    public class MarginSetterBehavior
    {
        /// <summary>
        /// get margin
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns></returns>
        public static Thickness GetMargin(DependencyObject dependencyObject) => (Thickness)dependencyObject.GetValue(MarginProperty);

        /// <summary>
        /// get margin
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value"></param>
        public static void SetMargin(DependencyObject dependencyObject, Thickness value) => dependencyObject.SetValue(MarginProperty, value);

        /// <summary>
        /// margin property
        /// </summary>
        public static readonly DependencyProperty MarginProperty =
            DependencyProperty.RegisterAttached(
                "Margin", typeof(Thickness),
                typeof(MarginSetterBehavior),
                new UIPropertyMetadata(new Thickness(),
                    Init));

        static void Init(object sender, DependencyPropertyChangedEventArgs e)
        {
            var panel = sender as Panel;
            if (panel == null) return;
            panel.Loaded += Panel_Loaded;
        }

        private static void Panel_Loaded(object sender, RoutedEventArgs e) => CreateThicknessForChildrens(sender);

        static void CreateThicknessForChildrens(
            object sender)
        {
            if (!(sender is Panel panel)) return;

            foreach (var child in panel.Children)
            {
                var fe = child as FrameworkElement;

                if (fe == null) continue;

                fe.Margin = MarginSetterBehavior.GetMargin(panel);
            }
        }


    }
}
