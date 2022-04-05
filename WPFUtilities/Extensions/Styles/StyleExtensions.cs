using System.Windows;

namespace WPFUtilities.Extensions.Styles
{
    /// <summary>
    /// style extensions
    /// </summary>
    public static class StyleExtensions
    {
        /// <summary>
        /// clone a style
        /// </summary>
        /// <param name="style">style to clone</param>
        /// <returns>cloned style</returns>
        public static Style MakeCopy(this Style style)
        {
            var newStyle = new Style(style.TargetType, style);

            foreach (var setter in style.Setters)
                newStyle
                    .Setters
                    .Add(setter);

            return newStyle;
        }
    }
}
