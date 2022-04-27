using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

using WPFUtilities.ComponentModels;

using app = System.Windows.Application;

namespace WPFUtilities.Components.UI.TreeDataGridControl
{
    /// <summary>
    /// converts the datagrid row is selected state to color of the expand collapse toggle path fill color
    /// </summary>
    public class RowSelectedStateToExpandCollapseTogglePathColorConverter :
        SingletonService<RowSelectedStateToExpandCollapseTogglePathColorConverter>,
        IMultiValueConverter
    {
        static readonly Color FallbackColor = Color.FromRgb(255, 0, 0);

        /// <summary>
        /// convert from bool to color
        /// </summary>
        /// <param name="values">value, expects [DataGridRow,ITreeDataGridRowViewModel,bool]</param>
        /// <param name="targetType">target type</param>
        /// <param name="parameter">parameter</param>
        /// <param name="culture">culture</param>
        /// <returns>if value is true, resource color 'GlyphColor' else resource color 'InvertedGlyphColor', red if resource not found</returns>
        public object Convert(
            object[] values,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            try
            {
                var row = (DataGridRow)values[0];
                var rowViewModel = (ITreeDataGridRowViewModel)values[1];
                var isSelected = (bool)values[2];
                var color =
                    app.Current.TryFindResource(
                        !isSelected ? 
                            "GlyphColor" : 
                            "InvertedGlyphColor")
                    ?? FallbackColor;
                /*
                var style =
                    (Style)app.Current.TryFindResource(
                        !isSelected ? 
                            "ExpandCollapseTogglePathUnselectedStyle" : 
                            "ExpandCollapseTogglePathSelectedStyle");
                row.Style = style;
                */
                return color;
            }
            catch
            {
                return FallbackColor;
            }
        }

        /// <summary>
        /// convert back from Visiblity to bool
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="targetTypes">target types</param>
        /// <param name="parameter">parameter</param>
        /// <param name="culture">culture</param>
        /// <exception cref="NotImplementedException">not implemented</exception>
        /// <returns></returns>
        public object[] ConvertBack(
            object value,
            Type[] targetTypes,
            object parameter,
            CultureInfo culture) 
            => throw new NotImplementedException();
    }
}