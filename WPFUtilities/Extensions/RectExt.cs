using System.Windows;

namespace WPFUtilities.Extensions
{
    public static class RectExt
    {
        public static string ToText(this Rect r)
            => $"({r.X} , {r.Y}) -> ({r.Right - 1} , {r.Bottom - 1}) [{r.Width}x{r.Height}]";
    }
}
