using System;
using System.Windows.Controls;

using WPFUtilities.Extensions.DataSources;
using WPFUtilities.Extensions.DependencyObjects;
using WPFUtilities.Extensions.FrameworkElements;

namespace WPFUtilities.Extensions.DataGrids
{
    /// <summary>
    /// data grid extensions
    /// </summary>
    public static class DataGridExtensions
    {
        /// <summary>
        /// triggers an action when the datagrid source is changed and / or reset
        /// </summary>
        /// <param name="datagrid">datagrid</param>
        /// <param name="action">action on source changed</param>
        /// <param name="loadedAction">action on loaded (optional)</param>
        public static void OnSourceChanged(
            this DataGrid datagrid,
            Action<DataGrid> action,
            Action<DataGrid> loadedAction = null)
        {
            datagrid.OnLoaded(
                (routed) =>
                {
                    loadedAction?.Invoke(datagrid);
                    datagrid.OnValueChanged(
                        DataGrid.ItemsSourceProperty,
                        (args) =>
                        {
                            action(datagrid);
                            datagrid.OnItemSourceReset(
                                (source) => action(datagrid));
                        });
                });
        }
    }
}
