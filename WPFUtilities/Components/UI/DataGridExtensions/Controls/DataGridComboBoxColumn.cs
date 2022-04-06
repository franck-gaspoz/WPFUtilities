using System.Windows;

using WPFUtilities.Extensions.DependencyObjects;

using DataGridComboBoxColumnType = System.Windows.Controls.DataGridComboBoxColumn;

namespace WPFUtilities.Components.UI.DataGridExtensions.Controls
{
    /// <summary>
    /// datagrid check box column extension
    /// </summary>
    public class DataGridComboBoxColumn : DataGridComboBoxColumnType, IDataGridNamedColumn
    {
        #region name

        /// <summary>
        /// column name
        /// </summary>
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        /// <summary>
        /// name property
        /// </summary>
        public static readonly DependencyProperty NameProperty =
            DependencyObjectExtensions.Register(
                "Name",
                typeof(string),
                typeof(DataGridComboBoxColumn),
                new PropertyMetadata(null));

        #endregion
    }
}
