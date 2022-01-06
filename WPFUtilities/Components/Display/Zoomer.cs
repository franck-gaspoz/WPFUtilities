
#define usepoint
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

using WPFUtilities.ComponentModels;

namespace WPFUtilities.Components.Display
{
    /// <summary>
    /// zoomer interactor (TODO: complete)
    /// </summary>
    public class Zoomer : ModelBase
    {
        static Zoomer _instance;
        /// <summary>
        /// shared instance
        /// </summary>
        public static Zoomer Instance => _instance ?? (_instance = new Zoomer());

        ScrollViewer _scrollViewer;
        FrameworkElement _target;
        double _zoomFactor;

        int _zfCount = 0;
        double _relx = 0, _rely = 0;

        /// <summary>
        /// set zoom factor
        /// </summary>
        /// <param name="zoomFactor">zoom factor (0..100)</param>
        /// <param name="target">the zoomed framework element</param>
        /// <param name="scrollViewer">if not null is used to keep view at same place</param>
        public void SetZoom(
            double zoomFactor,
            FrameworkElement target,
            ScrollViewer scrollViewer
            )
        {
            _zoomFactor = zoomFactor;
            _scrollViewer = scrollViewer;
            _target = target;
            var tg = target.LayoutTransform as TransformGroup;
            var gs = tg.Children[0] as ScaleTransform;

            //gs.CenterX = _scrollViewer.

            if (_scrollViewer != null)
            {
                computeTranslate();
                //_scrollViewer.ScrollChanged += _scrollViewer_ScrollChanged;
                //target.SizeChanged += Target_SizeChanged;
            }

            //gs.CenterX = _scrollViewer.HorizontalOffset + ((_scrollViewer.ViewportWidth - _scrollViewer.HorizontalOffset) / 2d);
            //gs.CenterY = _scrollViewer.VerticalOffset + ((_scrollViewer.ViewportHeight - _scrollViewer.VerticalOffset) / 2d);

            //gs.CenterX = _scrollViewer.ViewportWidth / 2d;
            //gs.CenterY = _scrollViewer.VerticalOffset / 2d;

            _oldScrollableWidth = _oldScrollableHeight = 0;
            _zfCount = 0;
            SetZoom(zoomFactor, gs);

#if usepoint

#else
            _target.Dispatcher.BeginInvoke(
                 DispatcherPriority.Loaded,
                 new Action(() => ScrollViewer_ScrollChanged(null, null)));
#endif
        }


        double _scrollableWidth = 0;
        /// <summary>
        /// summary
        /// </summary>
        public double ScrollableWidth
        {
            get
            {
                return _scrollableWidth;
            }
            set
            {
                _scrollableWidth = value;
                NotifyPropertyChanged();
            }
        }

        private void Target_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //_scrollViewer_ScrollChanged(sender, null);
        }

        void computeTranslate()
        {
            var offsetX = _scrollViewer.HorizontalOffset;
            var offsetY = _scrollViewer.VerticalOffset;
            _relx = offsetX / _scrollViewer.ScrollableWidth;
            _rely = offsetY / _scrollViewer.ScrollableHeight;
            //Log.Debug($"Zoomer: offsetX={offsetX} _relx={_relx} offsetY={offsetY} _rely={_rely}");
        }

        double _oldScrollableWidth = 0, _oldScrollableHeight = 0;

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e == null || _oldScrollableWidth != _scrollViewer.ScrollableWidth)
            {
                _oldScrollableWidth = _scrollViewer.ScrollableWidth;
                var offsetX = GetOffsetX(_relx, _scrollViewer);
                _scrollViewer.ScrollToHorizontalOffset(offsetX);
                //var relx = offsetX / _scrollViewer.ScrollableWidth;
                //Log.Debug($"Zoomer: ScrollViewer scroll changed: offsetX={offsetX} relx={relx} sw={_scrollViewer.ScrollableWidth}");
            }

            if (e == null || _oldScrollableHeight != _scrollViewer.ScrollableHeight)
            {
                _oldScrollableHeight = _scrollViewer.ScrollableHeight;
                var offsetY = GetOffsetY(_rely, _scrollViewer);
                _scrollViewer.ScrollToVerticalOffset(offsetY);
                //var rely = offsetY / _scrollViewer.ScrollableHeight;
                //Log.Debug($"Zoomer: ScrollViewer scroll changed: offsetY={offsetY} rely={rely} sh={_scrollViewer.ScrollableHeight}");
            }
        }

        /// <summary>
        /// get scroll viewer y offset
        /// </summary>
        /// <param name="relativeY">relative y</param>
        /// <param name="scrollViewer">scroll viewer</param>
        /// <returns>y offset</returns>
        public static double GetOffsetY(double relativeY, ScrollViewer scrollViewer)
            => double.IsNaN(relativeY) ? scrollViewer.ScrollableHeight * 0.5d
                : scrollViewer.ScrollableHeight * relativeY;

        /// <summary>
        /// get scroll viewer x offset
        /// </summary>
        /// <param name="relativeX">relative x</param>
        /// <param name="scrollViewer">scroll viewer</param>
        /// <returns>x offset</returns>
        public static double GetOffsetX(double relativeX, ScrollViewer scrollViewer)
            => double.IsNaN(relativeX) ? scrollViewer.ScrollableWidth * 0.5d
            : scrollViewer.ScrollableWidth * relativeX;

        /// <summary>
        /// set zoom factor
        /// </summary>
        /// <param name="zoomFactor">zoom factor (0..1)</param>
        /// <param name="scaleTransform">scale transform</param>
        void SetZoom(
            double zoomFactor,
            ScaleTransform scaleTransform
        )
        {
            scaleTransform.ScaleX = zoomFactor;
            scaleTransform.ScaleY = zoomFactor;
        }

        /// <summary>
        /// set zoom with animation
        /// </summary>
        /// <param name="zoomFactor">zoom factor (0..1)</param>
        /// <param name="scaleTransform">scale transform</param>
        void SetZoom2(
            double zoomFactor,
            ScaleTransform scaleTransform
        )
        {
            // animation duration in milli secondes (150)
            var duration =
                new Duration(TimeSpan.FromMilliseconds(150));
            var zfx =
                new DoubleAnimation(
                    scaleTransform.ScaleX,
                    zoomFactor,
                    duration);
            zfx.Completed += setZoomFactorAnimationCompleted;
            var zfy =
                new DoubleAnimation(
                    scaleTransform.ScaleY,
                    zoomFactor,
                    duration);
            zfy.Completed += setZoomFactorAnimationCompleted;
            scaleTransform
                .BeginAnimation(
                    ScaleTransform.ScaleXProperty,
                    zfx);
            scaleTransform
                 .BeginAnimation(
                    ScaleTransform.ScaleYProperty,
                    zfy);
        }

        /// <summary>
        /// zoom factor anim completed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="eventArgs">event args</param>
        private void setZoomFactorAnimationCompleted(object sender, EventArgs eventArgs)
        {
            _zfCount++;
            if (_zfCount == 2)
            {
                if (_scrollViewer != null)
                {
                    _scrollViewer.ScrollChanged -= ScrollViewer_ScrollChanged;
                    //_target.SizeChanged -= Target_SizeChanged;
                    ScrollViewer_ScrollChanged(null, null);
                }
            }
        }
    }
}
