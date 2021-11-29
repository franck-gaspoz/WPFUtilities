using System.Windows.Media;

using WPFUtilities.Components.Display;

namespace WPFUtilities.Command.Display
{
    public class ZoomCommand : CommandBase<ZoomCommandParameters>
    {
        public override void Execute(object parameter)
        {
            var p = Cast(parameter);
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
