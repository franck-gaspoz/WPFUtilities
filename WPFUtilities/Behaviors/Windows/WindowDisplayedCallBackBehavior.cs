using System.Windows;


namespace WPFUtilities.Behaviors.Windows
{
    public class WindowDisplayedCallBackBehavior
    {
        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEnabledProperty, value);
        }

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled",
                typeof(bool),
                typeof(WindowDisplayedCallBackBehavior),
                new PropertyMetadata(false, IsEnabledChanged));

        static void IsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is Window target)) return;

            if ((bool)e.NewValue)
            {
                target.SizeChanged += Target_SizeChanged;
            }
            else
            {
                target.SizeChanged -= Target_SizeChanged;
            }
        }

        static void Target_SizeChanged(
            object sender,
            SizeChangedEventArgs e)
        {
            if ((sender is Window w) &&
                e.NewSize.Width != double.NaN
                && e.NewSize.Height != double.NaN
                && e.NewSize.Width != 0
                && e.NewSize.Height != 0)
            {
                w.SizeChanged -= Target_SizeChanged;
                // TODO: couplage fort à changer
                (Application.Current as IApp)
                    ?.NotifyWindowDisplayed(sender);
            }
        }
    }
}
