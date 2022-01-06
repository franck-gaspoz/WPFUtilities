using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

using WPFUtilities.ComponentModels;

namespace WPFUtilities.Converters
{
    /// <summary>
    /// convert multi values to a value list
    /// </summary>
    public class MultiBindingConverter
        : Singleton<MultiBindingConverter>, IMultiValueConverter
    {
        /// <summary>
        /// object[] to List&gt;object&gt;
        /// </summary>
        /// <param name="values">values</param>
        /// <param name="targetType">target type</param>
        /// <param name="parameter">Enum type</param>
        /// <param name="culture">culture</param>
        /// <returns>string value of the enum value</returns>
        public object Convert(
            object[] values,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            return values.ToList();
        }

        /// <summary>
        /// convert back
        /// </summary>
        /// <exception cref="NotImplementedException">not implemented</exception>
        /// <param name="value">value</param>
        /// <param name="targetTypes">target types</param>
        /// <param name="parameter">parameter</param>
        /// <param name="culture">culture</param>
        /// <returns></returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
