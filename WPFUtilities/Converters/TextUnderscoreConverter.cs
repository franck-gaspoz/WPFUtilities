using System;
using System.Globalization;
using System.Windows.Data;

using WPFUtilities.ComponentModels;

namespace WPFUtilities.Converters
{
    /// <summary>
    /// disabled underscores in a string value (avoid underlined first words letters)
    /// </summary>
    public class TextUnderscoreConverter
        : SingletonService<TextUnderscoreConverter>, IValueConverter
    {
        /// <summary>
        /// disable underscores in a string value
        /// </summary>
        /// <param name="value">value, expects a string</param>
        /// <param name="targetType">target type</param>
        /// <param name="parameter">parameter</param>
        /// <param name="culture">culture</param>
        /// <returns>transformed value or value if not a string</returns>
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            try
            {
                string s = (string)value;
                s = s.Replace("_", "__");
                return s;
            }
            catch
            {
                return value;
            }
        }

        /// <summary>
        /// convert back
        /// </summary>
        /// <exception cref="NotImplementedException">not implemented</exception>
        /// <param name="value">value</param>
        /// <param name="targetType">target type</param>
        /// <param name="parameter">parameter</param>
        /// <param name="culture">culture</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
