using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace WPFUtilities.Converters
{
    public class MultiBindingConverter
        : IMultiValueConverter
    {
        static MultiBindingConverter _instance;
        public static MultiBindingConverter Instance
            => _instance ?? (_instance = new MultiBindingConverter());

        public object Convert(
            object[] values,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            return values.ToList();
        }

        public object[] ConvertBack(
            object value,
            Type[] targetTypes,
            object parameter,
            CultureInfo culture)
            => throw new NotImplementedException();
    }
}
