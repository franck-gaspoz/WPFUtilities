using System.Windows;

namespace WPFUtilities.Components.UI.ScrollingExtensions
{
    /// <summary>
    /// scroll viewer touch view properties
    /// </summary>
    public interface IScrollViewerTouchViewProperties
    {
        /// <summary>
        /// horizontal offset
        /// </summary>
        double HorizontalOffset { get; set; }

        /// <summary>
        /// vertical offset
        /// </summary>
        double VerticalOffset { get; set; }

        /// <summary>
        /// point
        /// </summary>
        Point Point { get; set; }

        /// <summary>
        /// is tracking
        /// </summary>
        bool IsTracking { get; set; }
    }
}
