using System.ComponentModel;
using System.Windows;

using WPFUtilities.Components.UI.DataGridExtensions;
using WPFUtilities.Extensions.DependencyObjects;
using WPFUtilities.Extensions.FrameworkElements;

using DataGridControlType = System.Windows.Controls.DataGrid;
using DataGridLength = System.Windows.Controls.DataGridLength;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// adjust column size modes
    /// </summary>
    public static partial class DataGrid
    {
        #region adjust column size modes

        /// <summary>
        /// get adjust column size modes
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static AdjustColumnSizeModes GetAdjustColumnSizeMode(DependencyObject dependencyObject) => (AdjustColumnSizeModes)dependencyObject.GetValue(AdjustColumnSizeModeProperty);

        /// <summary>
        /// set adjust column size modes
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="AdjustColumnSizeMode">AdjustColumnSizeMode</param>
        public static void SetAdjustColumnSizeMode(DependencyObject dependencyObject, AdjustColumnSizeModes AdjustColumnSizeMode) => dependencyObject.SetValue(AdjustColumnSizeModeProperty, AdjustColumnSizeMode);

        /// <summary>
        /// margin property
        /// </summary>
        public static readonly DependencyProperty AdjustColumnSizeModeProperty =
            DependencyProperty.Register(
                "AdjustColumnSizeMode",
                typeof(AdjustColumnSizeModes),
                typeof(DataGridControlType),
                new UIPropertyMetadata(AdjustColumnSizeModes.Inactive,
                    AdjustColumnSizeModeChanged));

        #endregion

        static void AdjustColumnSizeModeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (DesignerProperties.GetIsInDesignMode(dependencyObject)
                || !(dependencyObject is DataGridControlType datagrid)) return;

            datagrid.OnLoaded(
                (routed) =>
                {
                    InitializeAdjustColumnSize(datagrid);
                    datagrid.OnSizeChanged(
                        (sizeChanged) =>
                        {
                            AdjustColumnSizeMode(datagrid);
                        });
                });
        }

        static void InitializeAdjustColumnSize(DataGridControlType datagrid)
        {
            var props = datagrid.AddOrGetValue<DataGridViewProperties>(DataGridViewPropertiesProperty);
            props.InitialColumnsWidths = new DataGridLength[datagrid.Columns.Count];
            for (var i = 0; i < datagrid.Columns.Count; i++)
                props.InitialColumnsWidths[i] = datagrid.Columns[i].Width;
        }

        static void AdjustColumnSizeMode(DataGridControlType datagrid)
        {
            var mode = datagrid.GetValue<AdjustColumnSizeModes>(AdjustColumnSizeModeProperty);
            if (mode == AdjustColumnSizeModes.OnResize)
            {
                var props = datagrid.GetValue<DataGridViewProperties>(DataGridViewPropertiesProperty);
                for (var i = 0; i < datagrid.Columns.Count; i++)
                    datagrid.Columns[i].Width = props.InitialColumnsWidths[i];
            }
        }
    }
}
