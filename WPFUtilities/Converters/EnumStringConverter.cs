using System;
using System.Globalization;
using System.Windows.Data;

using WPFUtilities.ComponentModels;

namespace WPFUtilities.Converters
{
    /// <summary>
    /// enum string converter
    /// </summary>
    public class EnumStringConverter
        : Singleton<EnumStringConverter>, IValueConverter
    {
        /// <summary>
        /// Enum -> string
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="targetType">target type</param>
        /// <param name="parameter">Enum type</param>
        /// <param name="culture">culture</param>
        /// <returns>string value of the enum value</returns>
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (parameter is Type valueType)
            {
                return value?.ToString();
            }
            return value;
        }

        /// <summary>
        /// string -> Enum
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="targetType">target type</param>
        /// <param name="parameter">Enum type</param>
        /// <param name="culture">culture</param>
        /// <returns>enum of the string value or value if wrong parameters</returns>
        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (parameter is Type valueType
                && value is string enumString)
            {
                return Enum.Parse(valueType, enumString);
            }
            return value;
        }
    }
}
