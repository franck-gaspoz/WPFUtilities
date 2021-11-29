using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFUtilities.Converters
{
    public class ValidDoubleValueFilterConverter
        : IValueConverter
    {
        static ValidDoubleValueFilterConverter _instance;
        public static ValidDoubleValueFilterConverter Instance
            => _instance ?? (_instance = new ValidDoubleValueFilterConverter());

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

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            return value;
        }
    }
}
