using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

using WPFUtilities.ComponentModels;

namespace WPFUtilities.Converters
{
    /// <summary>
    /// converts a number to a visibility value, visibility is hidden if 0 if , collapsed if &lt; 0 , visible if &gt; 0
    /// </summary>
    public class NumberToVisibilityConverter :
        SingletonService<NumberToVisibilityConverter>,
        IValueConverter
    {
        /// <summary>
        /// convert from number to Visibility, visibility is hidden if 0 if , collapsed if &lt; 0 , visible if &gt; 0
        /// </summary>
        /// <param name="value">value, expects a boolean</param>
        /// <param name="targetType">target type</param>
        /// <param name="parameter">parameter</param>
        /// <param name="culture">culture</param>
        /// <returns>Visiblity.Visibile if value is true, Collapsed otherwize returns Collapsed if value is not a Visibility</returns>
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            try
            {
                var number = int.Parse(value?.ToString());
                return number > 0 ?
                    Visibility.Visible :
                        number == 0 ?
                            Visibility.Hidden
                            : Visibility.Collapsed;
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
        /// <returns>true if visivility is Visible, false otherwize returns false if value is not a boolean</returns>
        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            try
            {
                var visibility = (Visibility)value;
                return visibility == Visibility.Visible ? true : false;
            }
            catch
            {
                return false;
            }
        }
    }
}