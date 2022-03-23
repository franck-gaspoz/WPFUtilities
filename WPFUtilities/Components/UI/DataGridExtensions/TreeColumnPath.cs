using System.Windows;

using WPFUtilities.Extensions.DependencyObjects;

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
        #region tree column path

        /// <summary>
        /// get column path
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static string GetTreeColumnPath(DependencyObject dependencyObject) => (string)dependencyObject.GetValue(TreeColumnPathProperty);

        /// <summary>
        /// set column path
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="TreeColumnPath">TreeColumnPath</param>
        public static void SetTreeColumnPath(DependencyObject dependencyObject, string TreeColumnPath)
        {
            if (TreeColumnPathProperty == null) return;
            dependencyObject.SetValue(TreeColumnPathProperty, TreeColumnPath);
        }

        /// <summary>
        /// column path property
        /// </summary>
        public static readonly DependencyProperty TreeColumnPathProperty =
            DependencyObjectExtensions.Register(
                "TreeColumnPath",
                typeof(string),
                typeof(DataGridControlType),
                new UIPropertyMetadata(null));

        #endregion
    }
}
