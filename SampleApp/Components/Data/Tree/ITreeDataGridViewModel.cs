using System.Collections.Generic;

namespace SampleApp.Components.Data.Tree
{
    /// <summary>
    /// view model of a 'tree style' data grid
    /// </summary>
    public interface ITreeDataGridViewModel
    {
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
