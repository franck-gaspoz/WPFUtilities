using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using WPFUtilities.Extensions.DependencyObjects;
using WPFUtilities.Extensions.FrameworkElements;

using DataGridControlType = System.Windows.Controls.DataGrid;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// append columns from source
    /// </summary>
    public static partial class DataGrid
    {
        #region append columns 

        /// <summary>
        /// get adjust column size modes
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static ObservableCollection<DataGridColumn> GetAppendColumns(DependencyObject dependencyObject) => (ObservableCollection<DataGridColumn>)dependencyObject.GetValue(AppendColumnsProperty);

        /// <summary>
        /// set adjust column size modes
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="AppendColumns">AppendColumns</param>
        public static void SetAppendColumns(DependencyObject dependencyObject, ObservableCollection<DataGridColumn> AppendColumns) => dependencyObject.SetValue(AppendColumnsProperty, AppendColumns);

        /// <summary>
        /// margin property
        /// </summary>
        public static readonly DependencyProperty AppendColumnsProperty =
            DependencyProperty.RegisterAttached(
                "AppendColumns",
                typeof(ObservableCollection<DataGridColumn>),
                typeof(DataGrid),
                new UIPropertyMetadata(null, AppendColumnsChanged));

        #endregion

        static void AppendColumnsChanged(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs eventArgs)
        {
            if (!(dependencyObject is DataGridControlType datagrid)) return;

            datagrid.OnLoaded((routed) =>
            {
                try
                {
                    var columns = datagrid.GetValue<ObservableCollection<DataGridColumn>>(AppendColumnsProperty);
                    if (columns == null) return;

                    if (datagrid.Columns.Count == 1)
                        foreach (var column in columns)
                            if (!datagrid.Columns.Where(x => x.Header.ToString() == column.Header.ToString()).Any())
                                datagrid.Columns.Add(column);
                }
                catch (Exception) { }
            }, false);
        }

    }
}
