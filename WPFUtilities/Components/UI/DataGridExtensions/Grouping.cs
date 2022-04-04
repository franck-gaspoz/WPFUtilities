using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

using WPFUtilities.Extensions.DependencyObjects;

using DataGridControlType = System.Windows.Controls.DataGrid;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// add groups on datagrid. Group := group1[,group2[,...groupn]]]
    /// </summary>
    public static partial class DataGrid
    {
        #region group

        /// <summary>
        /// get group
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static string GetGroup(DependencyObject dependencyObject)
            => (string)dependencyObject.GetValue(GroupingProperty);

        /// <summary>
        /// set group
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="Group">Group</param>
        public static void SetGroup(DependencyObject dependencyObject, string Group)
        {
            if (GroupingProperty == null) return;
            dependencyObject.SetValue(GroupingProperty, Group);
        }

        /// <summary>
        /// adjust group site mode property
        /// </summary>
        public static readonly DependencyProperty GroupingProperty =
            DependencyObjectExtensions.Register(
                "Grouping",
                typeof(string),
                typeof(DataGridControlType),
                new UIPropertyMetadata(
                    null,
                    GroupChanged
                    ));

        #endregion

        static string[] GetGroups(DependencyObject dependencyObject)
            => GetGroup(dependencyObject)?.Split(',');

        static void GroupChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (DesignerProperties.GetIsInDesignMode(dependencyObject)
                || !(dependencyObject is DataGridControlType datagrid)) return;

            datagrid.Items.GroupDescriptions.Clear();
            foreach (var group in GetGroups(dependencyObject))
                datagrid.Items.GroupDescriptions.Add(
                    new PropertyGroupDescription(group));
        }

    }
}