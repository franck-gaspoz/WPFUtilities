using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFUtilities.Behaviors.ScrollViewers
{
    public class ScrollViewerTouchScrollingBehavior : DependencyObject
    {
        static readonly double EnableTriggerTreshold = 5;

        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEnabledProperty, value);
        }

        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled",
                typeof(bool),
                typeof(ScrollViewerTouchScrollingBehavior),
                new UIPropertyMetadata(false, IsEnabledChanged));

        static Dictionary<object, MouseCapture> _captures = new Dictionary<object, MouseCapture>();

        static void IsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is ScrollViewer target)) return;

            if ((bool)e.NewValue)
            {
                target.Loaded += Target_Loaded;
            }
            else
            {
                Target_Unloaded(target, new RoutedEventArgs());
            }
        }

        static void Target_Unloaded(object sender, RoutedEventArgs e)
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

        static void Target_Loaded(object sender, RoutedEventArgs e)
        {
            if (!(sender is ScrollViewer target)) return;

            target.Unloaded += Target_Unloaded;
            target.PreviewMouseLeftButtonDown += Target_PreviewMouseLeftButtonDown;
            target.PreviewMouseMove += Target_PreviewMouseMove;

            target.PreviewMouseLeftButtonUp += Target_PreviewMouseLeftButtonUp;
        }

        static void Target_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is ScrollViewer target)) return;

            target.ReleaseMouseCapture();
        }

        static void Target_PreviewMouseMove(object sender,
            MouseEventArgs e)
        {
            if (!_captures.ContainsKey(sender)) return;

            if (e.LeftButton != MouseButtonState.Pressed)
            {
                (sender as ScrollViewer).Cursor = null;
                _captures.Remove(sender);
                return;
            }

            if (!(sender is ScrollViewer target)) return;

            target.Cursor = Cursors.SizeAll;
            var capture = _captures[sender];

            var point = e.GetPosition(target);

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
            public Double HorizontalOffset { get; set; }
            public Double VerticalOffset { get; set; }
            public Point Point { get; set; }
        }
    }
}
