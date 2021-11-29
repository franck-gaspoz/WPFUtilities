using System.Windows;
using System.Windows.Controls;

namespace WPFUtilities.Command.Display
{
    public class ZoomCommandParameters
    {
        public FrameworkElement Target { get; set; }

        public ScrollViewer ScrollViewer { get; set; }

        public double Zoom { get; set; }

        public ZoomCommandParameters(FrameworkElement target, double zoom)
        {
            Target = target;
            Zoom = zoom;
        }

        public ZoomCommandParameters() { }
    }
}
