using System;
using System.Globalization;
using System.Windows.Data;

using WPFUtilities.ComponentModel;

namespace WPFUtilities.Converters
{
    /// <summary>
    /// object is not null to bool converter
    /// </summary>
    public class ObjectIsNotNullToBoolConverter
        : Singleton<ObjectIsNotNullToBoolConverter>, IValueConverter
    {
        /// <summary>
        /// returns true if value is not null, false otherwize
        /// </summary>
        /// <param name="value">value, expects a boolean</param>
        /// <param name="targetType">target type</param>
        /// <param name="parameter">parameter</param>
        /// <param name="culture">culture</param>
        /// <returns>!value. returns false if not a boolean</returns>
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
            => value != null;

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
