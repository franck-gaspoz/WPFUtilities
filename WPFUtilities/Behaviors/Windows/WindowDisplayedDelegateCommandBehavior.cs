using System.Windows;
using System.Windows.Input;

using Microsoft.Xaml.Behaviors;

namespace WPFUtilities.Behaviors.Windows
{
    /// <summary>
    /// callback app viewmodel when window is totally displayed (visible on screen)
    /// </summary>
    public class WindowDisplayedDelegateCommandBehavior
        : Behavior<Window>
    {
        #region IsEnabled

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
                typeof(WindowDisplayedDelegateCommandBehavior),
                new PropertyMetadata(true, IsEnabledChanged));

        /// <inheritdoc/>
        protected override void OnAttached()
            => AssociatedObject.SizeChanged += Target_SizeChanged;

        /// <inheritdoc/>
        protected override void OnDetaching()
            => AssociatedObject.SizeChanged -= Target_SizeChanged;

        /// <summary>
        /// IsEnabled changed handler
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="eventArgs">event args</param>
        static void IsEnabledChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (!(dependencyObject is Window target)) return;
            var isEnabled = GetIsEnabled(dependencyObject);

            if ((bool)eventArgs.NewValue && !isEnabled)
            {
                target.SizeChanged += Target_SizeChanged;
            }
            else
            {
                if (isEnabled)
                    target.SizeChanged -= Target_SizeChanged;
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// command
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// get command
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>comamnd</returns>
        public static ICommand GetCommand(DependencyObject dependencyObject)
            => (ICommand)dependencyObject.GetValue(CommandProperty);

        /// <summary>
        /// set command
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <param name="value">value</param>
        public static void SetCommand(DependencyObject dependencyObject, ICommand value)
            => dependencyObject.SetValue(CommandProperty, value);

        /// <summary>
        /// command dependency property
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command",
                typeof(ICommand),
                typeof(WindowDisplayedDelegateCommandBehavior),
                new PropertyMetadata(null));

        #endregion

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

                GetCommand(w)?.Execute(w);
            }
        }
    }
}
