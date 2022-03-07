using System.Linq;
using System.Windows;
using System.Windows.Controls;

using WPFUtilities.Extensions.DependencyObjects;
using WPFUtilities.Extensions.FrameworkElements;

using DataGridControlType = System.Windows.Controls.DataGrid;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// turn on tree column 'mode' on a datagrid column
    /// <para>needs the datagrid items source to be of type I..</para>
    /// <para>needs the ...</para>
    /// </summary>
    static partial class DataGrid
    {
        #region tree column

        /// <summary>
        /// get adjust column size modes
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static string GetSetTreeColumn(DependencyObject dependencyObject) => (string)dependencyObject.GetValue(SetTreeColumnProperty);

        /// <summary>
        /// set adjust column size modes
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="SetTreeColumn">SetTreeColumn</param>
        public static void SetSetTreeColumn(DependencyObject dependencyObject, string SetTreeColumn) => dependencyObject.SetValue(SetTreeColumnProperty, SetTreeColumn);

        /// <summary>
        /// margin property
        /// </summary>
        public static readonly DependencyProperty SetTreeColumnProperty =
            DependencyProperty.Register(
                "SetTreeColumn",
                typeof(string),
                typeof(DataGridControlType),
                new UIPropertyMetadata(null, SetTreeColumnChanged));

        #endregion

        static void SetTreeColumnChanged(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs eventArgs)
        {
            if (!(dependencyObject is DataGridControlType datagrid)) return;
            datagrid.OnLoaded((routed) =>
            {
                var cellDataTemplate = (DataTemplate)System.Windows.Application.Current
                    .FindResource("TreeDataGrid_TreeCell");
                var name = datagrid.GetValue<string>(SetTreeColumnProperty);
                var column = datagrid.Columns.Where(
                    x => x.Header.ToString() == name)
                        .FirstOrDefault();
                if (column is DataGridTemplateColumn tplcol)
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
