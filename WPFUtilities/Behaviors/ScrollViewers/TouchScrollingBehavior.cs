using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Microsoft.Xaml.Behaviors;

namespace WPFUtilities.Behaviors.ScrollViewers
{
    public class TouchScrollingBehavior :
        Behavior<ScrollViewer>
    {
        static readonly double EnableTriggerTreshold = 5;

        public Double HorizontalOffset { get; set; }
        public Double VerticalOffset { get; set; }
        public Point Point { get; set; }
        public bool IsTracking { get; set; }

        protected override void OnAttached()
        {
            AssociatedObject.PreviewMouseLeftButtonDown += Target_PreviewMouseLeftButtonDown;
            AssociatedObject.PreviewMouseMove += Target_PreviewMouseMove;
            AssociatedObject.PreviewMouseLeftButtonUp += Target_PreviewMouseLeftButtonUp;
        }

        protected override void OnDetaching()
        {
            IsTracking = false;
            AssociatedObject.PreviewMouseLeftButtonDown -= Target_PreviewMouseLeftButtonDown;
            AssociatedObject.PreviewMouseMove -= Target_PreviewMouseMove;
            AssociatedObject.PreviewMouseLeftButtonUp -= Target_PreviewMouseLeftButtonUp;
        }

        void Target_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HorizontalOffset = AssociatedObject.HorizontalOffset;
            VerticalOffset = AssociatedObject.VerticalOffset;
            var point = e.GetPosition(AssociatedObject);

            if (point.X > AssociatedObject.ActualWidth - SystemParameters.ScrollWidth
                || point.Y > AssociatedObject.ActualHeight - SystemParameters.ScrollHeight)
            {
                return;
            }
            Point = point;
            IsTracking = true;
        }

        void Target_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsTracking = false;
            AssociatedObject.ReleaseMouseCapture();
        }

        void Target_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!IsTracking || e.LeftButton != MouseButtonState.Pressed)
            {
                AssociatedObject.Cursor = null;
                return;
            }

            var point = e.GetPosition(AssociatedObject);

            AssociatedObject.Cursor = Cursors.SizeAll;
            var dy = point.Y - Point.Y;
            var dx = point.X - Point.X;
            if (Math.Abs(dy) > EnableTriggerTreshold
                || Math.Abs(dx) > EnableTriggerTreshold)
            {
                AssociatedObject.CaptureMouse();
            }

            AssociatedObject.ScrollToHorizontalOffset(HorizontalOffset - dx);
            AssociatedObject.ScrollToVerticalOffset(VerticalOffset - dy);
        }

    }
}
