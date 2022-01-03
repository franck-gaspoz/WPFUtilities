using System.Windows;

using WPFUtilities.Extensions.Appl;

namespace WPFUtilities.Behaviors.Windows
{
    /// <summary>
    /// callback app viewmodel when window is totally displayed (visible on screen)
    /// </summary>
    public class WindowDisplayedCallBackBehavior
    {
        /// <summary>
        /// get IsEnabled dependency property value for object
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>true if is enabled</returns>
        public static bool GetIsEnabled(DependencyObject dependencyObject)
            => (bool)dependencyObject.GetValue(IsEnabledProperty);

        /// <summary>
        /// set IsEnabled dependency property value for object
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="value">value</param>
        public static void SetIsEnabled(DependencyObject dependencyObject, bool value)
            => dependencyObject.SetValue(IsEnabledProperty, value);

        /// <summary>
        /// IsEnabled dependency property
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled",
                typeof(bool),
                typeof(WindowDisplayedCallBackBehavior),
                new PropertyMetadata(false, IsEnabledChanged));

        /// <summary>
        /// IsEnabled changed handler
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="eventArgs">event args</param>
        static void IsEnabledChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (!(dependencyObject is Window target)) return;

            if ((bool)eventArgs.NewValue)
            {
                target.SizeChanged += Target_SizeChanged;
            }
            else
            {
                target.SizeChanged -= Target_SizeChanged;
            }
        }

        /// <summary>
        /// window size changed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="eventArgs">event args</param>
        static void Target_SizeChanged(
            object sender,
            SizeChangedEventArgs eventArgs)
        {
            if ((sender is Window w) &&
                eventArgs.NewSize.Width != double.NaN
                && eventArgs.NewSize.Height != double.NaN
                && eventArgs.NewSize.Width != 0
                && eventArgs.NewSize.Height != 0)
            {
                w.SizeChanged -= Target_SizeChanged;

                Application.Current.ViewModel()?.NotifyMainWindowDisplayed(sender);
            }
        }
    }
}
