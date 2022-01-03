using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

using WPFUtilities.ComponentModel;

namespace WPFUtilities.Converters
{
    /// <summary>
    /// string to window state converter
    /// </summary>
    public class StringToWindowStateConverter
        : Singleton<StringToWindowStateConverter>, IValueConverter
    {
        /// <summary>
        /// transforms a string value representing a window state to a window state value, returns Normal if fail, returns collapsed if not a string value
        /// </summary>
        /// <param name="value">value, expects a double</param>
        /// <param name="targetType">target type</param>
        /// <param name="parameter">parameter</param>
        /// <param name="culture">culture</param>
        /// <returns>transformed value or Collapsed if not a string value</returns>
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            try
            {
                string v = (string)value;
                return (Enum.TryParse<WindowState>(v, out var ws)) ?
                    ws
                    : WindowState.Normal;
            }
            catch
            {
                return Visibility.Collapsed;
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
