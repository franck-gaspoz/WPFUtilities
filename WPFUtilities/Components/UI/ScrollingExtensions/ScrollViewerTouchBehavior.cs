using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using WPFUtilities.Extensions.Services;

namespace WPFUtilities.Components.UI.ScrollingExtensions
{
    /// <summary>
    /// touch scrolling behavior
    /// </summary>
    public partial class Scrolling
    {
        #region scroll viewer touch view properties

        /// <summary>
        /// get scroll viewer touch view properties
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <returns></returns>
        public static IScrollViewerTouchViewProperties GetScrollViewerTouchViewPropertiesProperty(DependencyObject dependencyObject)
            => (IScrollViewerTouchViewProperties)dependencyObject.GetValue(ScrollViewerTouchViewPropertiesProperty);

        /// <summary>
        /// set scroll viewer touch view properties
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">value</param>
        public static void SetScrollViewerTouchViewPropertiesProperty(DependencyObject dependencyObject, IScrollViewerTouchViewProperties value)
            => dependencyObject.SetValue(ScrollViewerTouchViewPropertiesProperty, value);

        /// <summary>
        /// scroll viewer touch view properties
        /// </summary>
        public static readonly DependencyProperty ScrollViewerTouchViewPropertiesProperty =
            DependencyProperty.Register(
                "ScrollViewerTouchViewProperties",
                typeof(IScrollViewerTouchViewProperties),
                typeof(ScrollViewer),
                new PropertyMetadata(null));

        #endregion

        /// <summary>
        /// enabling trigger treshold
        /// </summary>
        static readonly double EnableTriggerTreshold = 5;

        static IScrollViewerTouchViewProperties GetOrResolveScrollViewerTouchViewProperties(ScrollViewer scrollViewer)
            => scrollViewer.GetResolveCreateServiceFromProperty<IScrollViewerTouchViewProperties>(
                ScrollViewerTouchViewPropertiesProperty,
                () => new ScrollViewerTouchViewProperties());

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
            var props = GetOrResolveScrollViewerTouchViewProperties(scrollViewer);

            props.IsTracking = false;
            scrollViewer.PreviewMouseLeftButtonDown -= Target_PreviewMouseLeftButtonDown;
            scrollViewer.PreviewMouseMove -= Target_PreviewMouseMove;
            scrollViewer.PreviewMouseLeftButtonUp -= Target_PreviewMouseLeftButtonUp;
        }

        static void Target_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is ScrollViewer scrollViewer)) return;
            var props = GetOrResolveScrollViewerTouchViewProperties(scrollViewer);

            props.HorizontalOffset = scrollViewer.HorizontalOffset;
            props.VerticalOffset = scrollViewer.VerticalOffset;
            var point = e.GetPosition(scrollViewer);

            if (point.X > scrollViewer.ActualWidth - SystemParameters.ScrollWidth
                || point.Y > scrollViewer.ActualHeight - SystemParameters.ScrollHeight)
            {
                return;
            }
            props.Point = point;
            props.IsTracking = true;
        }

        static void Target_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is ScrollViewer scrollViewer)) return;
            var props = GetOrResolveScrollViewerTouchViewProperties(scrollViewer);

            props.IsTracking = false;
            scrollViewer.ReleaseMouseCapture();
        }

        static void Target_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!(sender is ScrollViewer scrollViewer)) return;
            var props = GetOrResolveScrollViewerTouchViewProperties(scrollViewer);

            if (!props.IsTracking || e.LeftButton != MouseButtonState.Pressed)
            {
                scrollViewer.Cursor = null;
                return;
            }

            var point = e.GetPosition(scrollViewer);

            scrollViewer.Cursor = Cursors.SizeAll;
            var dy = point.Y - props.Point.Y;
            var dx = point.X - props.Point.X;
            if (Math.Abs(dy) > EnableTriggerTreshold
                || Math.Abs(dx) > EnableTriggerTreshold)
            {
                scrollViewer.CaptureMouse();
            }

            scrollViewer.ScrollToHorizontalOffset(props.HorizontalOffset - dx);
            scrollViewer.ScrollToVerticalOffset(props.VerticalOffset - dy);
        }

    }
}
