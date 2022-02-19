using System.Windows;

namespace WPFUtilities.Components.UI.ScrollingExtensions
{
    /// <summary>
    /// scroll viewer touch view properties
    /// </summary>
    public class ScrollViewerTouchViewProperties
        : IScrollViewerTouchViewProperties
    {
        /// <inheritdoc/>
        public double HorizontalOffset { get; set; }

        /// <inheritdoc/>
        public double VerticalOffset { get; set; }

        /// <inheritdoc/>
        public Point Point { get; set; }

        /// <inheritdoc/>
        public bool IsTracking { get; set; }
    }
}
