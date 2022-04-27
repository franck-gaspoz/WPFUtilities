using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using WPFUtilities.Components.UI.DataGridExtensions.Controls;
using WPFUtilities.Extensions.DependencyObjects;
using WPFUtilities.Extensions.FrameworkElements;

using DataGridControlType = System.Windows.Controls.DataGrid;
using DataGridTemplateColumnType = System.Windows.Controls.DataGridTemplateColumn;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// turn on tree column 'mode' on a datagrid column
    /// </summary>
    static partial class DataGrid
    {
        #region tree column

        /// <summary>
        /// get adjust column size modes
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static string GetTreeColumnName(DependencyObject dependencyObject) => (string)dependencyObject.GetValue(TreeColumnNameProperty);

        /// <summary>
        /// set adjust column size modes
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="TreeColumnName">SetTreeColumn</param>
        public static void SetTreeColumnName(DependencyObject dependencyObject, string TreeColumnName)
        {
            if (TreeColumnNameProperty == null) return;
            dependencyObject.SetValue(TreeColumnNameProperty, TreeColumnName);
        }

        /// <summary>
        /// margin property
        /// </summary>
        public static readonly DependencyProperty TreeColumnNameProperty =
            DependencyObjectExtensions.Register(
                "TreeColumnName",
                typeof(string),
                typeof(DataGridControlType),
                new UIPropertyMetadata(null, TreeColumnNameChanged));

        #endregion

        #region tree data grid cell style key

        /// <summary>
        /// get tree data grid cell style key
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>tree data grid cell style key</returns>
        public static string GetTreeDataGridCellStyleKey(DependencyObject dependencyObject) => (string)dependencyObject.GetValue(TreeDataGridCellStyleKeyProperty);

        /// <summary>
        /// set tree data grid cell style key
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="TreeDataGridCellStyleKey">tree data grid cell style key</param>
        public static void SetTreeDataGridCellStyleKey(DependencyObject dependencyObject, string TreeDataGridCellStyleKey)
        {
            if (TreeDataGridCellStyleKeyProperty == null) return;
            dependencyObject.SetValue(TreeDataGridCellStyleKeyProperty, TreeDataGridCellStyleKey);
        }

        /// <summary>
        /// tree data grid cell style key property
        /// </summary>
        public static readonly DependencyProperty TreeDataGridCellStyleKeyProperty =
            DependencyObjectExtensions.Register(
                "TreeDataGridCellStyleKey", 
                typeof(string), 
                typeof(DataGridControlType), 
                new PropertyMetadata("TreeDataGrid_TreeCell"));

        #endregion

        static void TreeColumnNameChanged(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs eventArgs)
        {
            if (DesignerProperties.GetIsInDesignMode(dependencyObject)) return;
            if (!(dependencyObject is DataGridControlType datagrid)) return;

            datagrid.OnLoaded((routed) =>
            {
                var treeColumnPath = datagrid.GetValue<string>(TreeColumnPathProperty);
                if (treeColumnPath == null)
                {
                    var treeColumn = GetTreeColumnName(dependencyObject);
                    datagrid.SetValue(TreeColumnPathProperty, treeColumn);
                }

                var cellDataTemplate = (DataTemplate)System.Windows.Application.Current
                    .FindResource(
                        datagrid.GetValue(TreeDataGridCellStyleKeyProperty));
                var name = datagrid.GetValue<string>(TreeColumnNameProperty);

                // search by name in custom column type
                DataGridColumn column;
                column = (DataGridColumn)datagrid.Columns
                    .OfType<IDataGridNamedColumn>()
                    .Where(x => x.Name == name)
                    .FirstOrDefault();

                // search by header
                if (column == null)
                    column = datagrid.Columns.Where(
                        x => x.Header.ToString() == name)
                            .FirstOrDefault();

                if (column is DataGridTemplateColumnType tplcol)
                {
                    tplcol.CellTemplate = cellDataTemplate;
                    var gridRowStyle = (Style)System.Windows.Application.Current
                        .FindResource("TreeDataGrid_Row");
                    datagrid.RowStyle = gridRowStyle;
                }
            });
        }
    }
}
