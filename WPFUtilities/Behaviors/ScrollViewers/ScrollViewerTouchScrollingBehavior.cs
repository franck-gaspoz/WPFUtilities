using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Microsoft.Xaml.Behaviors;

namespace WPFUtilities.Behaviors.ScrollViewers
{
    /// <summary>
    /// scroll viewer touch scrolling behavior : enable scrolling with mouse + left click and drag
    /// </summary>
    public class ScrollViewerTouchScrollingBehavior : Behavior<ScrollViewer>
    {
        /// <summary>
        /// enabling trigger treshold
        /// </summary>
        static readonly double EnableTriggerTreshold = 5;

        /// <summary>
        /// get is enabled
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <returns>is enabled</returns>
        public static bool GetIsEnabled(DependencyObject dependencyObject) => (bool)dependencyObject.GetValue(IsEnabledProperty);

        /// <summary>
        /// set is enabled
        /// </summary>
        /// <param name="dependencyObject">dependencyObject</param>
        /// <param name="value">value</param>
        public static void SetIsEnabled(DependencyObject dependencyObject, bool value) => dependencyObject.SetValue(IsEnabledProperty, value);

        /// <summary>
        /// is enabled
        /// </summary>
        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        /// <summary>
        /// is enabled dependency property
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled",
                typeof(bool),
                typeof(ScrollViewerTouchScrollingBehavior),
                new UIPropertyMetadata(true, IsEnabledChanged));

        /// <summary>
        /// mouses captures
        /// </summary>
        static Dictionary<object, MouseCapture> _captures = new Dictionary<object, MouseCapture>();

        /// <inheritdoc/>
        protected override void OnAttached()
        {
            AssociatedObject.Loaded += Target_Loaded;
        }

        /// <inheritdoc/>
        protected override void OnDetaching()
        {
            Target_Unloaded(AssociatedObject, new RoutedEventArgs());
        }

        /// <summary>
        /// is enabled changed handler
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="eventArgs">event args</param>
        static void IsEnabledChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (!(dependencyObject is ScrollViewer target)) return;
            var isEnabled = GetIsEnabled(dependencyObject);
            if ((bool)eventArgs.NewValue && !isEnabled)
            {
                target.Loaded += Target_Loaded;
            }
            else
            {
                if (isEnabled)
                    Target_Unloaded(target, new RoutedEventArgs());
            }
        }

        static void Target_Unloaded(object sender, RoutedEventArgs eventArgs)
        {
            if (!(sender is ScrollViewer target)) return;

            _captures.Remove(sender);

            target.Loaded -= Target_Loaded;
            target.Unloaded -= Target_Unloaded;
            target.PreviewMouseLeftButtonDown -= Target_PreviewMouseLeftButtonDown;
            target.PreviewMouseMove -= Target_PreviewMouseMove;

            target.PreviewMouseLeftButtonUp -= Target_PreviewMouseLeftButtonUp;
        }

        static void Target_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is ScrollViewer target)) return;

            _captures[sender] = new MouseCapture
            {
                HorizontalOffset = target.HorizontalOffset,
                VerticalOffset = target.VerticalOffset,
                Point = e.GetPosition(target),
            };
        }

        static void Target_Loaded(object sender, RoutedEventArgs eventArgs)
        {
            if (!(sender is ScrollViewer target)) return;

            target.Unloaded += Target_Unloaded;
            target.PreviewMouseLeftButtonDown += Target_PreviewMouseLeftButtonDown;
            target.PreviewMouseMove += Target_PreviewMouseMove;

            target.PreviewMouseLeftButtonUp += Target_PreviewMouseLeftButtonUp;
        }

        static void Target_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs eventArgs)
        {
            if (!(sender is ScrollViewer target)) return;

            target.ReleaseMouseCapture();
        }

        static void Target_PreviewMouseMove(object sender,
            MouseEventArgs eventArgs)
        {
            if (!_captures.ContainsKey(sender)) return;

            if (eventArgs.LeftButton != MouseButtonState.Pressed)
            {
                (sender as ScrollViewer).Cursor = null;
                _captures.Remove(sender);
                return;
            }

            if (!(sender is ScrollViewer target)) return;

            target.Cursor = Cursors.SizeAll;
            var capture = _captures[sender];

            var point = eventArgs.GetPosition(target);

            var dy = point.Y - capture.Point.Y;
            var dx = point.X - capture.Point.X;
            if (Math.Abs(dy) > EnableTriggerTreshold
                || Math.Abs(dx) > EnableTriggerTreshold)
            {
                target.CaptureMouse();
            }

            target.ScrollToHorizontalOffset(capture.HorizontalOffset - dx);
            target.ScrollToVerticalOffset(capture.VerticalOffset - dy);
        }

        internal class MouseCapture
        {
            /// <summary>
            /// horizontal offset
            /// </summary>
            public Double HorizontalOffset { get; set; }

            /// <summary>
            /// vertical offset
            /// </summary>
            public Double VerticalOffset { get; set; }

            /// <summary>
            /// point
            /// </summary>
            public Point Point { get; set; }
        }
    }
}
