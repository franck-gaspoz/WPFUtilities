using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFUtilities.Converters
{
    public class TextUnderscoreConverter
        : IValueConverter
    {
        static TextUnderscoreConverter _instance;
        public static TextUnderscoreConverter Instance
            => _instance ?? (_instance = new TextUnderscoreConverter());

        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            try
            {
                string s = (string)value;
                s = s.Replace("_", "__");
                return s;
            }
            catch
            {
                return value;
            }
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
