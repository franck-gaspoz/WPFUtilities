using System.Windows;

using WPFUtilities.Extensions.DependencyObjects;

using DataGridType = System.Windows.Controls.DataGrid;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// tree data grid control
    /// </summary>
    public class TreeDataGrid : DataGridType
    {
        #region TreeColumnPath

        /// <summary>
        /// tree column binding path
        /// </summary>
        public string TreeColumnPath
        {
            get { return (string)GetValue(TreeColumnPathProperty); }
            set { SetValue(TreeColumnPathProperty, value); }
        }

        /// <summary>
        /// tree column binding path property
        /// </summary>
        public static readonly DependencyProperty TreeColumnPathProperty =
            DependencyObjectExtensions.Register(
                "TreeColumnPath",
                typeof(string),
                typeof(TreeDataGrid),
                new PropertyMetadata(null));

        #endregion

        #region TreeColumnName

        /// <summary>
        /// tree column name
        /// </summary>
        public string TreeColumnName
        {
            get { return (string)GetValue(TreeColumnNameProperty); }
            set { SetValue(TreeColumnNameProperty, value); }
        }

        /// <summary>
        /// tree column name property
        /// </summary>
        public static readonly DependencyProperty TreeColumnNameProperty =
            DependencyObjectExtensions.Register(
                "TreeColumnName",
                typeof(string),
                typeof(TreeDataGrid),
                new PropertyMetadata(null));

        #endregion

        /// <summary>
        /// creates a new instance
        /// </summary>
        public TreeDataGrid()
        {

        }
    }
}
