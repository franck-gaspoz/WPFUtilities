using System.Windows;

namespace WPFUtilities.Extensions.Geom
{
    /// <summary>
    /// rect extensions
    /// </summary>
    public static class RectExtensions
    {
        /// <summary>
        /// text representation of a Rect
        /// </summary>
        /// <param name="rect">rect</param>
        /// <returns>text representing the Rect object</returns>
        public static string ToText(this Rect rect)
            => $"({rect.X} , {rect.Y}) -> ({rect.Right - 1} , {rect.Bottom - 1}) [{rect.Width}x{rect.Height}]";
    }
}
