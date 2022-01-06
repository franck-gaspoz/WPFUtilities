using System.Windows;
using System.Windows.Controls;

namespace WPFUtilities.Commands.Render
{
    /// <summary>
    /// zoom command parameters
    /// </summary>
    public class ZoomCommandParameters
    {
        /// <summary>
        /// target element
        /// </summary>
        public FrameworkElement Target { get; set; }

        /// <summary>
        /// scroll viewer containing target element
        /// </summary>
        public ScrollViewer ScrollViewer { get; set; }

        /// <summary>
        /// zoom factor (0..1)
        /// </summary>
        public double Zoom { get; set; }

        /// <summary>
        /// creates a new instance
        /// </summary>
        /// <param name="target">target element</param>
        /// <param name="zoom">zoom factor</param>
        public ZoomCommandParameters(FrameworkElement target, double zoom)
        {
            Target = target;
            Zoom = zoom;
        }

    }
}
