using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFUtilities.Converters
{
    public class ObjectIsNotNullToBoolConverter
        : IValueConverter
    {
        static ObjectIsNotNullToBoolConverter _instance;
        public static ObjectIsNotNullToBoolConverter Instance
            => _instance ?? (_instance = new ObjectIsNotNullToBoolConverter());

        public ObjectIsNotNullToBoolConverter()
        {
        }

        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            return null;
        }
    }
}
