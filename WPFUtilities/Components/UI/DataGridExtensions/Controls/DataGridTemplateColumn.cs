using System.Windows;

using WPFUtilities.Extensions.DependencyObjects;

using DataGridColumnTemplateType = System.Windows.Controls.DataGridTemplateColumn;

namespace WPFUtilities.Components.UI.DataGridExtensions.Controls
{
    /// <summary>
    /// datagrid template column extension
    /// </summary>
    public class DataGridTemplateColumn : DataGridColumnTemplateType, IDataGridNamedColumn
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
            DependencyObjectExtensions.Register("Name",
                typeof(string),
                typeof(DataGridTemplateColumn),
                new PropertyMetadata(null));

        #endregion
    }
}
