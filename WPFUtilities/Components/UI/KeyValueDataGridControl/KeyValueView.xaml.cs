using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using WPFUtilities.Extensions.DependencyObjects;

using DataGridType = System.Windows.Controls.DataGrid;

namespace WPFUtilities.Components.UI.KeyValueDataGridControl
{
    /// <summary>
    /// key value view
    /// </summary>
    public partial class KeyValueView : UserControl
    {
        /// <summary>
        /// creates a new instance
        /// </summary>
        public KeyValueView()
        {
            InitializeComponent();
        }

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
                typeof(KeyValueView),
                new PropertyMetadata("Key"));

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
                typeof(KeyValueView),
                new PropertyMetadata("Value"));

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
                typeof(KeyValueView),
                new UIPropertyMetadata(null, GroupingChanged));

        private static void GroupingChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (DesignerProperties.GetIsInDesignMode(dependencyObject)
                || !(dependencyObject is KeyValueView keyValueView)) return;

            var datagrid = (DataGridType)keyValueView.FindName("datagrid");
            if (datagrid != null)
                datagrid.SetValue(DataGrid.GroupingProperty, GetGrouping(keyValueView));
        }

        #endregion
    }
}
