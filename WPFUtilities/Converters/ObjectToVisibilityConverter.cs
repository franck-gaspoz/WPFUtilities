using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

using WPFUtilities.ComponentModels;

namespace WPFUtilities.Converters
{
    /// <summary>
    /// convert an object value to a visbibility value depending on its nullity
    /// </summary>
    public class ObjectToVisibilityConverter
        : SingletonService<ObjectToVisibilityConverter>, IValueConverter
    {
        /// <summary>
        /// convert from object to Visibility
        /// </summary>
        /// <param name="value">value, expects an object</param>
        /// <param name="targetType">target type</param>
        /// <param name="parameter">parameter</param>
        /// <param name="culture">culture</param>
        /// <returns>Visiblity.Visible if value is not null, Collapsed otherwize</returns>
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            return (value == null) ? Visibility.Collapsed : Visibility.Visible;
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
