using System.Windows;
using System.Windows.Data;

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
        public static Binding GetTreeColumnName(DependencyObject dependencyObject) => (Binding)dependencyObject.GetValue(TreeColumnNameProperty);

        /// <summary>
        /// set adjust column size modes
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="TreeColumnName">TreeColumnName</param>
        public static void SetTreeColumnName(DependencyObject dependencyObject, Binding TreeColumnName) => dependencyObject.SetValue(TreeColumnNameProperty, TreeColumnName);

        /// <summary>
        /// margin property
        /// </summary>
        public static readonly DependencyProperty TreeColumnNameProperty =
            DependencyProperty.Register(
                "TreeColumnName",
                typeof(Binding),
                typeof(DataGridControlType),
                new UIPropertyMetadata(null));

        #endregion
    }
}
