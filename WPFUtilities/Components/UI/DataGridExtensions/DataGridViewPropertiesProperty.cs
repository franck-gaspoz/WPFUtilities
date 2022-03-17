using System.Windows;

using WPFUtilities.Components.UI.DataGridExtensions;
using WPFUtilities.Extensions.DependencyObjects;

using DataGridControlType = System.Windows.Controls.DataGrid;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// initial columns sizes
    /// </summary>
    public static partial class DataGrid
    {
        #region adjust column size modes

        /// <summary>
        /// get adjust column size modes
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static DataGridViewProperties GetColumnsSizesProperties(DependencyObject dependencyObject) => (DataGridViewProperties)dependencyObject.GetValue(DataGridViewPropertiesProperty);

        /// <summary>
        /// set adjust column size modes
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="ColumnsSizesProperties">ColumnsSizesProperties</param>
        public static void SetColumnsSizesProperties(DependencyObject dependencyObject, DataGridViewProperties ColumnsSizesProperties) => dependencyObject.SetValue(DataGridViewPropertiesProperty, ColumnsSizesProperties);

        /// <summary>
        /// margin property
        /// </summary>
        public static readonly DependencyProperty DataGridViewPropertiesProperty =
            DependencyObjectExtensions.Register(
                "DataGridViewProperties",
                typeof(DataGridViewProperties),
                typeof(DataGridControlType),
                new UIPropertyMetadata(null));

        #endregion
    }
}
