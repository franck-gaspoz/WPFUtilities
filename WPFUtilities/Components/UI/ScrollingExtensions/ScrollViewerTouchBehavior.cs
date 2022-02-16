using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Microsoft.Xaml.Behaviors;

namespace WPFUtilities.Components.UI.ScrollingExtensions
{
    /// <summary>
    /// touch scrolling behavior
    /// </summary>
    public class ScrollViewerTouchBehavior :
        Behavior<ScrollViewer>
    {
        /// <summary>
        /// enabling trigger treshold
        /// </summary>
        static readonly double EnableTriggerTreshold = 5;

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

        /// <summary>
        /// is tracking
        /// </summary>
        public bool IsTracking { get; set; }

        /// <inheritdoc/>
        protected override void OnAttached()
        {
            AssociatedObject.PreviewMouseLeftButtonDown += Target_PreviewMouseLeftButtonDown;
            AssociatedObject.PreviewMouseMove += Target_PreviewMouseMove;
            AssociatedObject.PreviewMouseLeftButtonUp += Target_PreviewMouseLeftButtonUp;
        }

        /// <inheritdoc/>
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
