using System.Windows;
using System.Windows.Controls;

namespace WPFUtilities.Behaviors
{
    public class MarginSetterBehavior
    {
        public static Thickness GetMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(MarginProperty);
        }

        public static void SetMargin(DependencyObject obj, Thickness value)
        {

            obj.SetValue(MarginProperty, value);
        }

        public static readonly DependencyProperty MarginProperty =
            DependencyProperty.RegisterAttached(
                "Margin", typeof(Thickness),
                typeof(MarginSetterBehavior),
                new UIPropertyMetadata(new Thickness(),
                    Init));

        public static void Init(object sender, DependencyPropertyChangedEventArgs e)
        {
            var panel = sender as Panel;
            if (panel == null) return;
            panel.Loaded += Panel_Loaded;
        }

        private static void Panel_Loaded(object sender, RoutedEventArgs e)
        {
            CreateThicknessForChildrens(sender);
        }

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
