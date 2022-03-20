using System.Collections.Generic;
using System.ComponentModel;

using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Data.Tree
{
    /// <summary>
    /// view model of a 'tree style' data grid
    /// </summary>
    public interface ITreeDataGridViewModel<ViewModelBase> :
        IModelBase

        where ViewModelBase :
            ITreeDataGridRowViewModel<ViewModelBase>,
            IModelBase
    {
        /// <summary>
        /// items
        /// </summary>
        BindingList<ViewModelBase> Items { get; }

        /// <summary>
        /// selected item
        /// </summary>
        ViewModelBase SelectedItem { get; set; }

        /// <summary>
        /// get selected tree item
        /// </summary>
        /// <returns></returns>
        ITreeDataGridRowViewModel GetSelectedItem();

        /// <summary>
        /// get items
        /// </summary>
        /// <returns>enumerable of tree items</returns>
        IEnumerable<ITreeDataGridRowViewModel> GetItems();
    }
}
