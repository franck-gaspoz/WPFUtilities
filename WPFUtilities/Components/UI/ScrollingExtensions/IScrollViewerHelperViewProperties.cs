using System.Windows.Controls;

namespace WPFUtilities.Components.UI.ScrollingExtensions
{
    /// <summary>
    /// scroll viewer helper view properties
    /// </summary>
    public interface IScrollViewerHelperViewProperties
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
