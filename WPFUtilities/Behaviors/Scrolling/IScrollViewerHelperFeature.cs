using System.Windows.Controls;

namespace WPFUtilities.Behaviors.Scrolling
{
    /// <summary>
    /// scroll viewer helper feature model interface
    /// </summary>
    public interface IScrollViewerHelperFeature
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
        /// scroll viewer control
        /// </summary>
        ScrollViewer ScrollViewer { get; set; }
    }
}
