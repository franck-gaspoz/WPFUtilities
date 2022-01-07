using System;
using System.Globalization;
using System.Windows.Data;

using WPFUtilities.ComponentModels;

namespace WPFUtilities.Converters
{
    /// <summary>
    /// transform NaN and Infinite double values to 0, emse keep value
    /// </summary>
    public class ValidDoubleValueFilterConverter
        : SingletonService<ValidDoubleValueFilterConverter>, IValueConverter
    {
        /// <summary>
        /// transform NaN and Infinite double values to 0, emse keep value
        /// </summary>
        /// <param name="value">value, expects a double</param>
        /// <param name="targetType">target type</param>
        /// <param name="parameter">parameter</param>
        /// <param name="culture">culture</param>
        /// <returns>transformed value or value if not a double</returns>
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (value is double d)
            {
                if (double.IsNaN(d)
                    || double.IsInfinity(d))
                    return 0d;
            }
            return value;
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
