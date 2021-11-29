using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFUtilities.Converters
{
    public class EnumStringConverter
        : IValueConverter
    {
        static EnumStringConverter _instance;
        public static EnumStringConverter Instance
            => _instance = _instance ?? new EnumStringConverter();

        protected EnumStringConverter() { }

        /// <summary>
        /// Enum -> string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
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
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
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
