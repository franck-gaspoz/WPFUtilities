using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using WPFUtilities.ComponentModels;
using WPFUtilities.Extensions.DependencyObjects;
using WPFUtilities.Helpers;

namespace SampleApp.Components.Data.Tree
{
    /// <summary>
    /// tree data grid column binding converter
    /// </summary>
    public class TreeDataGridColumnBindingConverter
        : SingletonService<TreeDataGridColumnBindingConverter>,
        IMultiValueConverter
    {
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
            var tb = values[1] as FrameworkElement;
            var dg = values[0] as DataGrid;
            var uc = WPFUtilities.Helpers.WPFHelper.FindAncestor<UserControl>(dg);
            var val0 = uc?.GetValue<string>(WPFUtilities.Components.UI.DataGrid.TreeColumnPathProperty);
            var val = dg?.GetValue<string>(WPFUtilities.Components.UI.DataGrid.TreeColumnPathProperty);
            var path = val0 ?? val;

            var value = PropertyPathHelper.GetValue(tb.DataContext, path);
            /*
            var binding = new Binding(path);
            binding.Source = tb.DataContext;
            BindingOperations.SetBinding(tb, missing dp)
            */
            return value;
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
