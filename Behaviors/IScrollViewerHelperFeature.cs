using System.Windows.Controls;

namespace WPFUtilities.Behaviors
{
    public interface IScrollViewerHelperFeature
    {
        double HorizontalOffset { get; set; }

        double VerticalOffset { get; set; }

        //event ScrollChangedEventHandler ScrollChanged;

        ScrollViewer ScrollViewer { get; set; }
    }
}
