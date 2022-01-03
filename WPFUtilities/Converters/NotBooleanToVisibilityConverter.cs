using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

using WPFUtilities.ComponentModel;

namespace WPFUtilities.Converters
{
    /// <summary>
    /// converts a boolean to a visibility value
    /// </summary>
    public class NotBooleanToVisibilityConverter :
        Singleton<NotBooleanToVisibilityConverter>,
        IValueConverter
    {
        /// <summary>
        /// convert from bool to Visibility
        /// </summary>
        /// <param name="value">value, expects a boolean</param>
        /// <param name="targetType">target type</param>
        /// <param name="parameter">parameter</param>
        /// <param name="culture">culture</param>
        /// <returns>Visiblity.Visibile if value is false, Collapsed otherwize returns Collapsed if value is not a Visibility</returns>
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            try
            {
                var visibility = !(bool)value;
                return visibility ?
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

        /// <summary>
        /// convert back from Visiblity to bool
        /// </summary>
        /// <param name="value">value, expects a visiblity</param>
        /// <param name="targetType">expect </param>
        /// <param name="parameter">parameter</param>
        /// <param name="culture">culture</param>
        /// <returns>false if visivility is Visible, true otherwize returns true if value is not a boolean</returns>
        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            try
            {
                var visibility = (Visibility)value;
                return visibility == Visibility.Visible ? false : true;
            }
            catch
            {
                return true;
            }
        }
    }
}