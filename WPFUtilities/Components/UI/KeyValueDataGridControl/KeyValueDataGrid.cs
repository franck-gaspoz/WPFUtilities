using System.ComponentModel;
using System.Windows;

using WPFUtilities.Extensions.DependencyObjects;
using WPFUtilities.Extensions.FrameworkElements;

using DataGridType = System.Windows.Controls.DataGrid;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// key value datagrid
    /// </summary>
    public class KeyValueDataGrid : DataGridType
    {
        static KeyValueDataGrid()
        {
            /*DefaultStyleKeyProperty.OverrideMetadata(typeof(KeyValueDataGrid),
                new FrameworkPropertyMetadata(typeof(KeyValueDataGrid)));*/
        }

        #region key column header

        /// <summary>
        /// key column header
        /// </summary>
        public object KeyColumnHeader
        {
            get { return (object)GetValue(KeyColumnHeaderProperty); }
            set { SetValue(KeyColumnHeaderProperty, value); }
        }

        /// <summary>
        /// key column header property
        /// </summary>
        public static readonly DependencyProperty KeyColumnHeaderProperty =
            DependencyObjectExtensions.Register(
                "KeyColumnHeader",
                typeof(object),
                typeof(KeyValueDataGrid),
                new PropertyMetadata("Key"));

        #endregion

        #region value column header

        /// <summary>
        /// value column header
        /// </summary>
        public object ValueColumnHeader
        {
            get { return (object)GetValue(ValueColumnHeaderProperty); }
            set { SetValue(ValueColumnHeaderProperty, value); }
        }

        /// <summary>
        /// value column header property
        /// </summary>
        public static readonly DependencyProperty ValueColumnHeaderProperty =
            DependencyObjectExtensions.Register(
                "ValueColumnHeader",
                typeof(object),
                typeof(KeyValueDataGrid),
                new PropertyMetadata("Value"));

        #endregion

        #region grouping

        /// <summary>
        /// get grouping
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static string GetGrouping(DependencyObject dependencyObject) => (string)dependencyObject.GetValue(DataGridProperty);

        /// <summary>
        /// set grouping
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="grouping">groups definition: [group1][,..,[groupn]]</param>
        public static void SetGrouping(DependencyObject dependencyObject, string grouping)
        {
            if (DataGridProperty == null) return;
            dependencyObject.SetValue(DataGridProperty, grouping);
        }

        /// <summary>
        /// grouping property
        /// </summary>
        public static readonly DependencyProperty DataGridProperty =
            DependencyObjectExtensions.Register(
                "Grouping",
                typeof(string),
                typeof(KeyValueDataGrid),
                new UIPropertyMetadata(null, GroupingChanged));

        private static void GroupingChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (DesignerProperties.GetIsInDesignMode(dependencyObject)
                || !(dependencyObject is KeyValueDataGrid keyValueView)) return;

            keyValueView.OnRendered(() =>
            {
                var datagrid =
                    WPFUtilities.Helpers.WPFHelper.FindVisualChild<DataGridType>(keyValueView);
                if (datagrid != null)
                    datagrid.SetValue(DataGrid.GroupingProperty, GetGrouping(keyValueView));
            });
        }

        #endregion
    }
}
