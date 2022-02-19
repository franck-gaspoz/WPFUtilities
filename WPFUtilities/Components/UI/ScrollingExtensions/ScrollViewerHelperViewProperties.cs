using System.Windows.Controls;

namespace WPFUtilities.Components.UI.ScrollingExtensions
{
    /// <summary>
    /// scroll viewer helper view properties
    /// </summary>
    public class ScrollViewerHelperViewProperties
        : IScrollViewerHelperViewProperties
    {
        /// <inheritdoc/>
        public double HorizontalOffset { get; set; }

        /// <inheritdoc/>
        public double VerticalOffset { get; set; }

        /// <inheritdoc/>
        public ScrollViewer ScrollViewer { get; set; }
    }
}
