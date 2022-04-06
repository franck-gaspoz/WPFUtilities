using System.Windows;

using WPFUtilities.Extensions.DependencyObjects;

using DataGridCheckBoxColumnType = System.Windows.Controls.DataGridCheckBoxColumn;

namespace WPFUtilities.Components.UI.DataGridExtensions.Controls
{
    /// <summary>
    /// datagrid check box column extension
    /// </summary>
    public class DataGridCheckBoxColumn : DataGridCheckBoxColumnType, IDataGridNamedColumn
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
                typeof(DataGridCheckBoxColumn),
                new PropertyMetadata(null));

        #endregion
    }
}
