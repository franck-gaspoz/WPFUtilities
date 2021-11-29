using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFUtilities.Converters
{
    public class DebugConverter
        : IValueConverter
    {
        static int n = 0;

        static DebugConverter _instance;
        public static DebugConverter Instance
            => _instance ?? (_instance = new DebugConverter());

        public static DebugConverter GetInstance()
        {
            return Instance;
        }

        protected DebugConverter()
        {
            //System.Diagnostics.Debug.WriteLine($"DebugConvert #{n}");
            n++;
        }

        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            //Debugger.Break();
            return value;
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            //Debugger.Break();
            return value;
        }
    }
}
