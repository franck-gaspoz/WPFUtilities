using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFUtilities.Components.UI.TreeDataGridControl
{
    /// <summary>
    /// control that draw lines joining tree nodes
    /// <para>expect data context is ITreeDataGridRowViewModel</para>
    /// </summary>
    public class TreeNodeLines : ItemsControl
    {
        /// <summary>
        /// creates a new instance
        /// </summary>
        public TreeNodeLines()
        {
            DataContextChanged += TreeNodeLines_DataContextChanged;
        }

        private void TreeNodeLines_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is ITreeDataGridRowViewModel viewModel)
            {
                System.Diagnostics.Debug.WriteLine(viewModel.Level);
            }
        }
    }
}
