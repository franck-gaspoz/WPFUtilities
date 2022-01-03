using System.Windows.Controls;

namespace WPFUtilities.Behaviors.ScrollViewers
{
    public interface IScrollViewerHelperFeature
    {
        double HorizontalOffset { get; set; }

        double VerticalOffset { get; set; }

        //event ScrollChangedEventHandler ScrollChanged;

        ScrollViewer ScrollViewer { get; set; }
    }
}
