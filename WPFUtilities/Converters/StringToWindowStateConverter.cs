using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFUtilities.Converters
{
    public class StringToWindowStateConverter
        : IValueConverter
    {
        static StringToWindowStateConverter _instance;
        public static StringToWindowStateConverter Instance
        {
            get
            {
                _instance = _instance ?? new StringToWindowStateConverter();
                return _instance;
            }
        }

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

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            return value?.ToString();
        }
    }
}
