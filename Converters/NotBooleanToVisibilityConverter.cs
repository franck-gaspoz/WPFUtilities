using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFUtilities.Converters
{
    public class NotBooleanToVisibilityConverter
        : IValueConverter
    {
        static NotBooleanToVisibilityConverter _instance;
        public static NotBooleanToVisibilityConverter Instance
            => _instance = _instance ?? (_instance = new NotBooleanToVisibilityConverter());

        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            try
            {
                bool v = (bool)value;
                return !v ?
                    Visibility.Visible :
                    ((parameter is Visibility notVisibleVisibility) ?
                        notVisibleVisibility :
                        Visibility.Collapsed);
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
            return null;
        }
    }
}
