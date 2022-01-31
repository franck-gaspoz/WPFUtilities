using System.Windows.Media;

using WPFUtilities.Commands.Abstract;
using WPFUtilities.Components.Render;

namespace WPFUtilities.Commands.Render
{
    /// <summary>
    /// zoom
    /// </summary>
    public class ZoomCommand : AbstractCommand<ZoomCommand>
    {
        /// <summary>
        /// zoom an element
        /// </summary>
        /// <param name="parameter">ZoomCommandParameters</param>
        public override void Execute(object parameter)
        {
            var p = (ZoomCommandParameters)parameter;
            if (p != null && p.Target != null)
            {
                var zoomFactor = ((double)p.Zoom) / 100d;
                var tg = p.Target.LayoutTransform as TransformGroup;

                Zoomer.Instance.SetZoom(
                    zoomFactor,
                    p.Target,
                    p.ScrollViewer);
            }
        }
    }
}
