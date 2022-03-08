using System;
using System.Globalization;
using System.Windows.Data;

using WPFUtilities.ComponentModels;

namespace WPFUtilities.Converters
{
    /// <summary>
    /// multi-purpose debug converter
    /// </summary>
    public class DebugMultiConverter
        : SingletonService<DebugMultiConverter>, IMultiValueConverter
    {
        /// <summary>
        /// instance counter
        /// </summary>
        public static int InstanceCount = 0;

        static Action<DebugMultiConverter, object[], object> CallBack = (debugConverter, values, parameter) =>
          {
              var txtvalues = "[" + string.Join(",", values) + "]";
              var message = $"#{InstanceCount} value={txtvalues} parameter={parameter}";
              System.Diagnostics.Debug.WriteLine(message);
          };

        /// <summary>
        /// build a new instance, update instance counter
        /// </summary>
        public DebugMultiConverter()
        {
            InstanceCount++;
        }

        /// <summary>
        /// convert
        /// </summary>
        /// <param name="values">values</param>
        /// <param name="targetType">target ype</param>
        /// <param name="parameter">parameter</param>
        /// <param name="culture">culture</param>
        /// <returns>value</returns>
        public object Convert(
            object[] values,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            CallBack?.Invoke(this, values, parameter);
            return null;
        }

        /// <inheritdoc/>
        public object[] ConvertBack(
            object value,
            Type[] targetTypes,
            object parameter,
            CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
