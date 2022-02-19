﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFUtilities.Components.UI.ScrollingExtensions
{
    /// <summary>
    /// touch scrolling behavior
    /// </summary>
    public partial class Scrolling
    {
        /// <summary>
        /// enabling trigger treshold
        /// </summary>
        static readonly double EnableTriggerTreshold = 5;

        static IScrollViewerTouchViewProperties GetOrResolveScrollViewerTouchViewProperties(ScrollViewer scrollViewer)
        {
            //var props = scrollViewer.GetValue(ScrollViewerTouchViewProperties)
        }

        /// <inheritdoc/>
        static void EnableScrollViewerTouch(ScrollViewer scrollViewer)
        {
            scrollViewer.PreviewMouseLeftButtonDown += Target_PreviewMouseLeftButtonDown;
            scrollViewer.PreviewMouseMove += Target_PreviewMouseMove;
            scrollViewer.PreviewMouseLeftButtonUp += Target_PreviewMouseLeftButtonUp;
        }

        /// <inheritdoc/>
        static void DisableScrollViewerTouch(ScrollViewer scrollViewer)
        {
            IsTracking = false;
            scrollViewer.PreviewMouseLeftButtonDown -= Target_PreviewMouseLeftButtonDown;
            scrollViewer.PreviewMouseMove -= Target_PreviewMouseMove;
            scrollViewer.PreviewMouseLeftButtonUp -= Target_PreviewMouseLeftButtonUp;
        }

        static void Target_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is ScrollViewer scrollViewer)) return;

            HorizontalOffset = scrollViewer.HorizontalOffset;
            VerticalOffset = scrollViewer.VerticalOffset;
            var point = e.GetPosition(scrollViewer);

            if (point.X > scrollViewer.ActualWidth - SystemParameters.ScrollWidth
                || point.Y > scrollViewer.ActualHeight - SystemParameters.ScrollHeight)
            {
                return;
            }
            Point = point;
            IsTracking = true;
        }

        static void Target_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is ScrollViewer scrollViewer)) return;

            IsTracking = false;
            scrollViewer.ReleaseMouseCapture();
        }

        static void Target_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!(sender is ScrollViewer scrollViewer)) return;

            if (!IsTracking || e.LeftButton != MouseButtonState.Pressed)
            {
                scrollViewer.Cursor = null;
                return;
            }

            var point = e.GetPosition(scrollViewer);

            scrollViewer.Cursor = Cursors.SizeAll;
            var dy = point.Y - Point.Y;
            var dx = point.X - Point.X;
            if (Math.Abs(dy) > EnableTriggerTreshold
                || Math.Abs(dx) > EnableTriggerTreshold)
            {
                scrollViewer.CaptureMouse();
            }

            scrollViewer.ScrollToHorizontalOffset(HorizontalOffset - dx);
            scrollViewer.ScrollToVerticalOffset(VerticalOffset - dy);
        }

    }
}
