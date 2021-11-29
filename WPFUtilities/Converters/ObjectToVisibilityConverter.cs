using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFUtilities.Converters
{
    public class ObjectToVisibilityConverter
        : IValueConverter
    {
        static ObjectToVisibilityConverter _instance;
        public static ObjectToVisibilityConverter Instance
            => _instance ?? (_instance = new ObjectToVisibilityConverter());

        public ObjectToVisibilityConverter()
        {
        }

        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            return (value == null) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            //Debugger.Break();
            return null;
        }
    }
}
