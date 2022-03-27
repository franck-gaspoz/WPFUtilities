using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using WPFUtilities.Extensions.DependencyObjects;

using DataGridControlType = System.Windows.Controls.DataGrid;

namespace WPFUtilities.Components.UI
{
    /// <summary>
    /// add sort on datagrid column - preserve previous sorts
    /// </summary>
    public static partial class DataGrid
    {
        #region sort property name + direction

        /// <summary>
        /// get sort
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static string GetSort(DependencyObject dependencyObject)
            => (string)dependencyObject.GetValue(SortProperty);

        /// <summary>
        /// set sort
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="Sort">Sort</param>
        public static void SetSort(DependencyObject dependencyObject, string Sort)
        {
            if (SortProperty == null) return;
            dependencyObject.SetValue(SortProperty, Sort);
        }

        /// <summary>
        /// adjust sort site mode property
        /// </summary>
        public static readonly DependencyProperty SortProperty =
            DependencyObjectExtensions.Register(
                "Sort",
                typeof(string),
                typeof(DataGridControlType),
                new UIPropertyMetadata(
                    null,
                    SortChanged));

        #endregion

        static void SortChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (DesignerProperties.GetIsInDesignMode(dependencyObject)
                || !(dependencyObject is DataGridControlType datagrid)) return;

            var sort = GetSort(datagrid);
            var values = sort.Split(':');
            if (values.Length != 2
                || (values[1] != "ASC"
                && values[1] != "DESC"))
                throw new ArgumentException("sort should be formated 'Name:ASC' or 'Name:DESC'");

            var sortPath = values[0];
            var sortDir = values[1] == "ASC" ?
                        ListSortDirection.Ascending
                        : ListSortDirection.Descending;
            datagrid.Items.SortDescriptions.Add(
                new SortDescription(
                    sortPath,
                    sortDir
                ));

            datagrid.Columns.CollectionChanged += (o, e) =>
            {
                foreach (var dc in datagrid.Columns
                    .Where(x => x is DataGridBoundColumn)
                    .Cast<DataGridBoundColumn>())
                {
                    if (dc.Binding is Binding binding
                        && binding.Path.Path == values[0])
                    {
                        dc.SortDirection = sortDir;
                    }
                }
            };
        }
    }
}